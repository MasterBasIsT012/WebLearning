using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace ReportService.Data
{
	public class ReportInfoRepository : IReportInfoRepository
	{
		static int Id = 0;
		Dictionary<int, IReportInfo> reports = new Dictionary<int, IReportInfo>();

		public void CreateReportInfo(IReportInfo reportInfo)
		{
			reports.Add(Id++, reportInfo);
		}

		public void DeleteReportInfo(int id)
		{
			reports.Remove(id);
		}

		public IReportInfo GetReportInfo(int id)
		{
			IReportInfo reportInfo;
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
