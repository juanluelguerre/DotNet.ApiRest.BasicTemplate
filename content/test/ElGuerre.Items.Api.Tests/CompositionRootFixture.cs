using AutoFixture;
using AutoFixture.AutoMoq;
using ElGuerre.Items.Api.Application.Services;
using ElGuerre.Items.Api.Domain;
using ElGuerre.Items.Api.Domain.Interfaces;
using ElGuerre.Items.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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


            // Prepare mocks
            Fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var dbContext = new Mock<ItemsContext>();
            dbContext.Setup(o => o.Items).Returns(() => GetMockDbSet());

            //var repository = Fixture.Freeze<Mock<IItemsRepository>>();
            //repository
            //    .Setup(s => s.GetByKeyAsync(It.IsAny<int>()))
            //    .ReturnsAsync(MockHelper.GetEntityMock());
            //repository
            //    .Setup(s => s.GetByKey(It.IsAny<int>()))
            //    .Returns(MockHelper.GetEntityMock());

            // Other services/interfaces.
            Services.AddTransient<IItemsService, ItemsService>();
            // Services.AddTransient(x => repository.Object);
        }

        private void Configure()
        {
            // Add some configuraton as needed
            // EJ.: ServiceProvider.GetService<IXxxxx>()
        }

        private DbSet<ItemEntity> GetMockDbSet()
        {
            var items = new List<ItemEntity>()
            {
                new ItemEntity() { Id = 1, Name ="Car", Description= "The car I'm going to buy" },
                new ItemEntity() { Id = 2, Name ="Laptop", Description= "The current laptop I'm using right now" },
                new ItemEntity() { Id = 3, Name ="Big House", Description= "The house of my dreams " }
            };

            var queryable = items.AsQueryable();

            var dbSet = new Mock<DbSet<ItemEntity>>();
            dbSet.As<IQueryable<ItemEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<ItemEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<ItemEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<ItemEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet.Object;
        }
    }
}