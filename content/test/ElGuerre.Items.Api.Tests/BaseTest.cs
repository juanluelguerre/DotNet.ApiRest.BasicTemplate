using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace ElGuerre.Items.Api.Tests
{
    // [ExcludeFromCodeCoverage]
    public abstract class BaseTest : IClassFixture<CompositionRootFixture>
    {
        protected readonly CompositionRootFixture Fixture;

        protected BaseTest(CompositionRootFixture fixture)
        {
            Fixture = fixture;
        }
    }
}
