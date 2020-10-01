#region Copyright Preamble
// 
//    Copyright @ 2020 NCode Group
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
#endregion

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using NCode.QuerySpecifications.Pipes;
using NCode.QuerySpecifications.Specifications;
using Xunit;

namespace NCode.QuerySpecifications.Tests
{
    public class QueryTests
    {
        [Fact]
        public void Build_Where()
        {
            var query = Query<Order>.Build(config => config
                .Where(order => order.Freight == 0));

            Assert.IsAssignableFrom<WhereQueryPipe<Order>>(query);
        }

        [Fact]
        public void Build_OrderByAscending_WithoutComparer()
        {
            var query = Query<Order>.Build(config => config
                .OrderBy(order => order.Freight));

            var orderBySpec = Assert.IsAssignableFrom<OrderByQueryPipe<Order, decimal?>>(query);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_WithComparer()
        {
            var query = Query<Order>.Build(config => config
                .OrderBy(order => order.Freight, Comparer<decimal?>.Default));

            var spec = Assert.Single(query.Specifications);
            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Same(Comparer<decimal?>.Default, orderBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByDescending_WithoutComparer()
        {
            var query = Query<Order>.Build(config => config
                .OrderByDescending(order => order.Freight));

            var spec = Assert.Single(query.Specifications);
            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);
            Assert.True(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByDescending_WithComparer()
        {
            var query = Query<Order>.Build(config => config
                .OrderByDescending(order => order.Freight, Comparer<decimal?>.Default));

            var spec = Assert.Single(query.Specifications);

            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec);
            Assert.True(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Same(Comparer<decimal?>.Default, orderBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByAscending_WithoutComparer()
        {
            var query = Query<Order>.Build(config => config
                .OrderBy(order => order.Freight)
                .ThenBy(order => order.ShipVia));

            Assert.Equal(2, query.Specifications.Count);

            var spec1 = query.Specifications[0];
            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec1);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);

            var spec2 = query.Specifications[1];
            var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, int?>>(spec2);
            Assert.False(thenBySpec.Descending);
            Assert.Equal(typeof(int?), thenBySpec.PropertyType);
            Assert.Null(thenBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByAscending_WithComparer()
        {
            var query = Query<Order>.Build(config => config
                .OrderBy(order => order.Freight)
                .ThenBy(order => order.ShipVia, Comparer<int?>.Default));

            Assert.Equal(2, query.Specifications.Count);

            var spec1 = query.Specifications[0];
            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec1);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);

            var spec2 = query.Specifications[1];
            var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, int?>>(spec2);
            Assert.False(thenBySpec.Descending);
            Assert.Equal(typeof(int?), thenBySpec.PropertyType);
            Assert.Same(Comparer<int?>.Default, thenBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByDescending_WithoutComparer()
        {
            var query = Query<Order>.Build(config => config
                .OrderBy(order => order.Freight)
                .ThenByDescending(order => order.ShipVia));

            Assert.Equal(2, query.Specifications.Count);

            var spec1 = query.Specifications[0];
            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec1);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);

            var spec2 = query.Specifications[1];
            var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, int?>>(spec2);
            Assert.True(thenBySpec.Descending);
            Assert.Equal(typeof(int?), thenBySpec.PropertyType);
            Assert.Null(thenBySpec.Comparer);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByDescending_WithComparer()
        {
            var query = Query<Order>.Build(config => config
                .OrderBy(order => order.Freight)
                .ThenByDescending(order => order.ShipVia, Comparer<int?>.Default));

            Assert.Equal(2, query.Specifications.Count);

            var spec1 = query.Specifications[0];
            var orderBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, decimal?>>(spec1);
            Assert.False(orderBySpec.Descending);
            Assert.Equal(typeof(decimal?), orderBySpec.PropertyType);
            Assert.Null(orderBySpec.Comparer);

            var spec2 = query.Specifications[1];
            var thenBySpec = Assert.IsAssignableFrom<IOrderByQuerySpecification<Order, int?>>(spec2);
            Assert.True(thenBySpec.Descending);
            Assert.Equal(typeof(int?), thenBySpec.PropertyType);
            Assert.Same(Comparer<int?>.Default, thenBySpec.Comparer);
        }

        [Fact]
        public void Build_Page()
        {
            var query = Query<Order>.Build(config => config
                .Page(5, 10));

            var spec = Assert.Single(query.Specifications);
            var pageSpec = Assert.IsAssignableFrom<IPageQuerySpecification<Order>>(spec);
            Assert.Equal(5, pageSpec.Skip);
            Assert.Equal(10, pageSpec.Take);
        }

        [Fact]
        public void Build_Distinct_WithoutComparer()
        {
            var query = Query<Order>.Build(config => config
                .Distinct());

            var spec = Assert.Single(query.Specifications);
            var distinctSpec = Assert.IsAssignableFrom<IDistinctQuerySpecification<Order>>(spec);
            Assert.Null(distinctSpec.Comparer);
        }

        [Fact]
        public void Build_Distinct_WithComparer()
        {
            var query = Query<Order>.Build(config => config
                .Distinct(EqualityComparer<Order>.Default));

            var spec = Assert.Single(query.Specifications);
            var distinctSpec = Assert.IsAssignableFrom<IDistinctQuerySpecification<Order>>(spec);
            Assert.Same(EqualityComparer<Order>.Default, distinctSpec.Comparer);
        }

        [Fact]
        public void Build_Select_WithoutInputSpecs_WithoutOutputSpecs()
        {
            var query = Query<Order>.Build(config => config
                .Select(order => order.Customer));

            Assert.Empty(query.InputSpecifications);
            Assert.Empty(query.OutputSpecifications);

            Assert.IsAssignableFrom<ISelectQuerySpecification<Order, Customer>>(query.TransformSpecification);
        }

        [Fact]
        public void Build_Select_WithInputSpecs_WithOutputSpecs()
        {
            var query = Query<Order>.Build(config => config
                .Where(order => order.Freight == 0)
                .OrderBy(order => order.OrderDate)
                .Select(order => order.Customer)
                .Output(output => output.Distinct().OrderBy(customer => customer.Country).ThenBy(customer => customer.CompanyName)));

            Assert.Equal(2, query.InputSpecifications.Count);
            Assert.Equal(3, query.OutputSpecifications.Count);

            Assert.IsAssignableFrom<ISelectQuerySpecification<Order, Customer>>(query.TransformSpecification);
        }

    }
}