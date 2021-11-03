using Plugins;

namespace TestProject
{
	[Plugin(Version = "1.0.0.1")]
	class TestPluginClass
	{
		[PluginMethod]
		public int TestPluginMethod(int a, int b)
		{
			return a + b;
		}

		public string TestMethod(string text)
		{
			return text;
		}

		[PluginMethod]
		public bool TestPluginMethod1()
		{
			return true;
		}

		[PluginMethod]
		string PluginSimpleTestMethod(string text)
		{
			return text;
		}
	}
}
