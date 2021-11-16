using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PluginService.Data;
using System.Collections.Generic;

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

		[Route("GetPlugins")]
		[HttpGet]
		public IActionResult GetPlugins()
		{
			List<IPluginMethodInfo> plugins = pluginService.GetPlugins();
			PluginsDTO pluginsDTO = new PluginsDTO();
			pluginsDTO.Plugins = ClassDTO.GetPluginsDTOs(plugins);
			return Ok(JsonConvert.SerializeObject(pluginsDTO));
		}

		[Route("GetSimplePLugins")]
		[HttpGet]
		public IActionResult GetSimplePlugins()
		{
			List<IPluginMethodInfo> simplePlugins = pluginService.GetSimplePlugins();
			PluginsDTO pluginsDTO = new PluginsDTO()
			{
				Plugins = ClassDTO.GetPluginsDTOs(simplePlugins)
			};
			return Ok(JsonConvert.SerializeObject(pluginsDTO));
		}

		[Route("ExecSimplePlugin")]
		[HttpPost]
		public IActionResult ExecSimplePlugin([FromBody] string method)
		{
			SimplePluginDTO simplePluginDTO = new SimplePluginDTO();
			simplePluginDTO.Result = pluginService.ExecSimplePlugin(method);
			return Ok(JsonConvert.SerializeObject(simplePluginDTO));
		}

		[Route("LoadPlugins")]
		[HttpPost]
		public IActionResult LoadPlugins([FromBody] string path)
		{
			PluginLoader.Path = path;
			pluginService.LoadPlugins(PluginLoader.Instance);
			return Ok();
		}
	}
}
