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

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            switch (specification)
            {
                case IIncludePathQuerySpecification<TEntity> includePathSpec:
                    {
                        pipe = new IncludePathQueryPipe<TEntity>(includePathSpec.NavigationPropertyPath);
                        return true;
                    }

                case IIncludePropertyQuerySpecification<TEntity> includePropertySpec when includePropertySpec.IsRoot:
                    {
                        var factoryType = typeof(RootFactory<>).MakeGenericType(includePropertySpec.OutputPropertyType);
                        var factory = (IQueryPipeFactory)Activator.CreateInstance(factoryType);
                        return factory.TryCreate(specification, out pipe);
                    }

                case IIncludePropertyQuerySpecification<TEntity> includePropertySpec:
                    {
                        var factoryType = typeof(ThenFactory<,>).MakeGenericType(includePropertySpec.InputPropertyType, includePropertySpec.OutputPropertyType);
                        var factory = (IQueryPipeFactory)Activator.CreateInstance(factoryType);
                        return factory.TryCreate(specification, out pipe);
                    }
            }

            pipe = null;
            return false;
        }

        private class RootFactory<TProperty> : IQueryPipeFactory
        {
            public string Name => EntityFrameworkCoreQueryNames.Include;

            public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
                where TEntity : class
            {
                if (specification is IIncludePropertyQuerySpecification<TEntity, TEntity, TProperty> includePropertySpec)
                {
                    pipe = new IncludePropertyRootQueryPipe<TEntity, TProperty>(includePropertySpec.NavigationPropertyPath);
                    return true;
                }

                pipe = null;
                return false;
            }
        }

        private class ThenFactory<TInputProperty, TOutputProperty> : IQueryPipeFactory
        {
            public string Name => EntityFrameworkCoreQueryNames.Include;

            public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
                where TEntity : class
            {
                if (specification is IIncludePropertyQuerySpecification<TEntity, TInputProperty, TOutputProperty> includePropertySpec)
                {
                    pipe = new IncludePropertyThenQueryPipe<TEntity, TInputProperty, TOutputProperty>(includePropertySpec.NavigationPropertyPath, includePropertySpec.IsEnumerable);
                    return true;
                }

                pipe = null;
                return false;
            }
        }

    }
}