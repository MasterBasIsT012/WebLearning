using System;

namespace WebLearning.Data
{
	enum ReportStatus
	{
		inProgress, Done, Error, Timeout
	}
	public class ReportInfo
	{
		private int RequestID;
		private string[] Params;
		private DateTime StartTime;
		private DateTime EndTime;
		private byte[] ReportResult;
		private string ErrorNassage;
		private ReportStatus Status;
	}
}
