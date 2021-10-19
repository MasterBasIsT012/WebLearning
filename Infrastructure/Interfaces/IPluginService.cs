using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
	public interface IPluginService
	{
		void ExecSimplePlugin(string method);
		List<IPluginMethodInfo> GetPlugins();
		IEnumerable<IPluginMethodInfo> GetSimplePlugins();
	}
}