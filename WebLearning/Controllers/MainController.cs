using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace WebLearning.Controllers
{
	public class MainController : Controller
	{
		private IReportService reportService;

		public MainController(IReportService reportService)
		{
			this.reportService = reportService;
		}

		//либо вызывает reportService из связей или дергает микросервис
		[HttpGet]
		public IActionResult Build([FromBody]string Params)
		{
			int id = reportService.Build(Params);
			return Ok(id);
		}
	}
}
