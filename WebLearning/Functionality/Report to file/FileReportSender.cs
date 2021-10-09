using Axelot_meeting_2.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace Axelot_meeting_2.FileReport
{
	public class FileReportSender : ISender
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private readonly string reportsDirecotoryName = "ProcessReports";
		private readonly string fileType = ".txt";
		private static string path;

		public FileReportSender()
		{
			path = GetDirectoryPath(reportsDirecotoryName);
		}
		private string GetDirectoryPath(string directoryName)
		{
			DirectoryInfo dir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
			string path = string.Concat(dir.Parent.FullName, "\\", directoryName);
			return path;
		}

		public void Send(IReport report)
		{
			(string type, int id, byte[] data) reportInfo = (string.Empty, 0, null);

			try
			{
				reportInfo = report.GetInfo();
				string path = GenPath(reportInfo.type, reportInfo.id);
				File.WriteAllBytes(path, reportInfo.data);
			}
			catch (Exception e)
			{
				logger.Error(e, $"Report {reportInfo.id}: send didn't complete");
			}
		}
		private string GenPath(string type, int id)
		{
			return string.Concat(path, "\\", type, "_", id.ToString(), fileType);
		}
	}
}