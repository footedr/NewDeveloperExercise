using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NewDeveloperExercise.Services.Data;

public static class DbExtensions
{
	public static void ConfigureDatabase(this WebApplicationBuilder builder)
	{
		builder.AddSqlServerDbContext<NewDeveloperExerciseContext>("sqlserver", options =>
		{
			// The default value allows 'dotnet ef migrations bundles' to build.
			options.ConnectionString = builder.Configuration.GetConnectionString("newdeveloperexercise") ?? "DEFAULT";
		});
	}

	public static async Task InitializeDatabase(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var dbContext = serviceScope.ServiceProvider.GetRequiredService<NewDeveloperExerciseContext>();
			await dbContext.Database.MigrateAsync();
		}
	}
}
