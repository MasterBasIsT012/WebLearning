using Infrastructure.Interfaces;
using System.Text;

namespace ReportService.Reports
{
	public class TimeoutReport : IReport
	{
		private readonly string type = "Timeout";
		private int id;
		private byte[] data;

		public void Build(int id, byte[] data)
		{
			this.id = id;
			SetData();
		}
		private void SetData()
		{
			data = Encoding.UTF8.GetBytes("Report error");
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