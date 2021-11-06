using System.Collections.Generic;

namespace Infrastructure.DTOs
{
	public class MethodInfoDTO
	{
		public string Name { get; set; }
		public string ReturnTypeName { get; set; }
		public List<string> Args { get; set; }
	}
}
