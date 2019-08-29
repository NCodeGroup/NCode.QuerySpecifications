using System;
using NCode.QuerySpecifications.EntityFrameworkCore;
using NCode.QuerySpecifications.Provider.Factories;
using NCode.QuerySpecifications.Provider.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Providers.EntityFrameworkCore
{
    public class IncludeQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => EntityFrameworkCoreQueryNames.Include;

        public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class
        {
            if (specification is IIncludeQuerySpecification<TEntity> includeSpec)
            {
                if (includeSpec.IsRoot)
                {
                    var factoryTypeRoot = typeof(RootFactory<>).MakeGenericType(includeSpec.OutputPropertyType);
                    var factoryRoot = (IQueryPipeFactory)Activator.CreateInstance(factoryTypeRoot);
                    return factoryRoot.TryCreate(specification, out queryPipe);
                }

                var factoryTypeThen = typeof(ThenFactory<,>).MakeGenericType(includeSpec.InputPropertyType, includeSpec.OutputPropertyType);
                var factoryThen = (IQueryPipeFactory)Activator.CreateInstance(factoryTypeThen);
                return factoryThen.TryCreate(specification, out queryPipe);
            }

            queryPipe = null;
            return false;
        }

        private class RootFactory<TProperty> : IQueryPipeFactory
        {
            public string Name => EntityFrameworkCoreQueryNames.Include;

            public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
                where TEntity : class
            {
                if (specification is IIncludeQuerySpecification<TEntity, TEntity, TProperty> includeSpec)
                {
                    queryPipe = new IncludeQueryPipe<TEntity, TProperty>(includeSpec.NavigationPropertyPath);
                    return true;
                }

                queryPipe = null;
                return false;
            }
        }

        private class ThenFactory<TInputProperty, TOutputProperty> : IQueryPipeFactory
        {
            public string Name => EntityFrameworkCoreQueryNames.Include;

            public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
                where TEntity : class
            {
                if (specification is IIncludeQuerySpecification<TEntity, TInputProperty, TOutputProperty> includeSpec)
                {
                    queryPipe = new ThenIncludeQueryPipe<TEntity, TInputProperty, TOutputProperty>(includeSpec.NavigationPropertyPath, includeSpec.IsEnumerable);
                    return true;
                }

                queryPipe = null;
                return false;
            }
        }

    }
}