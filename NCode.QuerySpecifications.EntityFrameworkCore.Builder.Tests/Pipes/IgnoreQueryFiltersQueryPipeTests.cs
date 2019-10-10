using System.Linq;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes;
using NCode.QuerySpecifications.TestUtilities;
using Xunit;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Tests.Pipes
{
	public class IgnoreQueryFiltersQueryPipeTests
	{
		[Fact]
		public void Apply_IgnoresQueryFilter()
		{
			var pipe = new IgnoreQueryFiltersQueryPipe<CustomerQuery>();

			using (var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>())
			using (var context = fixture.CreateContext())
			{
				var queryRoot = context.Query<CustomerQuery>();

				var countBefore = queryRoot.Count();
				Assert.NotEqual(0, countBefore);

				var countAfter = pipe.Apply(queryRoot).Count();
				Assert.NotEqual(countBefore, countAfter);
			}
		}

	}
}