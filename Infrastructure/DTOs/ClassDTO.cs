using Infrastructure.Interfaces;
using Plugins;
using System.Collections.Generic;
using System.Reflection;

namespace Infrastructure.DTOs
{
	public class ClassDTO
	{
		public string Name { get; set; }
		public string Vers { get; set; }
		public List<MethodInfoDTO> Methods { get; set; }

		public ClassDTO() { }

		public static List<ClassDTO> GetPluginsDTOs(List<IPluginMethodInfo> pluginMethods)
		{
			List<ClassDTO> classDTOs = new List<ClassDTO>();
			ClassDTO classDTO = new ClassDTO();
			string className = string.Empty;

			foreach (IPluginMethodInfo plugin in pluginMethods)
			{
				if (className != plugin.ClassName)
				{
					if (!string.IsNullOrEmpty(className))
						classDTOs.Add(classDTO);
					classDTO = GetClassDTO(plugin);
					className = plugin.ClassName;
				}
				MethodInfoDTO methodInfoDTO = new MethodInfoDTO(plugin);
				classDTO.Methods.Add(methodInfoDTO);
			}
			classDTOs.Add(classDTO);

			return classDTOs;
		}
		private static ClassDTO GetClassDTO(IPluginMethodInfo plugin)
		{
			Plugin pluginAttribute = (Plugin)plugin.Method.DeclaringType.GetCustomAttribute(typeof(Plugin));
			return new ClassDTO() { Name = plugin.ClassName, Vers = pluginAttribute.Version, Methods = new List<MethodInfoDTO>() };
		}
	}
}
