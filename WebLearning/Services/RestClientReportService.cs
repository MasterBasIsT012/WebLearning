using Infrastructure.Interfaces;
using RestSharp;
using NLog;
using System.IO;
using System.Reflection;
using System;

namespace WebLearning.Services
{
	public class RestClientReportService : IReportService
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly RestClient restClient = new RestClient("https://localhost:5001");
		private readonly string reportsRoute = "api/Report/";
		private readonly string reportsDirectoryName = "ProcessReports";

		public RestClientReportService()
		{
			try
			{
				logger.Info("Reports directory path setting on ReportService started");
				RestRequest restRequest = new RestRequest(GetReportsMethodRoute("SetPath"));
				restRequest.AddJsonBody(GetDirectoryPath(reportsDirectoryName));
				restClient.Post(restRequest);
				logger.Info("Reports directory path setting on ReportService finished");
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}
		}
		private string GetDirectoryPath(string directoryName)
		{
			DirectoryInfo dir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
			string path = string.Concat(dir.Parent.FullName, "\\", directoryName);
			return path;
		}
		private string GetReportsMethodRoute(string action)
		{
			return string.Concat(reportsRoute, action);
		}

		public int Build(string Params)
		{
			logger.Debug("Build method started from RestClientReprtService");
			RestRequest restRequest = new RestRequest(GetReportsMethodRoute("Build"));
			restRequest.AddJsonBody(Params);
			int.TryParse(restClient.Post(restRequest).Content, out int id);
			logger.Info($"Build method finished");

			return id;
		}

		public void Stop(int id)
		{
			logger.Debug("Stop method started from RestClientReprtService");
			RestRequest restRequest = new RestRequest(GetReportsMethodRoute("Stop"));
			restRequest.AddJsonBody(id.ToString());
			restClient.Post(restRequest);
			logger.Info($"Report {id}: report stopped");
		}

		public void KillAllTasks()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
