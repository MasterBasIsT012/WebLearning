using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReportService.FileReport;

namespace ReportService.Controllers
{
	[Route("api/Report")]
	public class ReportController : Controller
	{
		private readonly IReportService reportService;

		public ReportController(IReportService reportService)
		{
			this.reportService = reportService;
		}

		[HttpPost]
		[Route("Build")]
		public IActionResult Build([FromBody] string Params)
		{
			int buildTaskId = reportService.Build(Params);
			BuildDTO buildDTO = new BuildDTO() { Id = buildTaskId };
			return Ok(JsonConvert.SerializeObject(buildDTO));
		}

		[HttpPost]
		[Route("Stop")]
		public IActionResult Stop([FromBody] string Id)
		{
			reportService.Stop(int.Parse(Id));
			return Ok();
		}

		[HttpPost]
		[Route("SetPath")]
		public IActionResult SetPath([FromBody] string path)
		{
			FileReportSender.Path = path;
			return Ok();
		}
	}
}
