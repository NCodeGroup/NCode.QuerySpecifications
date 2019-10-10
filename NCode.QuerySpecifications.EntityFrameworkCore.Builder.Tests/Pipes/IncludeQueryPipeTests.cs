using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes;
using NCode.QuerySpecifications.TestUtilities;
using Xunit;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Tests.Pipes
{
	public class IncludeQueryPipeTests
	{
		[Fact]
		public void Apply_GivenPath_ThenNotNull()
		{
			var pipe = new IncludePathQueryPipe<Order>(nameof(Order.Customer));

			using (var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>())
			using (var context = fixture.CreateContext())
			{
				var queryRoot = context.Set<Order>();

				var before = queryRoot;
				Assert.NotEmpty(before);
				Assert.All(before, order => Assert.Null(order.Customer));

				var after = pipe.Apply(queryRoot).ToList();
				Assert.NotEmpty(after);
				Assert.All(after, order => Assert.NotNull(order.Customer));
			}
		}

		[Fact]
		public void Apply_GivenProperty_ThenNotNull()
		{
			var pipe = new IncludePropertyQueryPipe<Order, Customer>(order => order.Customer);

			using (var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>())
			using (var context = fixture.CreateContext())
			{
				var queryRoot = context.Set<Order>();

				var before = queryRoot;
				Assert.NotEmpty(before);
				Assert.All(before, order => Assert.Null(order.Customer));

				var after = pipe.Apply(queryRoot).ToList();
				Assert.NotEmpty(after);
				Assert.All(after, order => Assert.NotNull(order.Customer));
			}
		}

		[Fact]
		public void Apply_GivenPropertyThenObject_ThenNotNull()
		{
			var pipe = new IncludePropertyThenQueryPipe<OrderDetail, Order, Customer>(order => order.Customer, false);

			using (var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>())
			using (var context = fixture.CreateContext())
			{
				var queryRoot = context.Set<OrderDetail>().Include(detail => detail.Order);

				var before = queryRoot;
				Assert.NotEmpty(before);
				Assert.All(before, detail =>
				{
					Assert.NotNull(detail.Order);
					Assert.Null(detail.Order.Customer);
				});

				var after = pipe.Apply(queryRoot).ToList();
				Assert.NotEmpty(after);
				Assert.All(after, detail =>
				{
					Assert.NotNull(detail.Order);
					Assert.NotNull(detail.Order.Customer);
				});
			}
		}

	}
}