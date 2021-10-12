using System.Collections.Generic;
using WebLearning.Data;
using WebLearning.Interfaces;

namespace WebLearning.Data
{
	public class ReportInfoRepository : IReportInfoRepository
	{
		static int Id = 0;
		Dictionary<int, ReportInfo> reports = new Dictionary<int, ReportInfo>();

		public void CreateReportInfo(ReportInfo reportInfo)
		{
			reports.Add(Id++, reportInfo);
		}

		public void DeleteReportInfo(int id)
		{
			reports.Remove(id);
		}

		public ReportInfo GetReportInfo(int id)
		{
			ReportInfo reportInfo;
			if (!reports.TryGetValue(id, out reportInfo))
				return null;
			return reportInfo;
		}

		public void UpdateReportInfo(int id)
		{
			return;
		}
	}
}
