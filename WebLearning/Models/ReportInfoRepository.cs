using System.Collections.Generic;
using WebLearning.Data;

namespace WebLearning.Models
{
	public class InMemoryReportInfoRepository

	{
		Dictionary<string, ReportInfo> _reports = new Dictionary<string, ReportInfo>();

		public ReportInfo CreateReportInfo(ReportInfo reportInfo)
		{
			return null;
		}

		public ReportInfo DeleteReportInfo(string RequestID)
		{
			return null;
		}

		public ReportInfo GetReportInfo(string RequestID)
		{
			return null;
		}

		public ReportInfo UpdateReportInfo(string RequestID)
		{
			return null;
		}
	}
}
