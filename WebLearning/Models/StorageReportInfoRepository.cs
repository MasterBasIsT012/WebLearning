using System.Collections.Generic;
using WebLearning.Data;
using WebLearning.Interfaces;

namespace WebLearning.Models
{
	public class StorageReportInfoRepository : IStorageReportInfoRepository

	{
		static int Id = 0;
		Dictionary<int, ReportInfo> reports = new Dictionary<int, ReportInfo>();

		public ReportInfo CreateReportInfo(ReportInfo reportInfo)
		{
			reports.Add(Id++, reportInfo);
			return null;
		}

		public ReportInfo DeleteReportInfo(int id)
		{
			reports.Remove(id);
			return null;
		}

		public ReportInfo GetReportInfo(int id)
		{
			ReportInfo reportInfo;
			if (!reports.TryGetValue(id, out reportInfo))
				return null;
			return reportInfo;
		}

		public ReportInfo UpdateReportInfo(int id)
		{
			return null;
		}
	}
}
