using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
	public interface IPluginMethodInfo
	{
		public string ClassName { get; set; }
		public string Vers { get; set; }
		public string MethodName { get; set; }
		public string ReturnType { get; set; }
		public List<string> Arguments { get; set; }
	}
}