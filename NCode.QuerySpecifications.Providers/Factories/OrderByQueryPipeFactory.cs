using System;
using NCode.QuerySpecifications.Provider.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Provider.Factories
{
    public class OrderByQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.OrderBy;

        public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class
        {
            if (specification is IOrderByQuerySpecification<TEntity> orderBySpec)
            {
                var factoryType = typeof(Factory<>).MakeGenericType(orderBySpec.PropertyType);
                var factory = (IQueryPipeFactory)Activator.CreateInstance(factoryType);
                return factory.TryCreate(specification, out queryPipe);
            }

            queryPipe = null;
            return false;
        }

        private class Factory<TProperty> : IQueryPipeFactory
        {
            public string Name => QueryNames.OrderBy;

            public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
                where TEntity : class
            {
                if (specification is IOrderByQuerySpecification<TEntity, TProperty> orderBySpec)
                {
                    queryPipe = new OrderByQueryPipe<TEntity, TProperty>(orderBySpec.KeySelector, orderBySpec.Comparer, orderBySpec.Descending);
                    return true;
                }

                queryPipe = null;
                return false;
            }
        }

    }
}