using Infrastructure.Interfaces;
using RestSharp;
using NLog;

namespace WebLearning.Services
{
	public class RestClientReportService : IReportService
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly RestClient restClient = new RestClient("https://localhost:5001");

		public RestClientReportService()
		{
			//создать RestClient
			//URL достать из appsettings используется ConfigurationBuilder
		}

		public int Build(string Params)
		{
			logger.Debug("Build method started from RestClientReprtService");
			RestRequest restRequest = new RestRequest("Report/Build");
			restRequest.AddJsonBody(Params);
			int id = int.Parse(restClient.Get(restRequest).Content);
			logger.Info($"Build method finished");

			return id;
		}
		
		public void Stop(int id)
		{
			logger.Debug("Stop method started from RestClientReprtService");
			RestRequest restRequest = new RestRequest("Report/Stop");
			restRequest.AddJsonBody(id.ToString());
			logger.Info($"Report {id}: report stopped");
		}

		public void Dispose()
		{
			throw new System.NotImplementedException();
		}

		public void KillAllTasks()
		{
			throw new System.NotImplementedException();
		}

	}
}
