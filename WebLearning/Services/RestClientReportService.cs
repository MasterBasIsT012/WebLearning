using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;
using System.IO;
using System.Reflection;

namespace WebLearning.Services
{
	public class RestClientReportService : IReportService
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly string configureLine = "RestClientReporService:URL";
		private readonly RestClient restClient;
		private readonly string reportsRoute = "api/Report/";
		private readonly string reportsDirectoryName = "ProcessReports";
		private readonly string setPath = "SetPath";
		private readonly string build = "Build";
		private readonly string stop = "Stop";

		public RestClientReportService(IConfiguration configuration)
		{
			try
			{
				logger.Info("Reports directory path setting on ReportService started");
				string restClientURL = configuration.GetValue<string>(configureLine);
				restClient = new RestClient(restClientURL);
				RestRequest restRequest = new RestRequest(GetReportsMethodRoute(setPath));
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

		public int Build(string Params)
		{
			logger.Debug("Build method started from RestClientReprtService");
			BuildDTO buildDTO = Post<BuildDTO>(build, Params);
			int id = buildDTO.Id;
			logger.Info($"Build method finished");

			return id;
		}
		public void Stop(int id)
		{
			logger.Debug("Stop method started from RestClientReprtService");
			Post<string>(stop, id.ToString());
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

		private T Post<T>(string method, string body)
		{
			RestRequest restRequest = GetReportsRequest(method);
			restRequest.AddJsonBody(body);

			string content = restClient.Post(restRequest).Content;
			content = NormalizeToJson(content);

			T DTO = JsonConvert.DeserializeObject<T>(content);

			return DTO;
		}

		private RestRequest GetReportsRequest(string methodName)
		{
			RestRequest restRequest = new RestRequest(GetReportsMethodRoute(methodName));
			return restRequest;
		}
		private string GetReportsMethodRoute(string action)
		{
			return string.Concat(reportsRoute, action);
		}
		private string NormalizeToJson(string content)
		{
			content = content.Replace("\\", "");
			content = content.Trim('\\', '\"');
			return content;
		}
	}
}
