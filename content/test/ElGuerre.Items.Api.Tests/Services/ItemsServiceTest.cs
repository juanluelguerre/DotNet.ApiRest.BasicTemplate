using ElGuerre.Items.Api.Application.Services;
using Microsoft.Extensions.DependencyInjection;
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
        [Trait("Category", "Services")]        
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
        [Trait("Category", "Services")]
        public void GetItem_LessthanZero()
        {
            var item = itemsService.GetItem(-1);
            Assert.NotNull(item);
            Assert.Equal(1, item.Id);
        }
    }
}
