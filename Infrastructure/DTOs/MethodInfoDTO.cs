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
				ReturnTypeName = plugin.ReturnType.Name,
				Args = GetParameters(plugin)
			};

			return methodInfoDTO;
		}
		private List<string> GetParameters(IPluginMethodInfo plugin)
		{
			List<string> parameters = new List<string>();

			for (int i = 0; i < plugin.Arguments.Length; i++)
			{
				parameters.Add(plugin.Arguments[i].ParameterType.Name);
			}

			return parameters;
		}
	}
}
