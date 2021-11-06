using Infrastructure.Interfaces;
using System;
using System.Reflection;

namespace PluginService.Data
{
	public class PluginMethodInfo : IPluginMethodInfo
	{
		public string ClassName { get; set; }
		public string MethodName { get; set; }
		public object Instance { get; set; }
		public MethodInfo Method { get; set; }
		public Type ReturnType { get; set; }
		public ParameterInfo[] Arguments { get; set; }

		public PluginMethodInfo() { }

		public PluginMethodInfo(Type t, string methodName)
		{
			ClassName = t.Name;
			MethodName = methodName;
			Instance = Activator.CreateInstance(t);
			Method = t.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			ReturnType = Method.ReturnType;
			Arguments = Method.GetParameters();
		}
	}
}