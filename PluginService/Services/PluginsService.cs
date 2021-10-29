using Infrastructure.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PluginService.Services
{
	public class PluginsService : IPluginService
	{
		Logger logger = LogManager.GetCurrentClassLogger();
		private static List<IPluginMethodInfo> pluginMethods = new List<IPluginMethodInfo>();

		public PluginsService(IPluginLoader loader)
		{
			logger.Info("Plugins loading started");
			loader.LoadPlugins();
			pluginMethods = loader.PluginMethods;
			pluginMethods.OrderBy(method => method.ClassName);
			logger.Info("Plugins loading finished");
		}

		public List<IPluginMethodInfo> GetPlugins()
		{
			return pluginMethods;
		}

		public IEnumerable<IPluginMethodInfo> GetSimplePlugins()
		{
			IEnumerable<IPluginMethodInfo> methods =
				pluginMethods.
				Where(method => method.ReturnType == typeof(string)).
				Where(method => method.Arguments.Length == 1 && method.Arguments[0].ParameterType == typeof(string));
			return methods;
		}

		public void ExecSimplePlugin(string method)
		{
			(string className, string methodName, string methodArgument) = GetMethodInfo(method);
			IPluginMethodInfo pluginMethod =
				GetSimplePlugins().
				Where(method => method.ClassName == className).
				FirstOrDefault();

			if (null != pluginMethod)
			{
				InvokeMethod(pluginMethod, methodArgument);
			}
			else
			{
				logger.Warn($"Method {methodName} wasn't found");
				Console.WriteLine("Method doesn't exist");
			}
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

		private void InvokeMethod(IPluginMethodInfo pluginMethod, string methodArgument)
		{
			MethodInfo method = pluginMethod.Method;
			object[] args = { methodArgument };
			string output = (string)method.Invoke(pluginMethod.Instance, args);
			Console.WriteLine(output);
		}
	}
}
