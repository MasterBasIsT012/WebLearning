using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace WebLearning.Controllers
{
	public class MainController : Controller
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly IReportService reportService;

		public MainController([FromServices] IReportService reportService)
		{
			this.reportService = reportService;
		}

		[HttpGet]
		public IActionResult Build([FromBody] string Params)
		{
			logger.Debug("Build method started");
			int id = reportService.Build(Params);
			logger.Debug("Build method finished");
			return Ok(id);
		}
	}
}
