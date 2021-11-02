using System;
using System.Reflection;

namespace Infrastructure.Interfaces
{
	public interface IPluginMethodInfo
	{
		public string ClassName { get; }
		public ParameterInfo[] Arguments { get; }
		public object Instance { get; }
		public MethodInfo Method { get; }
		public string MethodName { get; }
		public Type ReturnType { get; }
	}
}