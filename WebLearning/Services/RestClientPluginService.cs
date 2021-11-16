using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Newtonsoft.Json;
using NLog;
using PluginService.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WebLearning.Services
{
	public class RestClientPluginService : IPluginService
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly RestClient restClient = new RestClient("https://localhost:5002");
		private readonly string pluginsRoute = "api/Plugin/";
		private readonly string pluginsDirectoryName = "Plugins";
		private readonly string getPlugins = "GetPlugins";
		private readonly string getSimplePlugins = "GetSimplePlugins";
		private readonly string execSimplePlugin = "ExecSimplePlugin";

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


		public List<IPluginMethodInfo> GetPlugins()
		{
			RestRequest restRequest = GetPluginRequest(getPlugins);
			string content = restClient.Get(restRequest).Content;
			content = content.Replace("\\", "");
			content = content.Trim('\\', '\"');
			PluginsDTO pluginsDTO = JsonConvert.DeserializeObject<PluginsDTO>(content);
			List<IPluginMethodInfo> pluginMethodInfos = GetPluginMethodInfos(pluginsDTO);
			return pluginMethodInfos;
		}
		private List<IPluginMethodInfo> GetPluginMethodInfos(PluginsDTO pluginsDTO)
		{
			List<IPluginMethodInfo> pluginMethodInfos = new List<IPluginMethodInfo>();

			foreach (ClassDTO classDTO in pluginsDTO.Plugins)
			{
				foreach (MethodInfoDTO methodInfoDTO in classDTO.Methods)
				{
					PluginMethodInfo pluginMethodInfo = new PluginMethodInfo()
					{
						ClassName = classDTO.Name,
						Vers = classDTO.Vers,
						MethodName = methodInfoDTO.Name,
						ReturnType = methodInfoDTO.ReturnTypeName,
						Arguments = methodInfoDTO.Args
					};

					pluginMethodInfos.Add(pluginMethodInfo);
				}
			}

			return pluginMethodInfos;
		}

		public List<IPluginMethodInfo> GetSimplePlugins()
		{
			RestRequest restRequest = GetPluginRequest(getSimplePlugins);
			string content = restClient.Get(restRequest).Content;
			content = content.Replace("\\", "");
			content = content.Trim('\\', '\"');
			PluginsDTO pluginsDTO = JsonConvert.DeserializeObject<PluginsDTO>(content);
			List<IPluginMethodInfo> pluginMethodInfos = GetPluginMethodInfos(pluginsDTO);
			return pluginMethodInfos;
		}

		public string ExecSimplePlugin(string method)
		{
			RestRequest restRequest = GetPluginRequest(execSimplePlugin);
			restRequest.AddJsonBody(method);
			string content = restClient.Post(restRequest).Content;
			content = content.Replace("\\", "");
			content = content.Trim('\\', '\"');
			SimplePluginDTO simplePluginDTO = JsonConvert.DeserializeObject<SimplePluginDTO>(content);
			return simplePluginDTO.Result;
		}
		
		public void LoadPlugins(IPluginLoader loader)
		{
			throw new NotImplementedException();
		}
	}
}