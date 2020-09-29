using System.Collections.Generic;
using System.Linq;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Builder.Transforms;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class CompositeQueryFactory : ICompositeQueryFactory
    {
        private readonly IReadOnlyDictionary<string, IQueryPipeFactory[]> _pipeFactories;
        private readonly IReadOnlyDictionary<string, IQueryTransformFactory[]> _transformFactories;

        public string Name => "Composite";

        public CompositeQueryFactory(IEnumerable<IQueryPipeFactory> pipeFactories, IEnumerable<IQueryTransformFactory> transformFactories)
        {
            _pipeFactories = pipeFactories
                .GroupBy(factory => factory.Name)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.ToArray());

            _transformFactories = transformFactories
                .GroupBy(factory => factory.Name)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.ToArray());
        }

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (_pipeFactories.TryGetValue(specification.Name, out var factories))
            {
                foreach (var factory in factories)
                {
                    if (factory.TryCreate(specification, out pipe))
                    {
                        return true;
                    }
                }
            }

            pipe = null;
            return false;
        }

        public bool TryCreate<TIn, TOut>(ITransformSpecification<TIn, TOut> specification, out IQueryTransform<TIn, TOut> transform)
            where TIn : class
            where TOut : class
        {
            if (_transformFactories.TryGetValue(specification.Name, out var factories))
            {
                foreach (var factory in factories)
                {
                    if (factory.TryCreate(specification, out transform))
                    {
                        return true;
                    }
                }
            }

            transform = null;
            return false;
        }

    }
}