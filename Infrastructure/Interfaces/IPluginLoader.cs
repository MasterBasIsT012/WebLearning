using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
	public interface IPluginLoader
	{
		public List<IPluginMethodInfo> PluginMethods { get; }
		public List<IPluginMethodInstance> PluginMethodInstances { get; }

		void LoadPlugins();
	}
}