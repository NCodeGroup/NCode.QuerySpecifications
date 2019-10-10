using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using NCode.QuerySpecifications.Specifications;
using Xunit;

namespace NCode.QuerySpecifications.Tests
{
	public class QueryTests
	{
		[Fact]
		public void Build_Where()
		{
			var query = Query<Order>.Build(config => config.Where(_ => _.Freight == 0));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("Where", spec.Name);
			Assert.Empty(spec.OutputSpecifications);
			Assert.IsAssignableFrom<IWhereQuerySpecification<Order>>(spec);
		}

		[Fact]
		public void Build_OrderByAscending()
		{
			var query = Query<Order>.Build(config => config.OrderBy(_ => _.Freight));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("OrderBy", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);

			Assert.False(orderBySpec.Descending);
			Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
			Assert.Null(orderBySpec.Comparer);
		}

		[Fact]
		public void Build_OrderByAscending_Comparer()
		{
			var query = Query<Order>.Build(config => config.OrderBy(_ => _.Freight, Comparer<decimal?>.Default));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("OrderBy", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);

			Assert.False(orderBySpec.Descending);
			Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
			Assert.Same(Comparer<decimal?>.Default, orderBySpec.Comparer);
		}

		[Fact]
		public void Build_OrderByDescending()
		{
			var query = Query<Order>.Build(config => config.OrderByDescending(_ => _.Freight, Comparer<decimal?>.Default));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("OrderBy", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);

			Assert.True(orderBySpec.Descending);
			Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
			Assert.Same(Comparer<decimal?>.Default, orderBySpec.Comparer);
		}

		[Fact]
		public void Build_OrderByAscending_ThenBy()
		{
			var query = Query<Order>.Build(config => config.OrderBy(_ => _.Freight).ThenBy(_ => _.ShipVia, Comparer<int?>.Default));

			var spec1 = Assert.Single(query.OutputSpecifications);
			Assert.NotNull(spec1);
			Assert.Equal("OrderBy", spec1.Name);

			var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec1);
			Assert.False(orderBySpec.Descending);
			Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
			Assert.Null(orderBySpec.Comparer);

			var spec2 = Assert.Single(orderBySpec.OutputSpecifications);
			Assert.NotNull(spec2);
			Assert.Equal("OrderBy", spec2.Name);

			var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, int?>>(spec2);
			Assert.False(thenBySpec.Descending);
			Assert.Equal(typeof(int?), thenBySpec.PropertyType);
			Assert.Same(Comparer<int?>.Default, thenBySpec.Comparer);
		}

		[Fact]
		public void Build_Page()
		{
			var query = Query<Order>.Build(config => config.Page(5, 10));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("Page", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var pageSpec = Assert.IsAssignableFrom<IPageQuerySpecification<Order>>(spec);

			Assert.Equal(5, pageSpec.Skip);
			Assert.Equal(10, pageSpec.Take);
		}

		[Fact]
		public void Build_Distinct()
		{
			var query = Query<Order>.Build(config => config.Distinct(EqualityComparer<Order>.Default));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("Distinct", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var distinctSpec = Assert.IsAssignableFrom<IDistinctQuerySpecification<Order>>(spec);

			Assert.Same(EqualityComparer<Order>.Default, distinctSpec.Comparer);
		}

		[Fact]
		public void Build_Select()
		{
			var query = Query<Order>.Build(config => config.Select(_ => _.Customer));

			Assert.Empty(query.InputSpecifications);
			Assert.Empty(query.OutputSpecifications);

			Assert.Equal("Select", query.TransformSpecification.Name);
			Assert.Same(query.InputSpecifications, query.TransformSpecification.InputSpecifications);
			Assert.Same(query.OutputSpecifications, query.TransformSpecification.OutputSpecifications);

			Assert.IsAssignableFrom<ISelectTransformSpecification<Order, Customer>>(query.TransformSpecification);
		}

		[Fact]
		public void Build_Select_InputAndOutput()
		{
			var query = Query<Order>.Build(config =>
			{
				return config
					.Where(_ => _.Freight == 0)
					.OrderBy(_ => _.OrderDate)
					.Select(_ => _.Customer)
					.Output(output => output
						.OrderBy(_ => _.ContactName));
			});

			Assert.Equal(2, query.InputSpecifications.Count);
			Assert.Single(query.OutputSpecifications);

			Assert.Equal("Select", query.TransformSpecification.Name);
			Assert.Same(query.InputSpecifications, query.TransformSpecification.InputSpecifications);
			Assert.Same(query.OutputSpecifications, query.TransformSpecification.OutputSpecifications);

			Assert.IsAssignableFrom<ISelectTransformSpecification<Order, Customer>>(query.TransformSpecification);
		}

	}
}