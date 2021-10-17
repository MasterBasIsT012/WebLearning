namespace Infrastructure.Interfaces
{
	public interface IReportInfoRepository
	{
		void AddReportInfo(IReportInfo reportInfo);
		void DeleteReportInfo(int id);
		IReportInfo GetReportInfo(int id);
		void UpdateReportInfo(int id, IReportInfo reportInfo);
	}
}