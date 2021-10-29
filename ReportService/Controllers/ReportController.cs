using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ReportService.FileReport;

namespace ReportService.Controllers
{
	[Route("api/Report/{action}")]
	public class ReportController : Controller
	{
		private readonly IReportService reportService;

		public ReportController(IReportService reportService)
		{
			this.reportService = reportService;
		}

		[HttpPost]
		public IActionResult Build([FromBody]string Params)
		{
			int buildTaskId = reportService.Build(Params);

			return Ok(buildTaskId);
		}

		[HttpPost]
		public IActionResult SetPath([FromBody]string path)
		{
			FileReportSender.Path = path;

			return Ok();
		}
	}
}
