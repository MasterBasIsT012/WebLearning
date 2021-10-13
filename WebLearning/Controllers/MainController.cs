using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace WebLearning.Controllers
{
	[Route("api/Main")]
	public class MainController : Controller
	{
		[HttpPost]
		public IActionResult Build(string Params)
		{
			RestClient restClient = new RestClient("http://localhost:5000/api/Reports");
			RestRequest restRequest = new RestRequest("/Build");
			restRequest.AddJsonBody(Params);
			restClient.Post(restRequest);
			return Ok();
		}
	}
}
