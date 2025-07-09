using Microsoft.EntityFrameworkCore;
using User_API.Models;

namespace User_API.Data
{
	public class ApiContext : DbContext
	{
		public DbSet<Pokemon> Pokemon { get; set; }

		public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
	}
}
