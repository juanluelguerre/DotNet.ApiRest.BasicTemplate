using ElGuerre.Items.Api.IntegrationTests;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace ElGuerre.Items.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseTest : IClassFixture<CompositionRootFixture>
    {
        protected readonly CompositionRootFixture Fixture;

        protected BaseTest(CompositionRootFixture fixture)
        {
            Fixture = fixture;
        }
    }
}
