using Infrastructure.Interfaces;
using System;
using System.Text;
using System.Threading;

namespace ReportService.WebAPI
{
	public class ReportBuilder : IReportBuilder
	{
		public byte[] BuildReport(string Params, CancellationToken token)
		{
			byte[] reportData = null;
			int time;

			try
			{
				TryCrash();

				time = Processing(token);

				reportData = GetData(time, Params);
			}
			catch (Exception)
			{
				return reportData;
			}

			return reportData;
		}
		private void TryCrash()
		{
			int coin = new Random().Next(0, 101);

			if (coin < 20)
				throw new Exception("Report error");
		}
		private int Processing(CancellationToken token)
		{
			int time = new Random().Next(5, 46);

			for (int i = 0; i < time; i++)
			{
				token.ThrowIfCancellationRequested();
				Thread.Sleep(1000);
			}

			return time;
		}
		private byte[] GetData(int time, string Params)
		{
			string message = string.Concat("Report ready at ", time.ToString(), " s.\n", Params);
			return Encoding.UTF8.GetBytes(message);
		}
	}
}