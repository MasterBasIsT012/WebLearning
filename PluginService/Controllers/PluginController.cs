using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using PluginService.Data;
using Infrastructure.DTOs;
using Plugins;
using System.Reflection;

namespace PluginService.Controllers
{
	[Route("api/Plugin/{actions}")]
	public class PluginController : Controller
	{
		private readonly IPluginService pluginService;

		public PluginController(IPluginService pluginService)
		{
			this.pluginService = pluginService;
		}

		[HttpGet]
		public IActionResult GetPlugins()
		{
			List<IPluginMethodInfo> plugins = pluginService.GetPlugins();
			List<ClassDTO> classessDTO = GetPluginsDTOs(plugins);
			return Ok(JsonConvert.SerializeObject(classessDTO));
		}
		private List<ClassDTO> GetPluginsDTOs(List<IPluginMethodInfo> pluginMethods)
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
				classDTO.Methods.Add(GetMethodDTO(plugin));
			}
			classDTOs.Add(classDTO);

			return classDTOs;
		}
		private ClassDTO GetClassDTO(IPluginMethodInfo plugin)
		{
			Plugin pluginAttribute = (Plugin)plugin.Method.DeclaringType.GetCustomAttribute(typeof(Plugin));
			return new ClassDTO() { Name = plugin.ClassName, Vers = pluginAttribute.Version, Methods = new List<MethodInfoDTO>() };
		}
		private MethodInfoDTO GetMethodDTO(IPluginMethodInfo plugin)
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

		[HttpPost]
		public IActionResult LoadPlugins([FromBody]string path)
		{
			PluginLoader.Path = path;
			pluginService.LoadPlugins(PluginLoader.Instance);
			return Ok();
		}
	}
}
