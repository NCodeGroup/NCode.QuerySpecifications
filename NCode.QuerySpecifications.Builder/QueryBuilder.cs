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
using System.Collections.Generic;
using System.Linq;
using NCode.QuerySpecifications.Builder.Factories;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder
{
    public interface IQueryBuilder
    {
        IQueryPipe<TEntity> Build<TEntity>(IQueryConfiguration<TEntity> queryConfiguration)
            where TEntity : class;

        IQueryPipe<TIn, TOut> Build<TIn, TOut>(IQueryConfiguration<TIn, TOut> queryConfiguration)
            where TIn : class
            where TOut : class;
    }

    public class QueryBuilder : IQueryBuilder
    {
        private readonly ICompositeQueryPipeFactory _compositeQueryPipeFactory;

        public QueryBuilder(ICompositeQueryPipeFactory compositeQueryPipeFactory)
        {
            _compositeQueryPipeFactory = compositeQueryPipeFactory ?? throw new ArgumentNullException(nameof(compositeQueryPipeFactory));
        }

        public IQueryPipe<TEntity> Build<TEntity>(IQueryConfiguration<TEntity> queryConfiguration)
            where TEntity : class
        {
            if (queryConfiguration == null)
                throw new ArgumentNullException(nameof(queryConfiguration));

            return CreateCompositePipe(queryConfiguration.OutputSpecifications);
        }

        public IQueryPipe<TIn, TOut> Build<TIn, TOut>(IQueryConfiguration<TIn, TOut> queryConfiguration)
            where TIn : class
            where TOut : class
        {
            if (queryConfiguration == null)
                throw new ArgumentNullException(nameof(queryConfiguration));

            if (!_compositeQueryPipeFactory.TryCreate(queryConfiguration.TransformSpecification, out var transformPipe))
                throw new InvalidOperationException("TODO");

            var inputPipe = CreateCompositePipe(queryConfiguration.InputSpecifications);

            var outputPipe = CreateCompositePipe(queryConfiguration.OutputSpecifications);

            return new CompositeQueryPipeTransform<TIn, TOut>(inputPipe, outputPipe, transformPipe);
        }

        private IQueryPipe<TEntity> CreatePipe<TEntity>(IQuerySpecification<TEntity> specification)
            where TEntity : class
        {
            if (_compositeQueryPipeFactory.TryCreate(specification, out var pipe))
            {
                return pipe;
            }

            throw new InvalidOperationException("TODO");
        }

        private IQueryPipe<TEntity> CreateCompositePipe<TEntity>(IEnumerable<IQuerySpecification<TEntity>> specifications)
            where TEntity : class
        {
            var pipes = specifications.Select(CreatePipe);

            return new CompositeQueryPipe<TEntity>(pipes);
        }
    }
}