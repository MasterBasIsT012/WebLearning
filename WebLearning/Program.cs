using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.IO;
using System.Reflection;

namespace WebLearning
{
	public class Program
	{
		static readonly Logger logger = LogManager.GetCurrentClassLogger();
		static readonly string reportsDirecotoryName = "ProcessReports";

		public static void Main(string[] args)
		{
			try
			{
				Initialization();
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				logger.Fatal("App initialization failled", ex);
			}
		}

		private static void Initialization()
		{
			logger.Info("Initialization started");
			CreateAndClearDirectory(reportsDirecotoryName);
			logger.Info("Initialization finished");
		}
		static void CreateAndClearDirectory(string directoryName)
		{
			logger.Info($"Directory {directoryName} installation started");
			string path = GetDirectoryPath(directoryName);

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			else
				ClearDirectory(path);

			logger.Info($"Directory {directoryName} installation finished");
		}
		static string GetDirectoryPath(string directoryName)
		{
			DirectoryInfo dir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
			string path = string.Concat(dir.Parent.FullName, "\\", directoryName);
			return path;
		}
		static void ClearDirectory(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			foreach (FileInfo file in directoryInfo.GetFiles())
				file.Delete();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			logger.Info("Server initialization started");
			var host = Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
			logger.Info("Server initialization finished");

			return host;
		}
	}
}
