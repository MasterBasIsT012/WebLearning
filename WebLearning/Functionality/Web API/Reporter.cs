using Axelot_meeting_2.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Axelot_meeting_2.WebAPI
{
	public class Reporter : IReporter
	{
		ISender sender;

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