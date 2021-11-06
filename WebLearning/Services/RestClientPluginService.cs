﻿using Infrastructure.Interfaces;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Infrastructure.DTOs;
using RestSharp.Serializers.NewtonsoftJson;

namespace WebLearning.Services
{
	public class RestClientPluginService: IPluginService
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly RestClient restClient = new RestClient("https://localhost:5002");
		private readonly string pluginsRoute = "api/Plugin/";
		private readonly string pluginsDirectoryName = "Plugins";

		public RestClientPluginService()
		{
			try
			{
				logger.Info("Plugins directory path setting on ReportService started");
				RestRequest restRequest = GetPluginRequest("LoadPlugins");
				restRequest.AddJsonBody(GetDirectoryPath(pluginsDirectoryName));
				restClient.Post(restRequest);
				logger.Info("Plugins directory path setting on ReportService finished");
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}
		}
		private RestRequest GetPluginRequest(string methodName)
		{
			RestRequest restRequest = new RestRequest(GetPluginsMethodRoute(methodName));
			return restRequest;
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
		
		public List<ClassDTO> GetPluginsDTOs()
		{
			RestRequest restRequest = GetPluginRequest("GetPlugins");
			string content = restClient.Get(restRequest).Content;
			content = content.Replace("\\", "");
			content = content.Trim('\\', '\"');
			return JsonConvert.DeserializeObject<List<ClassDTO>>(content);
		}

		public IEnumerable<IPluginMethodInfo> GetSimplePlugins()
		{
			throw new NotImplementedException();
		}
		
		public void LoadPlugins(IPluginLoader loader)
		{
			throw new NotImplementedException();
		}

	}
}