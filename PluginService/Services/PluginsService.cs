using Infrastructure.Interfaces;
using NLog;
using PluginService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PluginService.Services
{
	public class PluginsService : IPluginService
	{
		static readonly Logger logger = LogManager.GetCurrentClassLogger();
		private List<IPluginMethodInfo> pluginMethods = new List<IPluginMethodInfo>();
		private List<IPluginMethodInstance> pluginMethodInstances = new List<IPluginMethodInstance>();

		public void LoadPlugins(IPluginLoader loader)
		{
			logger.Info("Plugins loading started");
			loader.LoadPlugins();
			pluginMethods = loader.PluginMethods;
			pluginMethodInstances = loader.PluginMethodInstances;
			pluginMethods.OrderBy(method => method.ClassName);
			logger.Info("Plugins loading finished");
		}

		public List<IPluginMethodInfo> GetPlugins()
		{
			return pluginMethods;
		}

		public List<IPluginMethodInfo> GetSimplePlugins()
		{
			List<IPluginMethodInfo> methods =
				pluginMethods.
				Where(method => method.ReturnType == typeof(string).Name).
				Where(method => method.Arguments.Count == 1 && method.Arguments[0] == typeof(string).Name).ToList();
			return methods;
		}

		public string ExecSimplePlugin(string method)
		{
			string output = string.Empty;
			(string className, string methodName, string methodArgument) = GetMethodInfo(method);

			IPluginMethodInfo pluginMethod =
				GetSimplePlugins().
				Where(method => method.ClassName == className).
				FirstOrDefault();

			if (null != pluginMethod)
			{
				string[] args = { methodArgument };
				output = InvokeSimpleMethod(pluginMethod, args);
			}
			else
			{
				logger.Warn($"Method {methodName} wasn't found");
				Console.WriteLine("Method doesn't exist");
			}

			return output;
		}
		private string InvokeSimpleMethod(IPluginMethodInfo pluginMethod, string[] args)
		{
			foreach (IPluginMethodInstance methodInstance in pluginMethodInstances)
				if (string.Equals(methodInstance.method.Name, pluginMethod.MethodName))
					return (string)methodInstance.method.Invoke(methodInstance.Instance, args);

			return string.Empty;
		}

		private (string, string, string) GetMethodInfo(string method)
		{
			Regex
				regexClassName = new Regex(@".*\."),
				regexName = new Regex(@"\..*\("),
				regexMethod = new Regex(@"\(.*\)");
			string className = null, methodName = null, methodArgument = null;

			if (regexClassName.IsMatch(method))
			{
				className = regexClassName.Match(method).Value;
				className = className[0..^1];
			}
			if (regexName.IsMatch(method))
			{
				methodName = regexName.Match(method).Value;
				methodName = methodName[1..^1];
			}

			methodArgument = regexMethod.Match(method).Value;
			if (methodArgument.Length != 0)
				methodArgument = methodArgument[2..^2];

			return (className, methodName, methodArgument);
		}
	}
}
