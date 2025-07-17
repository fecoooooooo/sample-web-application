using Microsoft.EntityFrameworkCore;
using Shared.Models;
using User_API.Models;

namespace User_API.Data
{
	public class ApiContext : DbContext
	{
		public DbSet<Pokemon> Pokemon { get; set; }
		public DbSet<TempUser> TempUser { get; set; }

		public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

		/*protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Pokemon>().HasData(
				new Pokemon { Id = 1, Name = "Pikachu", Type = "Electric"},
				new Pokemon { Id = 2, Name = "Charizard", Type = "Fire" }
			);
		}*/
	}
}
