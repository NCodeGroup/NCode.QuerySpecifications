using System;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class OrderByQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.OrderBy;

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (specification is IOrderByQuerySpecification<TEntity> orderBySpec)
            {
                var factoryType = typeof(Factory<>).MakeGenericType(orderBySpec.PropertyType);
                var factory = (IQueryPipeFactory)Activator.CreateInstance(factoryType);
                return factory.TryCreate(specification, out pipe);
            }

            pipe = null;
            return false;
        }

        private class Factory<TProperty> : IQueryPipeFactory
        {
            public string Name => QueryNames.OrderBy;

            public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
                where TEntity : class
            {
                if (specification is IOrderByQuerySpecification<TEntity, TProperty> orderBySpec)
                {
                    pipe = new OrderByQueryPipe<TEntity, TProperty>(orderBySpec.KeySelector, orderBySpec.Comparer, orderBySpec.Descending, orderBySpec.IsRoot);
                    return true;
                }

                pipe = null;
                return false;
            }
        }

    }
}