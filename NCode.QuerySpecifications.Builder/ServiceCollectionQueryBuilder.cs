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

        IServiceCollectionQueryBuilder AddPipeTransformFactory<TFactory>()
            where TFactory : class, IQueryPipeTransformFactory;
    }

    public class ServiceCollectionQueryBuilder : IServiceCollectionQueryBuilder
    {
        public ServiceCollectionQueryBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
        }

        public IServiceCollection ServiceCollection { get; }

        public IServiceCollectionQueryBuilder AddPipeFactory<TFactory>()
            where TFactory : class, IQueryPipeFactory
        {
            ServiceCollection.TryAddEnumerable(ServiceDescriptor.Transient<IQueryPipeFactory, TFactory>());
            return this;
        }

        public IServiceCollectionQueryBuilder AddPipeTransformFactory<TFactory>()
            where TFactory : class, IQueryPipeTransformFactory
        {
            ServiceCollection.TryAddEnumerable(ServiceDescriptor.Transient<IQueryPipeTransformFactory, TFactory>());
            return this;
        }

    }
}