using Infrastructure.Interfaces;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WebLearning.Services
{
	public class RestClientPluginService: IPluginService
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly RestClient restClient = new RestClient();
		private readonly string pluginsDirectoryName = "Plugins";
		private readonly string pluginsRoute = "api/Plugin";

		public RestClientPluginService()
		{
			try
			{
				logger.Info("Plugins directory path setting on ReportService started");
				RestRequest restRequest = new RestRequest(GetPluginsMethodRoute("SetPath"));
				restRequest.AddJsonBody(GetDirectoryPath(pluginsDirectoryName));
				restClient.Post(restRequest);
				logger.Info("Plugins directory path setting on ReportService finished");
			}
			catch (Exception ex)
			{
				logger.Error("Plugins directory path setting on ReportService crashed", ex);
			}
		}
		private string GetPluginsMethodRoute(string action)
		{
			return string.Concat(pluginsRoute, action);
		}
		private string GetDirectoryPath(string directoryName)
		{
			DirectoryInfo dir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
			string path = string.Concat(dir.Parent.FullName, "\\", directoryName);
			return path;
		}

		public void ExecSimplePlugin(string method)
		{
			throw new NotImplementedException();
		}

		public List<IPluginMethodInfo> GetPlugins()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<IPluginMethodInfo> GetSimplePlugins()
		{
			throw new NotImplementedException();
		}
	}
}
