using System;
using System.Globalization;
using UnityEngine;

namespace Plugins.GelfLogger.Core
{
	[Serializable]
	public struct GelfMessage
	{
		[SerializeField] private string short_message;
		[SerializeField] private long timestamp;
		[SerializeField] private int level;

		[SerializeField] private string _stack_trace;
		[SerializeField] private string _os;
		[SerializeField] private string _os_version;
		[SerializeField] private string _device;
		[SerializeField] private string _device_uid;
		[SerializeField] private string _resolution;
		[SerializeField] private string _app_version;
		[SerializeField] private string _app_id;
		[SerializeField] private string _app_name;
		[SerializeField] private string _cpu;
		[SerializeField] private string _opengl;
		[SerializeField] private int _memory_size;
		[SerializeField] private int _gpu_memory_size;
		[SerializeField] private string _battery;
		[SerializeField] private string _orientation;
		[SerializeField] private string _online;
		[SerializeField] private string _session;
		[SerializeField] private string _source;
		[SerializeField] private string _app_metrica_id;

		public string ShortMessage => short_message;

		public int Level => level;

		public GelfMessage(
			string shortMessage,
			string stackTrace,
			int level,
			string source,
			string session,
			string appMetricaID
		) : this()
		{
			short_message = shortMessage;
			_stack_trace = stackTrace;
			timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			this.level = level;

			_os = Application.platform.ToString().ToLower();
			_os_version = SystemInfo.operatingSystem;
			_device = SystemInfo.deviceName;
			_device_uid = SystemInfo.deviceUniqueIdentifier;
			_resolution = Screen.currentResolution.ToString();
			_app_version = Application.version;
			_app_id = Application.identifier;
			_app_name = Application.productName;
			_cpu = SystemInfo.processorType;
			_opengl = SystemInfo.graphicsDeviceVersion;
			_memory_size = SystemInfo.systemMemorySize;
			_gpu_memory_size = SystemInfo.graphicsMemorySize;
			_battery = SystemInfo.batteryLevel.ToString(CultureInfo.InvariantCulture);
			_orientation = Screen.orientation.ToString();
			_online = (Application.internetReachability > 0).ToString();
			_session = session;
			_source = source;
			_app_metrica_id = appMetricaID;
		}
	}
}