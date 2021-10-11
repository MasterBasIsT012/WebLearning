namespace WebLearning.Interfaces
{
	public interface IReport
	{
		void Build(int id, byte[] data);
		void Send(ISender sender);
		(string, int, byte[]) GetInfo();
	}
}