using ElGuerre.Items.Api.Application.Extensions;
using ElGuerre.Items.Api.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;

namespace ElGuerre.Items.Api.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class CompositionRootFixture
    {
        private readonly TestServer _server;
        public HttpClient Client { get; }

        public CompositionRootFixture()
        {
            var host = new WebHostBuilder();

            _server = new TestServer(host
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

            _server.Host.MigrateDbContext<ItemsContext>((context, services) =>
            {
                var env = services.GetService<IWebHostEnvironment>();
                var settings = services.GetService<IOptions<AppSettings>>();
                var logger = services.GetService<ILogger<ItemsContextSeed>>();

                new ItemsContextSeed()
                    .SeedAsync(context, env, settings, logger)
                    .Wait();
            });

            Client = _server.CreateClient();
        }

        ~CompositionRootFixture()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}