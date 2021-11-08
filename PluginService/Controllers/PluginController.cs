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
	[Route("api/Plugin")]
	public class PluginController : Controller
	{
		private readonly IPluginService pluginService;

		public PluginController(IPluginService pluginService)
		{
			this.pluginService = pluginService;
		}

		[HttpGet]
		[Route("GetPlugins")]
		public IActionResult GetPlugins()
		{
			List<IPluginMethodInfo> plugins = pluginService.GetPlugins();
			List<ClassDTO> classessDTO = ClassDTO.GetPluginsDTOs(plugins);
			return Ok(JsonConvert.SerializeObject(classessDTO));
		}

		[HttpGet]
		[Route("GetSimplePLugins")]
		public IActionResult GetSimplePlugins()
		{
			List<IPluginMethodInfo> simplePlugins = pluginService.GetSimplePlugins();
			List<ClassDTO> classesDTOs = ClassDTO.GetPluginsDTOs(simplePlugins);
			return Ok(JsonConvert.SerializeObject(classesDTOs));
		}

		[HttpPost]
		[Route("LoadPlugins")]
		public IActionResult LoadPlugins([FromBody]string path)
		{
			PluginLoader.Path = path;
			pluginService.LoadPlugins(PluginLoader.Instance);
			return Ok();
		}
	}
}
