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

		public MainController([FromServices] IReportService reportService, [FromServices] IPluginService pluginService)
		{
			this.reportService = reportService;
			this.pluginService = pluginService;
		}

		[Route("Reports/Build")]
		[HttpPost]
		public IActionResult Build([FromBody]string Params)
		{
			int id = reportService.Build(Params);

			return Ok(id);
		}

		[Route("Reports/Stop")]
		[HttpPost]
		public IActionResult Stop([FromQuery]int id)
		{
			reportService.Stop(id);

			return Ok(id);
		}

		[Route("Plugins/GetPlugins")]
		[HttpGet]
		public IActionResult GetPlugins()
		{
			List<ClassDTO> plugins = pluginService.GetPluginsDTOs();
			return Ok(JsonConvert.SerializeObject(plugins));
		}
	}
}
