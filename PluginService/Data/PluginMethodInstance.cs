using Infrastructure.Interfaces;
using System;
using System.Reflection;

namespace PluginService.Data
{
	public class PluginMethodInstance : IPluginMethodInstance
	{
		public MethodInfo Method { get; set; }
		public object Instance { get; set; }

		public PluginMethodInstance(Type type, string MethodName)
		{
			Method = type.GetMethod(MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			Instance = Activator.CreateInstance(type);
		}
	}
}
