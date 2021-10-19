using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PluginService.Controllers
{
	public class PluginController : Controller
	{
		private readonly IPluginService pluginService;

		public PluginController(IPluginService pluginService)
		{
			this.pluginService = pluginService;
		}

		public IActionResult GetPlugins()
		{
			return Ok();
		}
	}
}
