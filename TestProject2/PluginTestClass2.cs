using Plugins;

namespace TestProject2
{
	[Plugin(Version = "1.0.0.3")]
	public class PluginTestClass2
	{
		[PluginMethod]
		public string SimplePluginMethod(string str)
		{
			return str;
		}

		[PluginMethod]
		PluginTestClass2 GetInstance()
		{
			return new PluginTestClass2();
		}
	}
}
