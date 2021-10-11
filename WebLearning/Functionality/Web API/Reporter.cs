using WebLearning.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebLearning.WebAPI
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