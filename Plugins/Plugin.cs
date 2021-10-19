using System;

namespace Plugins
{
	[AttributeUsage(AttributeTargets.Class)]
	public class Plugin: Attribute
	{
		public string Version;
	}
}
