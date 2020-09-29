using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NCode.QuerySpecifications.Builder.Factories;

namespace NCode.QuerySpecifications.Builder
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryBuilder(this IServiceCollection services, Action<IServiceCollectionQueryBuilder> configureCallback)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddTransient<IQueryBuilder, QueryBuilder>();
            services.TryAddTransient<ICompositeQueryPipeFactory, CompositeQueryPipeFactory>();

            var builder = new ServiceCollectionQueryBuilder(services);

            configureCallback(builder);

            // IQueryPipeFactory
            builder.AddPipeFactory<WhereQueryPipeFactory>();
            builder.AddPipeFactory<PageQueryPipeFactory>();
            builder.AddPipeFactory<OrderByQueryPipeFactory>();
            builder.AddPipeFactory<DistinctQueryPipeFactory>();

            // IQueryPipeTransformFactory
            builder.AddPipeTransformFactory<SelectQueryPipeFactory>();

            return services;
        }

    }
}