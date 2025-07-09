
using Microsoft.EntityFrameworkCore;
using User_API.Models;

namespace User_API.Data
{
	public class ApiContext : DbContext
	{
		public DbSet<TempUser> TempUsers { get; set; }

		public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<TempUser>().HasData(
				new TempUser { Id = 1, NameEN = "Ash", NameJP = "Satoshi", Age = 10},
				new TempUser { Id = 2, NameEN = "Misty", NameJP = "Kasumi", Age = 9}
			);
		}
	}
}
