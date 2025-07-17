
using Microsoft.EntityFrameworkCore;
using User_API.Data;
using User_API.DTO;
using User_API.Models;
using User_API.Repository;

namespace User_API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<ApiContext>(o =>
				o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowMultipleOrigins",
					policy => policy
						.WithOrigins("http://localhost:4200", "https://localhost:8081")
						.AllowAnyHeader()
						.AllowAnyMethod());
			});


			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.CreateMap<UserDto, User>();
				cfg.CreateMap<User, UserDto>();
			});

			builder.Services.AddScoped<IUserRepository, UserRepository>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("AllowMultipleOrigins");

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
