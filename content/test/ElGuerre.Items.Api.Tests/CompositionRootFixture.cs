using AutoFixture;
using AutoFixture.AutoMoq;
using ElGuerre.Items.Api.Application.Services;
using ElGuerre.Items.Api.Domain.Interfaces;
using ElGuerre.Items.Api.Infrastructure;
using ElGuerre.Items.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Security.Principal;

namespace ElGuerre.Items.Api.Tests
{
    [ExcludeFromCodeCoverage]
    public class CompositionRootFixture
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IServiceCollection Services { get; private set; }
        public IFixture Fixture { get; private set; }
        public IConfigurationRoot Configuration { get; }

        public CompositionRootFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            Services = new ServiceCollection();
            ConfigureServices();

            ServiceProvider = Services.BuildServiceProvider();
            Configure();
        }

        private void ConfigureServices()
        {
            // Dependency Injection
            Services = new ServiceCollection();

            // Net core services. 
            Services.AddLogging();
            Services.AddHttpContextAccessor();
            Services.Add(new ServiceDescriptor(typeof(IPrincipal),
                provider => new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                {
                    new Claim("## key ##", "## value ##"),
                    new Claim("## key ##", "## value ##"),
                })), ServiceLifetime.Scoped));
            Services.AddSingleton((IConfiguration)Configuration);


            // Prepare mocks.

            // TODO: Include Autofixture to learning about it but not used yet !!!
            Fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            // TODO: configure Autofixture when need it !


            Services.AddDbContext<ItemsContext>((x) => x.UseInMemoryDatabase(databaseName: Program.AppName), ServiceLifetime.Transient);

            // Other services/interfaces.
            Services.AddTransient<IItemsRepository, ItemsRepository>();
            Services.AddTransient<IItemsService, ItemsService>();
        }

        private void Configure()
        {
            using (var context = ServiceProvider.GetService<ItemsContext>())
            {
                context.AddRange(MockHelper.GetEntitiesMock());
                context.SaveChanges();
            }

            // Add some configuraton as needed
            // EJ.: ServiceProvider.GetService<IXxxxx>()
        }
    }
}