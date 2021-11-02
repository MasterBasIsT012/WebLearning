using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using PluginService.Data;

namespace PluginService.Controllers
{
	[Route("api/Plugin/{action}")]
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
			return Ok(JsonConvert.SerializeObject(plugins));
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
