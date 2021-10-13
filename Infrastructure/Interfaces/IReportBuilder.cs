using System.Threading;

namespace Infrastructure.Interfaces
{
	public interface IReportBuilder
	{
		byte[] BuildReport(CancellationToken token);
	}
}