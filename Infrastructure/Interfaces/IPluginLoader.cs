using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
	public interface IPluginLoader
	{
		List<IPluginMethodInfo> PluginMethods { get; }

		void LoadPlugins();
	}
}