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

            using var fixture = new NorthwindQueryInMemoryFixture<NoopModelCustomizer>();
            using var context = fixture.CreateContext();

            var results = pipe.Apply(context.Customers).ToList();

            var expectedCount = fixture.QueryAsserter.ExpectedData.Set<Customer>().Count();
            Assert.NotEqual(0, expectedCount);

            Assert.Equal(expectedCount, results.Count);
            Assert.Empty(context.ChangeTracker.Entries());
        }
    }
}