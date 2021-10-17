using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace WebLearning.Controllers
{
	[Route("api/Reports/{action}")]
	public class MainController : Controller
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly IReportService reportService;

		public MainController([FromServices] IReportService reportService)
		{
			this.reportService = reportService;
		}

		[HttpPost]
		public IActionResult Build([FromBody]string Params)
		{
			int id = reportService.Build(Params);

			return Ok(id);
		}

		[HttpGet]
		public IActionResult Stop([FromBody]int id)
		{
			reportService.Stop(id);

			return Ok(id);
		}
	}
}
