using Microsoft.AspNetCore.Mvc;
using System;
using WebLearning.Data;
using WebLearning.Interfaces;

namespace WebLearning.Controllers
{
	public class ReportController : Controller
	{
		private IReportService reportService;

		public ReportController(IReportService reportService)
		{
			this.reportService = reportService;
		}

		[HttpGet]
		public IActionResult Menu()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Build(string Params)
		{
			if (Params is null)
			{
				throw new ArgumentNullException(nameof(Params));
			}

			ReportInfo reportInfo = null;

			return Ok(reportInfo);
		}
	}
}
