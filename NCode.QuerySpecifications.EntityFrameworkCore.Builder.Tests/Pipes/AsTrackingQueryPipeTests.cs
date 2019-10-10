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

			using (var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>())
			using (var context = fixture.CreateContext())
			{
				var results = pipe.Apply(context.Customers).ToList();

				Assert.Equal(91, results.Count);
				Assert.Equal(91, context.ChangeTracker.Entries().Count());
			}
		}

	}
}