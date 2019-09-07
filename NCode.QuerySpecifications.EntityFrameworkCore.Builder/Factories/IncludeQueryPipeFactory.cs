using System;
using NCode.QuerySpecifications.Builder.Factories;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes;
using NCode.QuerySpecifications.EntityFrameworkCore.Specifications;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Factories
{
    public class IncludeQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => EntityFrameworkCoreQueryNames.Include;

        public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class
        {
            switch (specification)
            {
                case IIncludePathQuerySpecification<TEntity> includePathSpec:
                    {
                        queryPipe = new IncludePathQueryPipe<TEntity>(includePathSpec.NavigationPropertyPath);
                        return true;
                    }

                case IIncludePropertyQuerySpecification<TEntity> includePropertySpec when includePropertySpec.IsRoot:
                    {
                        var factoryType = typeof(PropertyFactory<>).MakeGenericType(includePropertySpec.OutputPropertyType);
                        var factory = (IQueryPipeFactory)Activator.CreateInstance(factoryType);
                        return factory.TryCreate(specification, out queryPipe);
                    }

                case IIncludePropertyQuerySpecification<TEntity> includePropertySpec:
                    {
                        var factoryType = typeof(ThenFactory<,>).MakeGenericType(includePropertySpec.InputPropertyType, includePropertySpec.OutputPropertyType);
                        var factory = (IQueryPipeFactory)Activator.CreateInstance(factoryType);
                        return factory.TryCreate(specification, out queryPipe);
                    }
            }

            queryPipe = null;
            return false;
        }

        private class PropertyFactory<TProperty> : IQueryPipeFactory
        {
            public string Name => EntityFrameworkCoreQueryNames.Include;

            public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
                where TEntity : class
            {
                if (specification is IIncludePropertyQuerySpecification<TEntity, TEntity, TProperty> includePropertySpec)
                {
                    queryPipe = new IncludePropertyQueryPipe<TEntity, TProperty>(includePropertySpec.NavigationPropertyPath);
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
                if (specification is IIncludePropertyQuerySpecification<TEntity, TInputProperty, TOutputProperty> includePropertySpec)
                {
                    queryPipe = new IncludePropertyThenQueryPipe<TEntity, TInputProperty, TOutputProperty>(includePropertySpec.NavigationPropertyPath, includePropertySpec.IsEnumerable);
                    return true;
                }

                queryPipe = null;
                return false;
            }
        }

    }
}