using System;
using System.Reflection;

namespace Infrastructure.Interfaces
{
	public interface IPluginMethodInfo
	{
		public string ClassName { get; set; }
		public ParameterInfo[] Arguments { get; set; }
		public object Instance { get; set; }
		public MethodInfo Method { get; set; }
		public string MethodName { get; set; }
		public Type ReturnType { get; set; }
	}
}