using System.Threading;

namespace WebLearning.Interfaces
{
	public interface IReportBuilder
	{
		byte[] BuildReport(CancellationToken token);
	}
}