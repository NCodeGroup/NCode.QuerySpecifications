﻿using System;
using System.Collections.Generic;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Provider.Factories;
using NCode.QuerySpecifications.Provider.Pipes;
using NCode.QuerySpecifications.Provider.Transforms;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Provider
{
    public class QueryBuilder : IQueryBuilder
    {
        private readonly ICompositeQueryFactory _compositeQueryFactory;

        public QueryBuilder(ICompositeQueryFactory compositeQueryFactory)
        {
            _compositeQueryFactory = compositeQueryFactory ?? throw new ArgumentNullException(nameof(compositeQueryFactory));
        }

        public virtual IQueryPipe<TEntity> Build<TEntity>(IQueryConfiguration<TEntity> configuration)
            where TEntity : class
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            return BuildPipe(configuration.OutputSpecifications);
        }

        private IQueryPipe<TEntity> BuildPipe<TEntity>(IEnumerable<IQuerySpecification<TEntity>> specifications)
            where TEntity : class
        {
            IQueryPipe<TEntity> chain = new IdentityQueryPipe<TEntity>();

            BuildPipe(chain, specifications);

            return chain;
        }

        private void BuildPipe<TEntity>(IQueryPipe<TEntity> chain, IEnumerable<IQuerySpecification<TEntity>> specifications)
            where TEntity : class
        {
            foreach (var specification in specifications)
            {
                if (_compositeQueryFactory.TryCreate(specification, out var next))
                {
                    chain = new ChainQueryPipe<TEntity>(chain, next);
                }
                else
                {
                    throw new InvalidOperationException("TODO");
                }

                if (specification is IQueryConfiguration<TEntity> children)
                {
                    BuildPipe(chain, children.OutputSpecifications);
                }
            }
        }

        public virtual IQueryTransform<TIn, TOut> Build<TIn, TOut>(ITransformConfiguration<TIn, TOut> configuration)
            where TIn : class
            where TOut : class
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (!_compositeQueryFactory.TryCreate(configuration.TransformSpecification, out var transform))
                throw new InvalidOperationException("TODO");

            var inputPipe = BuildPipe(configuration.InputSpecifications);

            var outputPipe = BuildPipe(configuration.OutputSpecifications);

            var chain = new ChainQueryTransform<TIn, TOut>(inputPipe, outputPipe, transform);

            return chain;
        }

    }
}