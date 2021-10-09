using Axelot_meeting_2.FileReport;
using Axelot_meeting_2.Interfaces;
using Axelot_meeting_2.WebAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebLearning
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IReportService, ReportService>();
			services.AddTransient<IReportBuilder, ReportBuilder>();
			services.AddTransient<IReportsFactory, FileReportsFactory>();
			services.AddTransient<IReporter, Reporter>();
			services.AddTransient<ISender, FileReportSender>();
			services.AddControllersWithViews();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
