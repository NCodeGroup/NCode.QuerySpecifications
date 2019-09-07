using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NCode.QuerySpecifications.Builder.Factories;

namespace NCode.QuerySpecifications.Builder
{
    public interface IServiceCollectionQueryBuilder
    {
        IServiceCollectionQueryBuilder AddPipeFactory<TFactory>()
            where TFactory : class, IQueryPipeFactory;

        IServiceCollectionQueryBuilder AddTransformFactory<TFactory>()
            where TFactory : class, IQueryTransformFactory;
    }

    public class ServiceCollectionQueryBuilder : IServiceCollectionQueryBuilder
    {
        public ServiceCollectionQueryBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
        }

        public IServiceCollection ServiceCollection { get; }

        public virtual IServiceCollectionQueryBuilder AddPipeFactory<TFactory>()
            where TFactory : class, IQueryPipeFactory
        {
            ServiceCollection.TryAddEnumerable(ServiceDescriptor.Transient<IQueryPipeFactory, TFactory>());
            return this;
        }

        public virtual IServiceCollectionQueryBuilder AddTransformFactory<TFactory>()
            where TFactory : class, IQueryTransformFactory
        {
            ServiceCollection.TryAddEnumerable(ServiceDescriptor.Transient<IQueryTransformFactory, TFactory>());
            return this;
        }

    }
}