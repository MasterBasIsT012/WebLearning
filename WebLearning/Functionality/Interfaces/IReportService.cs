namespace Axelot_meeting_2.Interfaces
{
	public interface IReportService
	{
		int Build();
		void Dispose();
		void KillAllTasks();
		void Stop(int id);
	}
}