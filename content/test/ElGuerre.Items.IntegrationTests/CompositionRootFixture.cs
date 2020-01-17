using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;

namespace ElGuerre.Items.Api.IntegrationTests
{
	// [ExcludeFromCodeCoverage]
    public class CompositionRootFixture
    {
		private readonly TestServer _server;
		public HttpClient Client { get; }

		public CompositionRootFixture()
		{
			_server = new TestServer(new WebHostBuilder()
				.UseEnvironment("Tests")
				// .UseEnvironment("Development")
				.CaptureStartupErrors(true)
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseWebRoot("Assets")
				.ConfigureAppConfiguration((context, configBuilder) =>
				{
					configBuilder
						.SetBasePath(context.HostingEnvironment.ContentRootPath)
						.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
						.AddEnvironmentVariables();
				})
				.UseStartup<Startup>());
				

			Client = _server.CreateClient();
		}

		~CompositionRootFixture()
		{
			Client.Dispose();
			_server.Dispose();
		}
	}
}