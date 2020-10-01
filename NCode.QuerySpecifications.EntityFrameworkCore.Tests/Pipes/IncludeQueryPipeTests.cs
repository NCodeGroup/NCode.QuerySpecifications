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

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using NCode.QuerySpecifications.EntityFrameworkCore.Pipes;
using NCode.QuerySpecifications.TestUtilities;
using Xunit;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Tests.Pipes
{
    public class IncludeQueryPipeTests
    {
        [Fact]
        public void Apply_GivenPath_ThenNotNull()
        {
            var pipe = new IncludePathQueryPipe<Order>(nameof(Order.Customer));

            using var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>();
            using var context = fixture.CreateContext();

            var queryRoot = context.Set<Order>();

            var before = queryRoot;
            Assert.NotEmpty(before);
            Assert.All(before, order => Assert.Null(order.Customer));

            var after = pipe.Apply(queryRoot).ToList();
            Assert.NotEmpty(after);
            Assert.All(after, order => Assert.NotNull(order.Customer));
        }

        [Fact]
        public void Apply_GivenProperty_ThenNotNull()
        {
            var pipe = new IncludePropertyRootQueryPipe<Order, Customer>(order => order.Customer);

            using var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>();
            using var context = fixture.CreateContext();

            var queryRoot = context.Set<Order>();

            var before = queryRoot;
            Assert.NotEmpty(before);
            Assert.All(before, order => Assert.Null(order.Customer));

            var after = pipe.Apply(queryRoot).ToList();
            Assert.NotEmpty(after);
            Assert.All(after, order => Assert.NotNull(order.Customer));
        }

        [Fact]
        public void Apply_GivenPropertyWithObject_ThenNotNull()
        {
            var pipe = new IncludePropertyThenQueryPipe<OrderDetail, Order, Customer>(order => order.Customer);

            using var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>();
            using var context = fixture.CreateContext();

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

        [Fact]
        public void Apply_GivenPropertyWithCollection_ThenNotNull()
        {
            var pipe = new IncludePropertyThenQueryPipe<Product, OrderDetail, Order>(detail => detail.Order);

            using var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>();
            using var context = fixture.CreateContext();

            var queryRoot = context.Set<Product>().Include(product => product.OrderDetails);

            var before = queryRoot;
            Assert.NotEmpty(before);
            Assert.All(before, product =>
            {
                Assert.NotNull(product.OrderDetails);
                Assert.All(product.OrderDetails, detail =>
                {
                    Assert.Null(detail.Order);
                });
            });

            var after = pipe.Apply(queryRoot).ToList();
            Assert.NotEmpty(after);
            Assert.All(after, product =>
            {
                Assert.NotNull(product.OrderDetails);
                Assert.All(product.OrderDetails, detail =>
                {
                    Assert.NotNull(detail.Order);
                });
            });
        }

    }
}