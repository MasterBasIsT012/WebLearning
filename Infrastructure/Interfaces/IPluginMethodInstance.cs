using System.Reflection;

namespace Infrastructure.Interfaces
{
	public interface IPluginMethodInstance
	{
		object Instance { get; set; }
		MethodInfo method { get; set; }
	}
}