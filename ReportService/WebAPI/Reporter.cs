using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.WebAPI
{
	public class Reporter : IReporter
	{
		private readonly ISender sender;

		public Reporter([FromServices] ISender sender)
		{
			this.sender = sender;
		}

		public void Report(IReport report)
		{
			report.Send(sender);
		}
	}
}