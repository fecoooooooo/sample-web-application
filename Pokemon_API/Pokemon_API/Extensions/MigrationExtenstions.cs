using Microsoft.EntityFrameworkCore;
using User_API.Data;

namespace Pokemon_API.Extensions
{
	public static class MigrationExtenstions
	{
		public static void ApplyMigrations(this IApplicationBuilder app)
		{
			using IServiceScope scope = app.ApplicationServices.CreateScope();

			using ApiContext dbContext = 
				scope.ServiceProvider.GetRequiredService<ApiContext>();

			dbContext.Database.Migrate();
		}
	}
}
