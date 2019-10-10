using System.Linq;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes;
using NCode.QuerySpecifications.TestUtilities;
using Xunit;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Tests.Pipes
{
	public class AsNoTrackingQueryPipeTests
	{
		[Fact]
		public void Apply_DisablesChangeTracker()
		{
			var pipe = new AsNoTrackingQueryPipe<Customer>();

			using (var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>())
			using (var context = fixture.CreateContext())
			{
				var results = pipe.Apply(context.Customers).ToList();

				Assert.Equal(91, results.Count);
				Assert.Empty(context.ChangeTracker.Entries());
			}
		}

	}
}