using System.Linq;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes;
using NCode.QuerySpecifications.TestUtilities;
using Xunit;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Tests.Pipes
{
    public class AsTrackingQueryPipeTests
    {
        [Fact]
        public void Apply_EnablesChangeTracker()
        {
            var pipe = new AsTrackingQueryPipe<Customer>();

            using var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>();
            using var context = fixture.CreateContext();

            var results = pipe.Apply(context.Customers).ToList();

            var expectedCount = fixture.QueryAsserter.ExpectedData.Set<Customer>().Count();
            Assert.NotEqual(0, expectedCount);

            Assert.Equal(expectedCount, results.Count);
            Assert.Equal(expectedCount, context.ChangeTracker.Entries().Count());
        }

    }
}