using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
	public interface IPluginService
	{
		void LoadPlugins(IPluginLoader loader);
		string ExecSimplePlugin(string method);
		List<IPluginMethodInfo> GetPlugins();
		List<IPluginMethodInfo> GetSimplePlugins();
	}
}