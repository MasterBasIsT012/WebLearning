using Infrastructure.Interfaces;
using System;

namespace ReportService.Data
{
	public class ReportInfo : IReportInfo
	{
		public int ID { get; set; }
		public string Params { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public byte[] ReportResult { get; set; }
		public string ErrorMassage { get; set; }
		public int Status { get; set; }
	}
}
