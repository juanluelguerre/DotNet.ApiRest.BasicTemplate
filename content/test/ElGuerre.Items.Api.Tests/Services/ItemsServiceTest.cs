using ElGuerre.Items.Api.Tests;
using ElGuerre.Items.Api.Application.Models;
using ElGuerre.Items.Api.Application.Services;
using ElGuerre.Items.Api.Domain;
using ElGuerre.Items.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ElGuerre.Items.Api.Tests.Services
{
    public class ItemsServiceTest : BaseTest
    {
        private readonly IItemsService itemsService;

        public ItemsServiceTest(CompositionRootFixture fixture) : base(fixture)
        {
            itemsService = Fixture.ServiceProvider.GetService<IItemsService>();
        }        

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(99)]
        [InlineData(int.MaxValue)]        
        public void GetItem_OK(int id)
        {
            var item = itemsService.GetItem(id);
            Assert.NotNull(item);
            Assert.Equal(1, item.Id);
        }

        [Fact]
        public void GetItem_LessthanZero()
        {
            var item = itemsService.GetItem(-1);
            Assert.NotNull(item);
            Assert.Equal(1, item.Id);
        }
    }
}
