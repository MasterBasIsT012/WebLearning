using System.Threading;

namespace Infrastructure.Interfaces
{
	public interface IReportBuilder
	{
		byte[] BuildReport(string Params, CancellationToken token);
	}
}