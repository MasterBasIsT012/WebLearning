using Axelot_meeting_2.Interfaces;
using Axelot_meeting_2.Reports;

namespace Axelot_meeting_2.FileReport
{
	public class FileReportsFactory : IReportsFactory
	{
		public IReport GetErrorReport(int id, byte[] data)
		{
			ErrorReport report = new ErrorReport();
			report.Build(id, data);
			return report;
		}

		public IReport GetSuccessReport(int id, byte[] data)
		{
			SuccessReport report = new SuccessReport();
			report.Build(id, data);
			return report;
		}

		public IReport GetTimeoutReport(int id, byte[] data)
		{
			TimeoutReport report = new TimeoutReport();
			report.Build(id, data);
			return report;
		}
	}
}
