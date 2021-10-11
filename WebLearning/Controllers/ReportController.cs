using WebLearning.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace WebLearning.Controllers
{
	public class ReportController : Controller
	{
		IReportService reportService;

		public ReportController(IReportService reportService)
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
