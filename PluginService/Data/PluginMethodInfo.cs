using Infrastructure.Interfaces;
using Plugins;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PluginService.Data
{
	public class PluginMethodInfo : IPluginMethodInfo
	{
		public string ClassName { get; set; }
		public string Vers { get; set; }
		public string MethodName { get; set; }
		public string ReturnType { get; set; }
		public List<string> Arguments { get; set; }

		public PluginMethodInfo() { }
		public PluginMethodInfo(Type type, string methodName)
		{
			ClassName = type.Name;
			Plugin pluginAttribute = (Plugin)type.GetCustomAttribute(typeof(Plugin));
			Vers = pluginAttribute.Version;
			MethodName = methodName;
			MethodInfo Method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			ReturnType = Method.ReturnType.Name;
			ParameterInfo[] parameters = Method.GetParameters();
			Arguments = GetParametersTypes(parameters);
		}
		private List<string> GetParametersTypes(ParameterInfo[] parameters)
		{
			List<string> arguments = new List<string>();

			foreach (ParameterInfo parameter in parameters)
				arguments.Add(parameter.ParameterType.Name);

			return arguments;
		}
	}
}