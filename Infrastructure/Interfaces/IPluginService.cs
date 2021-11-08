using Infrastructure.DTOs;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
	public interface IPluginService
	{
		void LoadPlugins(IPluginLoader loader);
		void ExecSimplePlugin(string method);
		List<IPluginMethodInfo> GetPlugins();
		List<ClassDTO> GetPluginsDTOs();
		List<IPluginMethodInfo> GetSimplePlugins();
		List<ClassDTO> GetSimplePluginsDTOs();
	}
}