using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebLearning.Controllers
{
	public class ReportController : Controller
	{
		private readonly IReportService reportService;

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
