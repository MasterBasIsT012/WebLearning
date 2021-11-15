using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
	public class MethodInfoDTO
	{
		public string Name { get; set; }
		public string ReturnTypeName { get; set; }
		public List<string> Args { get; set; }

		public MethodInfoDTO() { }
		public MethodInfoDTO(IPluginMethodInfo plugin)
		{
			MethodInfoDTO methodInfoDTO = GetMethodDTO(plugin);
			Name = methodInfoDTO.Name;
			ReturnTypeName = methodInfoDTO.ReturnTypeName;
			Args = methodInfoDTO.Args;
		}

		public MethodInfoDTO GetMethodDTO(IPluginMethodInfo plugin)
		{
			MethodInfoDTO methodInfoDTO = new MethodInfoDTO()
			{
				Name = plugin.MethodName,
				ReturnTypeName = plugin.ReturnType,
				Args = plugin.Arguments
			};

			return methodInfoDTO;
		}
	}
}
