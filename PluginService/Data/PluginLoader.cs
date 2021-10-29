using Infrastructure.Interfaces;
using NLog;
using Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PluginService.Data
{
	public class PluginLoader : IPluginLoader
	{
		Logger logger = LogManager.GetCurrentClassLogger();
		private List<IPluginMethodInfo> pluginMethods = new List<IPluginMethodInfo>();
		private readonly string directoryName = "Plugins";
		private string path;

		public List<IPluginMethodInfo> PluginMethods { get => pluginMethods; }

		public PluginLoader()
		{
			path = GetDirectoryPath(directoryName);
		}
		private string GetDirectoryPath(string directoryName)
		{
			DirectoryInfo dir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
			string path = string.Concat(dir.Parent.FullName, "\\", directoryName);
			return path;
		}

		public void LoadPlugins()
		{
			logger.Debug("LoadPlugins method started from PluginLoader");
			foreach (string file in Directory.GetFiles(path))
				GetPluginMethodsFromAssembly(file);
			logger.Debug("LoadPlugins methods finished");
		}
		private void GetPluginMethodsFromAssembly(string filePath)
		{
			Assembly assembly = Assembly.LoadFrom(filePath);
			foreach (Type t in assembly.GetTypes())
				if (IsPluginType(t))
					GetPluginMethodsFromType(t);
		}
		private void GetPluginMethodsFromType(Type t)
		{
			foreach (MethodInfo method in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
				if (IsPluginMethod(method))
					SavePluginMethod(t, method.Name);
		}
		private void SavePluginMethod(Type t, string methodName)
		{
			PluginMethodInfo pluginMethod = new PluginMethodInfo(t, methodName);
			pluginMethods.Add(pluginMethod);
		}
		private bool IsPluginType(Type t)
		{
			Plugin pluginAttribute = (Plugin)t.GetCustomAttribute(typeof(Plugin));
			return null != pluginAttribute;
		}
		private bool IsPluginMethod(MethodInfo method)
		{
			PluginMethod pluginAttribute = (PluginMethod)method.GetCustomAttribute(typeof(PluginMethod));
			return null != pluginAttribute;
		}
	}
}
