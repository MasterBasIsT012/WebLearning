using Axelot_meeting_2.Interfaces;

namespace Axelot_meeting_2.Reports
{
	public class SuccessReport : IReport
	{
		private string type = "Report";
		private int id;
		private byte[] data;

		public void Build(int id, byte[] data)
		{
			this.id = id;
			this.data = data;
		}

		public void Send(ISender sender)
		{
			sender.Send(this);
		}

		public (string, int, byte[]) GetInfo()
		{
			return (type, id, data);
		}
	}
}