using Infrastructure.Interfaces;

namespace ReportService.ReportsAPI
{
	public class Reporter : IReporter
	{
		private readonly ISender sender;

		public Reporter(ISender sender)
		{
			this.sender = sender;
		}

		public void Report(IReport report)
		{
			report.Send(sender);
		}
	}
}