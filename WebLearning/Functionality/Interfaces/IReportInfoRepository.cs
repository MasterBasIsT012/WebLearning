using WebLearning.Data;

namespace WebLearning.Interfaces
{
	public interface IReportInfoRepository
	{
		void CreateReportInfo(ReportInfo reportInfo);
		void DeleteReportInfo(int id);
		ReportInfo GetReportInfo(int id);
		void UpdateReportInfo(int id);
	}
}