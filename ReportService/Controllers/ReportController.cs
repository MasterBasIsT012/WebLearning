using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebLearning.Controllers
{
	[Route("api/Reports")]
	public class ReportController : Controller
	{
		private IReportService reportService;

		public ReportController(IReportService reportService)
		{
			this.reportService = reportService;
		}

		[HttpGet]
		public IActionResult Build([FromBody]string Params)
		{
			int buildTaskId = reportService.Build(Params);

			return Ok(buildTaskId);
		}
	}
}
