using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;

namespace WebLearning.Controllers
{
	[Route("api")]
	public class MainController : Controller
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly IReportService reportService;
		private readonly IPluginService pluginService;

		public MainController(IReportService reportService, IPluginService pluginService)
		{
			this.reportService = reportService;
			this.pluginService = pluginService;
		}

		[Route("Reports/Build")]
		[HttpPost]
		public IActionResult Build([FromBody] string Params)
		{
			int id = reportService.Build(Params);

			return Ok(id);
		}

		[Route("Reports/Stop")]
		[HttpPost]
		public IActionResult Stop([FromQuery] int id)
		{
			reportService.Stop(id);

			return Ok(id);
		}

		[Route("Plugins/GetPlugins")]
		[HttpGet]
		public IActionResult GetPlugins()
		{
			List<IPluginMethodInfo> plugins = pluginService.GetPlugins();
			PluginsDTO pluginsDTO = new PluginsDTO();
			pluginsDTO.Plugins = ClassDTO.GetPluginsDTOs(plugins);
			return Ok(JsonConvert.SerializeObject(pluginsDTO));
		}

		[Route("Plugins/GetSimplePlugins")]
		[HttpGet]
		public IActionResult GetSimplePlugins()
		{
			List<IPluginMethodInfo> simplePlugins = pluginService.GetSimplePlugins();
			PluginsDTO pluginsDTO = new PluginsDTO();
			pluginsDTO.Plugins = ClassDTO.GetPluginsDTOs(simplePlugins);
			return Ok(JsonConvert.SerializeObject(pluginsDTO));
		}

		[Route("Plugins/ExecSimplePlugin")]
		[HttpPost]
		public IActionResult ExecSimplePlugin([FromBody] string method)
		{
			SimplePluginDTO simplePluginDTO = new SimplePluginDTO();
			simplePluginDTO.Result = pluginService.ExecSimplePlugin(method);
			return Ok(JsonConvert.SerializeObject(simplePluginDTO));
		}
	}
}
