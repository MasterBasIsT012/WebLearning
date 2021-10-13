namespace Infrastructure.Interfaces
{
	public interface IReportsFactory
	{
		IReport GetErrorReport(int id, byte[] data);
		IReport GetSuccessReport(int id, byte[] data);
		IReport GetTimeoutReport(int id, byte[] data);
	}
}