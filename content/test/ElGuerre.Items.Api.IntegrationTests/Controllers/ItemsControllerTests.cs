using ElGuerre.Items.Api.Application.Models;
using ElGuerre.Items.Api.Controllers;
using ElGuerre.Items.Api.IntegrationTests;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace ElGuerre.Items.IntegrationTests.Controllers
{
    public class ItemsControllerTests : BaseTest
    {
        private const string BASE_URL = "/api/v1/";
        private readonly string _baseUrl;

        public ItemsControllerTests(CompositionRootFixture fixture) : base(fixture)
        {
            _baseUrl =
                $"{BASE_URL}{nameof(ItemsController)}"
                .Replace("Controller", string.Empty);
        }

        [Fact]
        public async Task Get_Items_Ok()
        {
            // Arrange
            var url = $"{_baseUrl}";

            // Act
            var response = await Fixture.Client.GetAsync(url);

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ItemModel>>(content);
            Assert.NotNull(result);

            Assert.True(result.Count > 0, "No items found !");
        }


        [Fact]
        public async Task Post_Item_Ok()
        {
            // Arrange
            var url = $"{_baseUrl}";

            // Act            
            var model = new ItemModel
            {
                Id = 1,
                Name = "Peugeot 3008"
            };

            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await Fixture.Client.PostAsync(url, byteContent);

            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var result = int.Parse(content);
            Assert.Equal(1, result);
        }
    }
}
