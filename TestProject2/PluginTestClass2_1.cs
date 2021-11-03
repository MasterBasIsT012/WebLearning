using Plugins;

namespace TestProject2
{
	[Plugin(Version = "1.0.0.3.1")]
	public class PluginTestClass2_1
	{
		[PluginMethod]
		static PluginTestClass2_1 GetInstance()
		{
			return new PluginTestClass2_1();
		}
	}
}
