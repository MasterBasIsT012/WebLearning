using System;
using System.Reflection;

namespace Infrastructure.Interfaces
{
	public interface IPluginMethodInfo
	{
		string ClassName { get; }
		ParameterInfo[] Arguments { get; }
		object Instance { get; }
		MethodInfo Method { get; }
		string MethodName { get; }
		Type ReturnType { get; }
	}
}