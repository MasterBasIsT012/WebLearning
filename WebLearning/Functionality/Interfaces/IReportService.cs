﻿using WebLearning.Data;

namespace WebLearning.Interfaces
{
	public interface IReportService
	{
		int Build(string Params);
		void Dispose();
		void KillAllTasks();
		void Stop(int id);
	}
}