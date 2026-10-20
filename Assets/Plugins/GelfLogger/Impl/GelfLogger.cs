using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using PdUtils.Web;
using PdUtils.Web.Impl;
using PimDeWitte.UnityMainThreadDispatcher;
using Plugins.GelfLogger.Core;
using UnityEngine;

namespace Plugins.GelfLogger.Impl
{
	public class GelfLogger : MonoBehaviour, IGelfLogger
	{
		private const int MESSAGE_POOL_SIZE = 64;
		public const string SESSION_SAVE_KEY = "GelfLogger.Session.Number";

		[SerializeField] private string host;
		[SerializeField] private string source;

		private string _sessionNumber;
		private string _appMetricaId;
		private bool _hasException;

		private readonly List<string> _deniedTypes = new();
		private readonly Queue<string> _messagePool = new(MESSAGE_POOL_SIZE);

		private IX509CertificateValidator _certificateValidator;
		private Thread _messageThread;

		public bool LogSelfExceptions { get; set; }

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
			var sessionNumber = PlayerPrefs.GetInt(SESSION_SAVE_KEY, 0) + 1;
			_sessionNumber = sessionNumber.ToString();
			PlayerPrefs.SetInt(SESSION_SAVE_KEY, sessionNumber);
			PlayerPrefs.Save();
			Application.logMessageReceived += LogCallback;
			_certificateValidator = new X509CertificateValidatorAlwaysTrue();
			_messageThread = new Thread(SendMessagesThread);
			_messageThread.Start();
		}

		private void OnDestroy()
		{
			_messageThread?.Abort();
			_messageThread = null;
			Application.logMessageReceived -= LogCallback;
		}

		public void SetAppMetricaId(string id) => _appMetricaId = id;

		public IGelfLogger AddDeniedType<T>()
		{
			var type = typeof(T);
			if (!_deniedTypes.Contains(type.Name))
			{
				_deniedTypes.Add(type.Name);
			}

			return this;
		}

		public IGelfLogger AddCustomName(string customName)
		{
			if (!_deniedTypes.Contains(customName))
			{
				_deniedTypes.Add(customName);
			}

			return this;
		}

		public IGelfLogger AddNamespace(string @namespace)
		{
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(t => t.GetTypes())
				.Where(t => t.IsClass && t.Namespace == @namespace);

			foreach (var type in types)
			{
				if (!_deniedTypes.Contains(type.Name))
				{
					_deniedTypes.Add(type.Name);
				}
			}

			return this;
		}

		private void LogCallback(string message, string stackTrace, LogType logType)
		{
			if (_hasException)
				return;

			if (logType == LogType.Exception)
				_hasException = true;

			var gelfLevel = GetGelfLevel(logType);
			var gelfMessage = new GelfMessage(
				message,
				stackTrace,
				(int)gelfLevel,
				source,
				_sessionNumber,
				_appMetricaId
			);
			TrySendLog(gelfMessage);
		}

		private void TrySendLog(GelfMessage gelfMessage)
		{
			const int information = (int)GelfLevel.Informational;
			const int warning = (int)GelfLevel.Warning;
			const int debug = (int)GelfLevel.Debug;

			switch (gelfMessage.Level)
			{
				case information:
				{
					var extractedType = ExtractTypeFromLogMessage(gelfMessage.ShortMessage);
					if (string.IsNullOrEmpty(extractedType) || _deniedTypes.Contains(extractedType))
						return;
					break;
				}
				case warning or debug:
					return;
			}

			var json = JsonUtility.ToJson(gelfMessage);
			lock (_messagePool)
			{
				if (_messagePool.Count >= MESSAGE_POOL_SIZE)
					_messagePool.Dequeue();
				_messagePool.Enqueue(json);
			}
		}

		private async void SendMessagesThread()
		{
			while (true)
			{
				try
				{
					if (_messagePool.Count == 0)
					{
						Thread.Sleep(100);
						continue;
					}

					string json;

					lock (_messagePool)
					{
						json = _messagePool.Dequeue();
					}

					var dataBytes = Encoding.UTF8.GetBytes(json);
					var messageSend = false;
					while (!messageSend)
					{
						try
						{
							var request = (HttpWebRequest)WebRequest.Create(host);
							request.ServerCertificateValidationCallback += _certificateValidator.ValidateCertificate;
							request.Timeout = 5000;
							request.ContentLength = dataBytes.Length;
							request.ContentType = "application/x-www-form-urlencoded";
							request.Method = "POST";

							using (var requestBody = await request.GetRequestStreamAsync())
							{
								await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
							}

							using (var response = await request.GetResponseAsync())
							{
							}

							messageSend = true;
						}
						catch (Exception e)
						{
							if (LogSelfExceptions)
                                await UnityMainThreadDispatcher.Instance().EnqueueAsync(() => Debug.LogException(e));
							Thread.Sleep(30);
						}
					}
				}
				catch (Exception e)
				{
					if (LogSelfExceptions)
                        await UnityMainThreadDispatcher.Instance().EnqueueAsync(() => Debug.LogException(e));
					Thread.Sleep(30);
				}
			}
		}

		private static string ExtractTypeFromLogMessage(string message)
		{
			var startIndex = message.IndexOf("[", StringComparison.Ordinal);
			if (startIndex == -1) return null;

			var endIndex = message.IndexOf("]", StringComparison.Ordinal);
			if (endIndex == -1) return null;

			return message.Substring(startIndex + 1, endIndex - startIndex - 1);
		}

		private static GelfLevel GetGelfLevel(LogType logType)
		{
			switch (logType)
			{
				case LogType.Error:
					return GelfLevel.Error;
				case LogType.Assert:
					return GelfLevel.Debug;

				case LogType.Warning:
					return GelfLevel.Warning;

				case LogType.Log:
					return GelfLevel.Informational;

				case LogType.Exception:
					return GelfLevel.Critical;

				default:
					throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
			}
		}
	}
}