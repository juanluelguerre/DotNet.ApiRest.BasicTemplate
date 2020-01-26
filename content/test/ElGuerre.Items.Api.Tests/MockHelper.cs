using ElGuerre.Items.Api.Domain;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ElGuerre.Items.Api.Tests
{
    [ExcludeFromCodeCoverage]
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

        public static IEnumerable<ItemEntity> GetEntitiesMock()
        {
            return new[] {
                new ItemEntity() { Id = 1, Name = "Peugeot 3008", Description = "My new future car" },
                new ItemEntity() { Id = 2, Name = "Big House", Description = "My Dream home" },
                new ItemEntity() { Id = 3, Name = "Bruno", Description = "My happy son" },
                new ItemEntity() { Id = 4, Name = "SSD", Description = "My new Mac Hard Disk" }
            };
        }
    }
}
