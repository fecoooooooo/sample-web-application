using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Threading.Tasks;

namespace ApiGateway
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
				.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();

			builder.Services
				.AddOcelot(builder.Configuration)
				.AddDelegatingHandler<AcceptAllCertsHandler>(true);

			var app = builder.Build();
			await app.UseOcelot();
			app.Run();
		}
	}


	public sealed class AcceptAllCertsHandler : DelegatingHandler
	{
		private static readonly HttpClientHandler Inner = new HttpClientHandler
		{
			ServerCertificateCustomValidationCallback =
				HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
		};

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
		{
			// Újraépítjük a kérést a saját handlerünkkel
			var invoker = new HttpMessageInvoker(Inner, disposeHandler: false);
			return invoker.SendAsync(request, ct);
		}
	}
}