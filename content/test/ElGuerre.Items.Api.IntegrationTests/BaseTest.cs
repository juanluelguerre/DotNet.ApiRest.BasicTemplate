using ElGuerre.Items.Api.IntegrationTests;
using Xunit;

namespace ElGuerre.Items.IntegrationTests
{
    public abstract class BaseTest : IClassFixture<CompositionRootFixture>
    {
        protected readonly CompositionRootFixture Fixture;

        protected BaseTest(CompositionRootFixture fixture)
        {
            Fixture = fixture;
        }
    }
}
