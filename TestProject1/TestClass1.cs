using Plugins;

namespace TestProject1
{
	public class TestClass1
	{
		[PluginMethod]
		public string TestSimplePluginMethod(string str)
		{
			return "123";
		}

		(int, string) TestMethod()
		{
			return (0, "Andrey");
		}
	}
}
