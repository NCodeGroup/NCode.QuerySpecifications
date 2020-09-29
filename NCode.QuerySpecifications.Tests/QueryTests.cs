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
            var query = Query<Order>.Configure(config => config
                .Where(order => order.Freight == 0));

            var spec = Assert.Single(query.OutputSpecifications);

            Assert.NotNull(spec);
            Assert.Equal("Where", spec.Name);
            Assert.IsAssignableFrom<IWhereQuerySpecification<Order>>(spec);
        }

        [Fact]
        public void Build_OrderByAscending_WithoutComparer()
        {
            var query = Query<Order>.Configure(config => config
                .OrderBy(order => order.Freight));

            var spec = Assert.Single(query.OutputSpecifications);

            Assert.NotNull(spec);
            Assert.Equal("OrderBy", spec.Name);

            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);

            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_WithComparer()
        {
            var query = Query<Order>.Configure(config => config
                .OrderBy(order => order.Freight, Comparer<decimal?>.Default));

            var spec = Assert.Single(query.OutputSpecifications);

            Assert.NotNull(spec);
            Assert.Equal("OrderBy", spec.Name);

            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);

            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Same(Comparer<decimal?>.Default, orderBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByDescending_WithoutComparer()
        {
            var query = Query<Order>.Configure(config => config
                .OrderByDescending(order => order.Freight));

            var spec = Assert.Single(query.OutputSpecifications);

            Assert.NotNull(spec);
            Assert.Equal("OrderBy", spec.Name);

            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);

            Assert.True(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByDescending_WithComparer()
        {
            var query = Query<Order>.Configure(config => config
                .OrderByDescending(order => order.Freight, Comparer<decimal?>.Default));

            var spec = Assert.Single(query.OutputSpecifications);

            Assert.NotNull(spec);
            Assert.Equal("OrderBy", spec.Name);

            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);

            Assert.True(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Same(Comparer<decimal?>.Default, orderBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByAscending_WithoutComparer()
        {
            var query = Query<Order>.Configure(config => config
                .OrderBy(order => order.Freight)
                .ThenBy(order => order.ShipVia));

            Assert.Equal(2, query.OutputSpecifications.Count);

            var spec1 = query.OutputSpecifications[0];
            Assert.NotNull(spec1);
            Assert.Equal("OrderBy", spec1.Name);

            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec1);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);

            var spec2 = query.OutputSpecifications[1];
            Assert.NotNull(spec2);
            Assert.Equal("OrderBy", spec2.Name);

            var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, int?>>(spec2);
            Assert.False(thenBySpec.Descending);
            Assert.Equal(typeof(int?), thenBySpec.PropertyType);
            Assert.Null(thenBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByAscending_WithComparer()
        {
            var query = Query<Order>.Configure(config => config
                .OrderBy(order => order.Freight)
                .ThenBy(order => order.ShipVia, Comparer<int?>.Default));

            Assert.Equal(2, query.OutputSpecifications.Count);

            var spec1 = query.OutputSpecifications[0];
            Assert.NotNull(spec1);
            Assert.Equal("OrderBy", spec1.Name);

            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec1);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);

            var spec2 = query.OutputSpecifications[1];
            Assert.NotNull(spec2);
            Assert.Equal("OrderBy", spec2.Name);

            var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, int?>>(spec2);
            Assert.False(thenBySpec.Descending);
            Assert.Equal(typeof(int?), thenBySpec.PropertyType);
            Assert.Same(Comparer<int?>.Default, thenBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByDescending_WithoutComparer()
        {
            var query = Query<Order>.Configure(config => config
                .OrderBy(order => order.Freight)
                .ThenByDescending(order => order.ShipVia));

            Assert.Equal(2, query.OutputSpecifications.Count);

            var spec1 = query.OutputSpecifications[0];
            Assert.NotNull(spec1);
            Assert.Equal("OrderBy", spec1.Name);

            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec1);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);

            var spec2 = query.OutputSpecifications[1];
            Assert.NotNull(spec2);
            Assert.Equal("OrderBy", spec2.Name);

            var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, int?>>(spec2);
            Assert.True(thenBySpec.Descending);
            Assert.Equal(typeof(int?), thenBySpec.PropertyType);
            Assert.Null(thenBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByDescending_WithComparer()
        {
            var query = Query<Order>.Configure(config => config
                .OrderBy(order => order.Freight)
                .ThenByDescending(order => order.ShipVia, Comparer<int?>.Default));

            Assert.Equal(2, query.OutputSpecifications.Count);

            var spec1 = query.OutputSpecifications[0];
            Assert.NotNull(spec1);
            Assert.Equal("OrderBy", spec1.Name);

            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec1);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);

            var spec2 = query.OutputSpecifications[1];
            Assert.NotNull(spec2);
            Assert.Equal("OrderBy", spec2.Name);

            var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, int?>>(spec2);
            Assert.True(thenBySpec.Descending);
            Assert.Equal(typeof(int?), thenBySpec.PropertyType);
            Assert.Same(Comparer<int?>.Default, thenBySpec.Comparer);
        }

        [Fact]
        public void Build_Page()
        {
            var query = Query<Order>.Configure(config => config
                .Page(5, 10));

            var spec = Assert.Single(query.OutputSpecifications);

            Assert.NotNull(spec);
            Assert.Equal("Page", spec.Name);

            var pageSpec = Assert.IsAssignableFrom<IPageQuerySpecification<Order>>(spec);

            Assert.Equal(5, pageSpec.Skip);
            Assert.Equal(10, pageSpec.Take);
        }

        [Fact]
        public void Build_Distinct_WithoutComparer()
        {
            var query = Query<Order>.Configure(config => config
                .Distinct());

            var spec = Assert.Single(query.OutputSpecifications);

            Assert.NotNull(spec);
            Assert.Equal("Distinct", spec.Name);

            var distinctSpec = Assert.IsAssignableFrom<IDistinctQuerySpecification<Order>>(spec);

            Assert.Null(distinctSpec.Comparer);
        }

        [Fact]
        public void Build_Distinct_WithComparer()
        {
            var query = Query<Order>.Configure(config => config
                .Distinct(EqualityComparer<Order>.Default));

            var spec = Assert.Single(query.OutputSpecifications);

            Assert.NotNull(spec);
            Assert.Equal("Distinct", spec.Name);

            var distinctSpec = Assert.IsAssignableFrom<IDistinctQuerySpecification<Order>>(spec);

            Assert.Same(EqualityComparer<Order>.Default, distinctSpec.Comparer);
        }

        [Fact]
        public void Build_Select_WithoutInputSpecs_WithoutOutputSpecs()
        {
            var query = Query<Order>.Configure(config => config
                .Select(order => order.Customer));

            Assert.Empty(query.InputSpecifications);
            Assert.Empty(query.OutputSpecifications);

            Assert.Equal("Select", query.TransformSpecification.Name);
            Assert.IsAssignableFrom<ISelectQuerySpecification<Order, Customer>>(query.TransformSpecification);
        }

        [Fact]
        public void Build_Select_WithInputSpecs_WithOutputSpecs()
        {
            var query = Query<Order>.Configure(config => config
                .Where(order => order.Freight == 0)
                .OrderBy(order => order.OrderDate)
                .Select(order => order.Customer)
                .Output(output => output.Distinct().OrderBy(customer => customer.Country).ThenBy(customer => customer.CompanyName)));

            Assert.Equal(2, query.InputSpecifications.Count);
            Assert.Equal("Where", query.InputSpecifications[0].Name);
            Assert.Equal("OrderBy", query.InputSpecifications[1].Name);

            Assert.Equal(3, query.OutputSpecifications.Count);
            Assert.Equal("Distinct", query.OutputSpecifications[0].Name);
            Assert.Equal("OrderBy", query.OutputSpecifications[1].Name);
            Assert.Equal("OrderBy", query.OutputSpecifications[2].Name);

            Assert.Equal("Select", query.TransformSpecification.Name);
            Assert.IsAssignableFrom<ISelectQuerySpecification<Order, Customer>>(query.TransformSpecification);
        }

    }
}