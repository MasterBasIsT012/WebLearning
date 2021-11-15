using Infrastructure.Interfaces;
using System.Collections.Generic;

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
			List<ClassDTO> classesDTO = new List<ClassDTO>();
			ClassDTO classDTO = new ClassDTO();
			string className = string.Empty;

			foreach (IPluginMethodInfo plugin in pluginMethods)
			{
				if (className != plugin.ClassName)
				{
					if (!string.IsNullOrEmpty(className))
						classesDTO.Add(classDTO);
					classDTO = GetClassDTO(plugin);
					className = plugin.ClassName;
				}
				MethodInfoDTO methodInfoDTO = new MethodInfoDTO(plugin);
				classDTO.Methods.Add(methodInfoDTO);
			}
			classesDTO.Add(classDTO);

			return classesDTO;
		}
		private static ClassDTO GetClassDTO(IPluginMethodInfo plugin)
		{
			return new ClassDTO() { Name = plugin.ClassName, Vers = plugin.Vers, Methods = new List<MethodInfoDTO>() };
		}
	}
}
