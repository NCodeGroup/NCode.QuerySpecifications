using System.Collections.Generic;
using NCode.QuerySpecifications.Specifications;
using NCode.QuerySpecifications.TestModels;
using Xunit;

namespace NCode.QuerySpecifications.Tests
{
	public class QueryTests
	{
		[Fact]
		public void Build_Where()
		{
			var query = Query<Post>.Build(config => config.Where(_ => _.Rating == 0));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("Where", spec.Name);
			Assert.Empty(spec.OutputSpecifications);
			Assert.IsAssignableFrom<IWhereQuerySpecification<Post>>(spec);
		}

		[Fact]
		public void Build_OrderByAscending()
		{
			var query = Query<Post>.Build(config => config.OrderBy(_ => _.Rating));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("OrderBy", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Post, int>>(spec);

			Assert.False(orderBySpec.Descending);
			Assert.Equal(typeof(int), orderBySpec.PropertyType);
			Assert.Null(orderBySpec.Comparer);
		}

		[Fact]
		public void Build_OrderByAscending_Comparer()
		{
			var query = Query<Post>.Build(config => config.OrderBy(_ => _.Rating, Comparer<int>.Default));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("OrderBy", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Post, int>>(spec);

			Assert.False(orderBySpec.Descending);
			Assert.Equal(typeof(int), orderBySpec.PropertyType);
			Assert.Same(Comparer<int>.Default, orderBySpec.Comparer);
		}

		[Fact]
		public void Build_OrderByDescending()
		{
			var query = Query<Post>.Build(config => config.OrderByDescending(_ => _.Rating, Comparer<int>.Default));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("OrderBy", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Post, int>>(spec);

			Assert.True(orderBySpec.Descending);
			Assert.Equal(typeof(int), orderBySpec.PropertyType);
			Assert.Same(Comparer<int>.Default, orderBySpec.Comparer);
		}

		[Fact]
		public void Build_OrderByAscending_ThenBy()
		{
			var query = Query<Post>.Build(config => config.OrderBy(_ => _.Rating).ThenBy(_ => _.Content, Comparer<string>.Default));

			var spec1 = Assert.Single(query.OutputSpecifications);
			Assert.NotNull(spec1);
			Assert.Equal("OrderBy", spec1.Name);

			var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Post, int>>(spec1);
			Assert.False(orderBySpec.Descending);
			Assert.Equal(typeof(int), orderBySpec.PropertyType);
			Assert.Null(orderBySpec.Comparer);

			var spec2 = Assert.Single(orderBySpec.OutputSpecifications);
			Assert.NotNull(spec2);
			Assert.Equal("OrderBy", spec2.Name);

			var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Post, string>>(spec2);
			Assert.False(thenBySpec.Descending);
			Assert.Equal(typeof(string), thenBySpec.PropertyType);
			Assert.Same(Comparer<string>.Default, thenBySpec.Comparer);
		}

		[Fact]
		public void Build_Page()
		{
			var query = Query<Post>.Build(config => config.Page(5, 10));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("Page", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var pageSpec = Assert.IsAssignableFrom<IPageQuerySpecification<Post>>(spec);

			Assert.Equal(5, pageSpec.Skip);
			Assert.Equal(10, pageSpec.Take);
		}

		[Fact]
		public void Build_Distinct()
		{
			var query = Query<Post>.Build(config => config.Distinct(EqualityComparer<Post>.Default));

			var spec = Assert.Single(query.OutputSpecifications);

			Assert.NotNull(spec);
			Assert.Equal("Distinct", spec.Name);
			Assert.Empty(spec.OutputSpecifications);

			var distinctSpec = Assert.IsAssignableFrom<IDistinctQuerySpecification<Post>>(spec);

			Assert.Same(EqualityComparer<Post>.Default, distinctSpec.Comparer);
		}

		[Fact]
		public void Build_Select()
		{
			var query = Query<Post>.Build(config => config.Select(_ => _.Blog));

			Assert.Empty(query.InputSpecifications);
			Assert.Empty(query.OutputSpecifications);

			Assert.Equal("Select", query.TransformSpecification.Name);
			Assert.Same(query.InputSpecifications, query.TransformSpecification.InputSpecifications);
			Assert.Same(query.OutputSpecifications, query.TransformSpecification.OutputSpecifications);

			Assert.IsAssignableFrom<ISelectTransformSpecification<Post, Blog>>(query.TransformSpecification);
		}

		[Fact]
		public void Build_Select_InputAndOutput()
		{
			var query = Query<Post>.Build(config =>
			{
				return config
					.Where(_ => _.Rating == 0)
					.OrderBy(_ => _.PostId)
					.Select(_ => _.Blog)
					.Output(output => output
						.OrderBy(_ => _.BlogId));
			});

			Assert.Equal(2, query.InputSpecifications.Count);
			Assert.Single(query.OutputSpecifications);

			Assert.Equal("Select", query.TransformSpecification.Name);
			Assert.Same(query.InputSpecifications, query.TransformSpecification.InputSpecifications);
			Assert.Same(query.OutputSpecifications, query.TransformSpecification.OutputSpecifications);

			Assert.IsAssignableFrom<ISelectTransformSpecification<Post, Blog>>(query.TransformSpecification);
		}

	}
}