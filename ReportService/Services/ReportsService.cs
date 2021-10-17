using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReportService.Services
{
	public class ReportsService : IDisposable, IReportService
	{
		private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		private int Id = 0;
		private readonly int timeoutTime = 30000;
		private readonly Dictionary<int, CancellationTokenSource> cancellationTokenSources = new Dictionary<int, CancellationTokenSource>();
		private readonly IReportBuilder reportBuilder;
		private readonly IReportsFactory reportFactory;
		private readonly IReporter reporter;

		public ReportsService(IReportBuilder reportBuilder, IReportsFactory reportFactory, IReporter reporter)
		{
			logger.Info("Web installation started");
			this.reportBuilder = reportBuilder;
			this.reportFactory = reportFactory;
			this.reporter = reporter;
			logger.Info("Web installation finished");
		}

		public int Build(string Params)
		{
			logger.Debug("Build method started from Web");
			int id = Id++;

			BuildReport(id, Params);

			return id;
		}
		private async void BuildReport(int id, string Params)
		{
			byte[] reportData = null;

			CancellationToken token = CreateAndSaveCancellationToken(id);
			Task<byte[]> buildTask = Task.Factory.StartNew(() => reportData = reportBuilder.BuildReport(Params, token), token);
			Task taskTimeout = Task.Delay(timeoutTime);

			if (await Task.WhenAny(buildTask, taskTimeout) == buildTask)
			{
				if (reportData != null)
					reporter.Report(reportFactory.GetSuccessReport(id, reportData));
				else
				{
					reporter.Report(reportFactory.GetErrorReport(id, reportData));
					logger.Warn($"Report {id}: report error");
				}
			}
			else
			{
				reporter.Report(reportFactory.GetTimeoutReport(id, reportData));
				logger.Warn($"Report {id}: report timeout exception");
			}
		}
		private CancellationToken CreateAndSaveCancellationToken(int id)
		{
			CancellationTokenSource cts = new CancellationTokenSource();

			cancellationTokenSources.Add(id, cts);

			return cts.Token;
		}

		public void Stop(int id)
		{
			logger.Debug("Stop method started from Web");

			CancellationTokenSource cts;
			if (cancellationTokenSources.TryGetValue(id, out cts))
			{
				cts.Cancel();
				cts.Dispose();
				cancellationTokenSources.Remove(id);
			}

			logger.Info($"Report {id}: report stopped");
		}

		public void Dispose()
		{
			foreach (CancellationTokenSource cts in cancellationTokenSources.Values)
				cts.Dispose();
		}

		public void KillAllTasks()
		{
			foreach (CancellationTokenSource cts in cancellationTokenSources.Values)
			{
				cts.Cancel();
				cts.Dispose();
			}
		}
	}
}