namespace Plugins.GelfLogger
{
	public interface IGelfLogger
	{
		bool LogSelfExceptions { get; set; }

		void SetAppMetricaId(string id);

		IGelfLogger AddDeniedType<T>();
		IGelfLogger AddCustomName(string customName);
		IGelfLogger AddNamespace(string @namespace);
	}
}