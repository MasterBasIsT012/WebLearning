using Infrastructure.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace ReportService.FileReport
{
	public class FileReportSender : ISender
	{
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private readonly string reportsDirecotoryName = "ProcessReports";
		private readonly string fileType = ".txt";
		public static string Path { get; set; }

		public FileReportSender()
		{
			Path = GetDirectoryPath(reportsDirecotoryName);
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
			return string.Concat(Path, "\\", type, "_", id.ToString(), fileType);
		}
	}
}