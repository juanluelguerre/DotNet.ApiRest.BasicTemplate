using ElGuerre.Items.Api.Application.Models;
using ElGuerre.Items.Api.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ElGuerre.Items.Api.Tests.Services
{
   // [Trait("Category", "Services")]
    public class ItemsServiceTest : BaseTest
    {
        private readonly IItemsService itemsService;

        public ItemsServiceTest(CompositionRootFixture fixture) : base(fixture)
        {
            itemsService = Fixture.ServiceProvider.GetService<IItemsService>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void GetItem_OK(int id)
        {
            var item = itemsService.GetItem(id);
            Assert.NotNull(item);
            Assert.Equal(id, item.Id);
        }

        [Fact]
        public void GetItem_LessthanZero()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => itemsService.GetItem(-1));
            Assert.Equal($"Item Id must be a possitive number.{Environment.NewLine}Parameter name: id", ex.Message);
        }

        [Fact]
        public void GetAllItems_OK()
        {
            var item = itemsService.GetItems();
            Assert.NotNull(item);
            Assert.Equal(4, item.Count);
        }

        [Fact]
        public async Task Update_Ok()
        {
            var model = new ItemModel
            {
                Id = 2,
                Name = "More and More Big House on the Mountain"
            };
            var result = await itemsService.UpdateAsync(model);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Update_NullModel_ArgumentNullException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => itemsService.UpdateAsync(null));
            Assert.Equal($"Input model cannot be null.{Environment.NewLine}Parameter name: model", ex.Message);
        }

        [Fact]
        public async Task Update_EmptyModel_ArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => itemsService.UpdateAsync(new ItemModel()));
            Assert.Equal($"Item Id cannot be null or empty.{Environment.NewLine}Parameter name: Id", ex.Message);
        }
    }
}
