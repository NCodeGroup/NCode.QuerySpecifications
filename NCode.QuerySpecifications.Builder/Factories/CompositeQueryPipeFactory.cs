using System.Collections.Generic;
using System.Linq;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public interface ICompositeQueryPipeFactory : IQueryPipeFactory, IQueryPipeTransformFactory
    {
        // nothing
    }

    public class CompositeQueryPipeFactory : ICompositeQueryPipeFactory
    {
        private readonly IReadOnlyDictionary<string, IQueryPipeFactory[]> _pipeFactories;
        private readonly IReadOnlyDictionary<string, IQueryPipeTransformFactory[]> _transformFactories;

        public string Name => "Composite";

        public CompositeQueryPipeFactory(IEnumerable<IQueryPipeFactory> pipeFactories, IEnumerable<IQueryPipeTransformFactory> transformFactories)
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

        public bool TryCreate<TIn, TOut>(IQuerySpecification<TIn, TOut> specification, out IQueryPipe<TIn, TOut> pipe)
            where TIn : class
            where TOut : class
        {
            if (_transformFactories.TryGetValue(specification.Name, out var factories))
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

    }
}