using Plugins;

namespace TestProject
{
	public class TestClass
	{
		[PluginMethod]
		void PluginTestMethod(ref double a)
		{
			a *= 2;
		}

		protected int TestMethod()
		{
			return 0;
		}
	}
}
