using WebLearning.Data;

namespace WebLearning.Interfaces
{
	public interface IStorageReportInfoRepository
	{
		ReportInfo CreateReportInfo(ReportInfo reportInfo);
		ReportInfo DeleteReportInfo(int id);
		ReportInfo GetReportInfo(int id);
		ReportInfo UpdateReportInfo(int id);
	}
}