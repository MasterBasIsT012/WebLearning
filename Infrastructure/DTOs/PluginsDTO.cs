using System.Collections.Generic;

namespace Infrastructure.DTOs
{
	public class PluginsDTO
	{
		public List<ClassDTO> Plugins { get; set; }

		public PluginsDTO()
		{
			Plugins = new List<ClassDTO>();
		}
	}
}
