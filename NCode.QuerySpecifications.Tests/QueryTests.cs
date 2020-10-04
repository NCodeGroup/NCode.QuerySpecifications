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

using System;
using System.Linq;
using Moq;
using NCode.QuerySpecifications.Pipes;
using NCode.QuerySpecifications.Specifications;
using Xunit;

namespace NCode.QuerySpecifications.Tests
{
    public class QueryTests : IDisposable
    {
        private readonly MockRepository _mockRepository;

        private static readonly IQueryable<string> QueryRoot = new[]
        {
            "1",
            "aa",
            "AA",
            "2",
            "bb",
            "BB",
            "3",
            "cc",
            "CC",
            "1",
            "2",
            "3",
        }.AsQueryable();

        public QueryTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
        }

        public void Dispose()
        {
            _mockRepository.Verify();
        }

        [Fact]
        public void Build_UseQuerySpecification()
        {
            var mockQueryPipe = _mockRepository.Create<IQueryPipe<string, string>>();
            var mockQuerySpecification = _mockRepository.Create<IQuerySpecification<string, string>>();

            mockQueryPipe
                .Setup(_ => _.Apply(QueryRoot))
                .Returns(QueryRoot)
                .Verifiable();

            mockQuerySpecification
                .Setup(_ => _.Build())
                .Returns(mockQueryPipe.Object)
                .Verifiable();

            var queryPipe = Query<string>.Build(config => config
                .UseQuerySpecification(mockQuerySpecification.Object));

            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(QueryRoot, query);
        }

        [Fact]
        public void Build_UseTransformSpecification()
        {
            var mockQueryPipe = _mockRepository.Create<IQueryPipe<string, string>>();
            var mockQuerySpecification = _mockRepository.Create<IQuerySpecification<string, string>>();

            mockQueryPipe
                .Setup(_ => _.Apply(QueryRoot))
                .Returns(QueryRoot)
                .Verifiable();

            mockQuerySpecification
                .Setup(_ => _.Build())
                .Returns(mockQueryPipe.Object)
                .Verifiable();

            var queryPipe = Query<string>.Build(config => config
                .UseTransformSpecification(mockQuerySpecification.Object));

            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(QueryRoot, query);
        }

        [Fact]
        public void Build_UseQuery()
        {
            var mockQueryPipe = _mockRepository.Create<IQueryPipe<string, string>>();

            mockQueryPipe
                .Setup(_ => _.Apply(QueryRoot))
                .Returns(QueryRoot)
                .Verifiable();

            var queryPipe = Query<string>.Build(config => config
                .UseQuery(mockQueryPipe.Object));

            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(QueryRoot, query);
        }

        [Fact]
        public void Build_UseTransform()
        {
            var mockQueryPipe = _mockRepository.Create<IQueryPipe<string, string>>();

            mockQueryPipe
                .Setup(_ => _.Apply(QueryRoot))
                .Returns(QueryRoot)
                .Verifiable();

            var queryPipe = Query<string>.Build(config => config
                .UseTransform(mockQueryPipe.Object));

            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(QueryRoot, query);
        }

        [Fact]
        public void Build_Where()
        {
            var queryPipe = Query<string>.Build(config => config
                .Where(value => value.Length == 1));

            var expected = new[] { "1", "2", "3", "1", "2", "3" };
            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_OrderByAscending_WithoutComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .OrderBy(value => value));

            var expected = new[] { "1", "1", "2", "2", "3", "3", "aa", "AA", "bb", "BB", "cc", "CC" };
            var query = queryPipe.Apply(QueryRoot.Reverse());
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_OrderByAscending_WithComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .OrderBy(value => value, StringComparer.OrdinalIgnoreCase));

            var expected = new[] { "1", "1", "2", "2", "3", "3", "AA", "aa", "BB", "bb", "CC", "cc" };
            var query = queryPipe.Apply(QueryRoot.Reverse());
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_OrderByDescending_WithoutComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .OrderByDescending(value => value));

            var expected = new[] { "CC", "cc", "BB", "bb", "AA", "aa", "3", "3", "2", "2", "1", "1" };
            var query = queryPipe.Apply(QueryRoot.Reverse());
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_OrderByDescending_WithComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .OrderByDescending(value => value, StringComparer.OrdinalIgnoreCase));

            var expected = new[] { "cc", "CC", "bb", "BB", "aa", "AA", "3", "3", "2", "2", "1", "1" };
            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByAscending_WithoutComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .OrderBy(value => value)
                .ThenBy(value => value));

            var expected = new[] { "1", "1", "2", "2", "3", "3", "aa", "AA", "bb", "BB", "cc", "CC" };
            var query = queryPipe.Apply(QueryRoot.Reverse());
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByAscending_WithComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .OrderBy(value => value)
                .ThenBy(value => value, StringComparer.OrdinalIgnoreCase));

            var expected = new[] { "1", "1", "2", "2", "3", "3", "aa", "AA", "bb", "BB", "cc", "CC" };
            var query = queryPipe.Apply(QueryRoot.Reverse());
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByDescending_WithoutComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .OrderBy(value => value)
                .ThenByDescending(value => value));

            var expected = new[] { "1", "1", "2", "2", "3", "3", "aa", "AA", "bb", "BB", "cc", "CC" };
            var query = queryPipe.Apply(QueryRoot.Reverse());
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_OrderByAscending_ThenByDescending_WithComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .OrderBy(value => value)
                .ThenByDescending(value => value, StringComparer.OrdinalIgnoreCase));

            var expected = new[] { "1", "1", "2", "2", "3", "3", "aa", "AA", "bb", "BB", "cc", "CC" };
            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_Page()
        {
            var queryPipe = Query<string>.Build(config => config
                .Page(2, 2));

            var expected = new[] { "AA", "2" };
            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_Distinct_WithoutComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .Distinct());

            var expected = new[] { "1", "aa", "AA", "2", "bb", "BB", "3", "cc", "CC" };
            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_Distinct_WithComparer()
        {
            var queryPipe = Query<string>.Build(config => config
                .Distinct(StringComparer.OrdinalIgnoreCase));

            var expected = new[] { "1", "aa", "2", "bb", "3", "cc" };
            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_Select_WithoutInputSpecs_WithoutOutputSpecs()
        {
            var queryPipe = Query<string>.Build(config => config
                .Select(value => value.ToUpperInvariant()));

            var expected = new[] { "1", "AA", "AA", "2", "BB", "BB", "3", "CC", "CC", "1", "2", "3" };
            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(expected, query);
        }

        [Fact]
        public void Build_Select_WithInputSpecs_WithOutputSpecs()
        {
            var queryPipe = Query<string>.Build(config => config
                .Where(value => value.Length > 1)
                .Select(value => value.ToUpperInvariant())
                .Output(output => output.Distinct()));

            var expected = new[] { "AA", "BB", "CC" };
            var query = queryPipe.Apply(QueryRoot);
            Assert.Equal(expected, query);
        }

    }
}