using System;

namespace ReportService.Data
{
	enum ReportStatus
	{
		inProgress, Done, Error, Timeout
	}
	public class ReportInfo
	{
		private readonly int RequestID;
		private readonly string[] Params;
		private readonly DateTime StartTime;
		private readonly DateTime EndTime;
		private readonly byte[] ReportResult;
		private readonly string ErrorNassage;
		private readonly ReportStatus Status;
	}
}
