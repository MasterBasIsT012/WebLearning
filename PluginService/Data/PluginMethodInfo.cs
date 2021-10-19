using Infrastructure.Interfaces;
using System;
using System.Reflection;

namespace Data.Plugining
{
	public class PluginMethodInfo : IPluginMethodInfo
	{
		public string ClassName { get; }
		public string MethodName { get; }
		public object Instance { get; }
		public MethodInfo Method { get; }
		public Type ReturnType { get; }
		public ParameterInfo[] Arguments { get; }

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