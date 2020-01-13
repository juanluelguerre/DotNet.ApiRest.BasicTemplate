using ElGuerre.Items.Api.Domain;

namespace ElGuerre.Items.Api.Tests
{
    public static class MockHelper
    {
        public static ItemEntity GetEntityMock()
        {
            return new ItemEntity
            {
                Id = 1,
                Name = "One thing",
                Description = "One thing to do something every where !"
            };
        }
    }
}
