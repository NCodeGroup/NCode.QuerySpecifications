using System;
using System.Collections.Generic;
using System.Linq;
using NCode.QuerySpecifications.Builder.Factories;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Builder.Transforms;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder
{
    public interface IQueryBuilder
    {
        IQueryPipe<TEntity> Build<TEntity>(IQueryConfiguration<TEntity> configuration)
            where TEntity : class;

        IQueryTransform<TIn, TOut> Build<TIn, TOut>(ITransformConfiguration<TIn, TOut> configuration)
            where TIn : class
            where TOut : class;
    }

    public class QueryBuilder : IQueryBuilder
    {
        private readonly ICompositeQueryFactory _compositeQueryFactory;

        public QueryBuilder(ICompositeQueryFactory compositeQueryFactory)
        {
            _compositeQueryFactory = compositeQueryFactory ?? throw new ArgumentNullException(nameof(compositeQueryFactory));
        }

        private IQueryPipe<TEntity> CreatePipe<TEntity>(IQuerySpecification<TEntity> specification)
            where TEntity : class
        {
            if (_compositeQueryFactory.TryCreate(specification, out var pipe))
            {
                return pipe;
            }

            throw new InvalidOperationException("TODO");
        }

        private IQueryPipe<TEntity> CreateChain<TEntity>(IEnumerable<IQuerySpecification<TEntity>> specifications)
            where TEntity : class
        {
            var pipes = specifications.Select(CreatePipe);

            return new CompositeQueryPipe<TEntity>(pipes);
        }

        public IQueryPipe<TEntity> Build<TEntity>(IQueryConfiguration<TEntity> configuration)
            where TEntity : class
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            return CreateChain(configuration.OutputSpecifications);
        }

        public IQueryTransform<TIn, TOut> Build<TIn, TOut>(ITransformConfiguration<TIn, TOut> configuration)
            where TIn : class
            where TOut : class
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (!_compositeQueryFactory.TryCreate(configuration.TransformSpecification, out var transform))
                throw new InvalidOperationException("TODO");

            var inputPipe = CreateChain(configuration.InputSpecifications);

            var outputPipe = CreateChain(configuration.OutputSpecifications);

            var chain = new ChainQueryTransform<TIn, TOut>(inputPipe, outputPipe, transform);

            return chain;
        }

    }
}