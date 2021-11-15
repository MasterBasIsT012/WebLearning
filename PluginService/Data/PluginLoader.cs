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
		readonly Logger logger = LogManager.GetCurrentClassLogger();
		private readonly List<IPluginMethodInfo> pluginMethodsInfo = new List<IPluginMethodInfo>();
		private readonly List<IPluginMethodInstance> pluginMethodInstances = new List<IPluginMethodInstance>();

		public static string Path { get; set; }
		public static IPluginLoader instance = null;

		public static IPluginLoader Instance
		{
			get
			{
				if (instance == null)
					instance = new PluginLoader();
				return instance;
			}
		}

		public List<IPluginMethodInfo> PluginMethods { get => pluginMethodsInfo; }
		public List<IPluginMethodInstance> PluginMethodInstances { get => pluginMethodInstances; }

		public void LoadPlugins()
		{
			logger.Debug("LoadPlugins method started from PluginLoader");
			foreach (string file in Directory.GetFiles(Path))
				GetPluginMethods(file);
			logger.Debug("LoadPlugins methods finished");
		}
		private void GetPluginMethods(string filePath)
		{
			Assembly assembly = Assembly.LoadFrom(filePath);
			foreach (Type type in assembly.GetTypes())
				if (IsPluginType(type))
					GetAndSavePluginMethods(type);
		}
		private void GetAndSavePluginMethods(Type type)
		{
			foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
				if (IsPluginMethod(method))
					SavePluginMethod(type, method.Name);
		}
		private void SavePluginMethod(Type t, string methodName)
		{
			PluginMethodInfo pluginMethod = new PluginMethodInfo(t, methodName);
			PluginMethodInstance pluginMethodInstance = new PluginMethodInstance(t, methodName);
			pluginMethodsInfo.Add(pluginMethod);
			pluginMethodInstances.Add(pluginMethodInstance);
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
