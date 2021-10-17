using Infrastructure.Interfaces;
using RestSharp;

namespace WebLearning.Services
{
	public class RestClientReportService : IReportService
	{
		private RestClient restClient = new RestClient("http://localhost:5002/api/Reports");

		public RestClientReportService()
		{
			//создать RestClient
			//URL достать из appsettings используется ConfigurationBuilder
		}

		public int Build(string Params)
		{
			RestRequest restRequest = new RestRequest("/Build");
			restRequest.AddJsonBody(Params);
			restClient.Post(restRequest);
			return 0;
		}

		public void Dispose()
		{
			throw new System.NotImplementedException();
		}

		public void KillAllTasks()
		{
			throw new System.NotImplementedException();
		}

		public void Stop(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}
