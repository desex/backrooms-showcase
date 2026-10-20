using Plugins.GelfLogger;

namespace GelfLogger.Impl
{
	public class GelfLoggerWrapper : IGelfLogger
	{
		public bool LogSelfExceptions { get; set; }

		public void SetAppMetricaId(string id)
		{
		}

		public IGelfLogger AddDeniedType<T>() => this;

		public IGelfLogger AddCustomName(string customName) => this;

		public IGelfLogger AddNamespace(string @namespace) => this;
	}
}