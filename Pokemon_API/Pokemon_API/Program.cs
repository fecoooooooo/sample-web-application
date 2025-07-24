
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pokemon_API.DTO;
using Pokemon_API.Extensions;
using Pokemon_API.Repository;
using Shared.Models;
using Shared.Repository;
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
					Url = "https://localhost:8085"
				});
			});

			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.CreateMap<Pokemon, PokemonDto>();
				cfg.CreateMap<PokemonDto, Pokemon>();
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

			builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
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
