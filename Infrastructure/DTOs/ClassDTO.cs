using System.Collections.Generic;

namespace Infrastructure.DTOs
{
	public class ClassDTO
	{
		public string Name { get; set; }
		public string Vers { get; set; }
		public List<MethodInfoDTO> Methods { get; set; }
	}
}
