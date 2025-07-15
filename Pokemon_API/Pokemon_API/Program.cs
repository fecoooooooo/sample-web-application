
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pokemon_API.Extensions;
using User_API.Data;

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

			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pokemon API", Version = "v1" });

				c.AddServer(new OpenApiServer
				{
					Url = "https://localhost:8081"
				});
			});


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
			
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				app.ApplyMigrations();
			}

			app.UseCors("AllowMultipleOrigins");

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
