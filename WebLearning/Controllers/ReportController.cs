using Axelot_meeting_2.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace WebLearning.Controllers
{
	public class ReportController : Controller
	{
		IReportService reportService;

		public ReportController([FromServices] IReportService reportService)
		{
			this.reportService = reportService;
		}

		public IActionResult Build()
		{
			reportService.Build();
			return View();
		}

		public string Hello(string name)
		{
			return HtmlEncoder.Default.Encode($"Hello {name}");
		}
	}
}
