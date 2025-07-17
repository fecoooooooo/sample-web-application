using Microsoft.EntityFrameworkCore;
using Shared.Models;
using User_API.Models;

namespace User_API.Data
{
	public class ApiContext : DbContext
	{
		public DbSet<Pokemon> Pokemon { get; set; }
		public DbSet<User> User { get; set; }

		public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Pokemon>().HasData(
				new Pokemon { Id = 1, Name = "Pikachu", Type = "Electric"},
				new Pokemon { Id = 2, Name = "Zapdosh", Type = "Electric"},
				new Pokemon { Id = 3, Name = "Charizard", Type = "Fire" },
				new Pokemon { Id = 4, Name = "Arcaine", Type = "Fire" },
				new Pokemon { Id = 5, Name = "Rapidash", Type = "Fire" },
				new Pokemon { Id = 6, Name = "Weedle", Type = "Bug" },
				new Pokemon { Id = 7, Name = "Venomoth", Type = "Bug" },
				new Pokemon { Id = 8, Name = "Metapod", Type = "Bug" }
			);

			modelBuilder.Entity<User>().HasData(
				new User { Id = 1, Age = 10, NameEN = "Ash", NameJP = "Satoshi" },
				new User { Id = 2, Age = 11, NameEN = "Misty", NameJP = "Kasumi" },
				new User { Id = 3, Age = 11, NameEN = "Brock", NameJP = "Takeshi" }
			);
		}	
	}
}
