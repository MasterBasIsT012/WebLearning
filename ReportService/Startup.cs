using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReportService.FileReport;
using ReportService.Services;
using ReportService.WebAPI;

namespace ReportService
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddSingleton<IReportService, ReportsService>();
			services.AddTransient<IReportBuilder, ReportBuilder>();
			services.AddTransient<IReportsFactory, FileReportsFactory>();
			services.AddTransient<IReporter, Reporter>();
			services.AddTransient<ISender, FileReportSender>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseEndpoints(endpoints =>
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action}/{id?}"));
		}
	}
}
