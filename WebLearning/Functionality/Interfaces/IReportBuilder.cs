using System.Threading;

namespace Axelot_meeting_2.Interfaces
{
	public interface IReportBuilder
	{
		byte[] BuildReport(CancellationToken token);
	}
}