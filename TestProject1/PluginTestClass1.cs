using Plugins;

namespace TestProject1
{
	[Plugin(Version = "1.0.0.2")]
	class PluginTestClass1
	{
		[PluginMethod]
		(int, string) PluginTestMethod(int id, string name)
		{
			return (id, name);
		}

		[PluginMethod]
		double PluginTestMethod1()
		{
			return 1.1;
		}

		char TestMethod(int a)
		{
			return (char)a;
		}

		[PluginMethod]
		string PluginSimpleTestMethod(string str)
		{
			return str;
		}
	}
}
