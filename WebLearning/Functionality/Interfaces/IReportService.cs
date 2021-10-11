namespace WebLearning.Interfaces
{
	public interface IReportService
	{
		int Build();
		void Dispose();
		void KillAllTasks();
		void Stop(int id);
	}
}