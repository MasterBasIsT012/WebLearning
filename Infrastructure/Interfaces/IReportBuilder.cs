using System.Threading;

namespace Infrastructure.Interfaces
{
	public interface IReportBuilder
	{
		byte[] BuildReport(IReportInfo reportInfo, ref IReportInfoRepository reportInfoRepository, CancellationToken token);
	}
}