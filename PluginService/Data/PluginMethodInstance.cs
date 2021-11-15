using Infrastructure.Interfaces;
using System;
using System.Reflection;

namespace PluginService.Data
{
	public class PluginMethodInstance : IPluginMethodInstance
	{
		public MethodInfo method { get; set; }
		public object Instance { get; set; }

		public PluginMethodInstance(Type type, string MethodName)
		{
			method = type.GetMethod(MethodName);
			Instance = Activator.CreateInstance(type);
		}
	}
}
