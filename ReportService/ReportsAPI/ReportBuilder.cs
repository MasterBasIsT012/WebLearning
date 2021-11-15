using Infrastructure.Interfaces;
using ReportService.Data;
using System;
using System.Text;
using System.Threading;

namespace ReportService.ReportsAPI
{
	public class ReportBuilder : IReportBuilder
	{
		public byte[] BuildReport(IReportInfo reportInfo, ref IReportInfoRepository reportInfoRepository, CancellationToken token)
		{
			try
			{
				reportInfo.StartTime = DateTime.Now;
				reportInfo.Status = (int)ReportStatus.inProgress;
				reportInfoRepository.AddReportInfo(reportInfo);
				TryCrash();
				Processing(token);

				reportInfo.EndTime = DateTime.Now;
				reportInfo.ReportResult = GetData(GetTimeOfProccessing(reportInfo), reportInfo.Params);
			}
			catch (Exception)
			{
				reportInfo.EndTime = DateTime.Now;
				reportInfo.Status = (int)ReportStatus.Error;
				reportInfoRepository.UpdateReportInfo(reportInfo.ID, reportInfo);
				return reportInfo.ReportResult;
			}

			reportInfo.Status = (int)ReportStatus.Done;
			return reportInfo.ReportResult;
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
		private int GetTimeOfProccessing(IReportInfo reportInfo)
		{
			return (int)(reportInfo.EndTime - reportInfo.StartTime).TotalSeconds;
		}
		private byte[] GetData(int time, string Params)
		{
			string message = string.Concat("Report ready at ", time.ToString(), " s.\n", Params);
			return Encoding.UTF8.GetBytes(message);
		}
	}
}