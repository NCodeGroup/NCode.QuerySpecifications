using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NCode.QuerySpecifications.Builder.Factories;

namespace NCode.QuerySpecifications.Builder
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollectionQueryBuilder AddQueryBuilder(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddTransient<IQueryBuilder, QueryBuilder>();
            services.TryAddTransient<ICompositeQueryFactory, CompositeQueryFactory>();

            var builder = new ServiceCollectionQueryBuilder(services);

            // IQueryPipeFactory
            builder.AddPipeFactory<WhereQueryPipeFactory>();
            builder.AddPipeFactory<PageQueryPipeFactory>();
            builder.AddPipeFactory<OrderByQueryPipeFactory>();
            builder.AddPipeFactory<DistinctQueryPipeFactory>();

            // IQueryTransformFactory
            builder.AddTransformFactory<SelectQueryTransformFactory>();

            return builder;
        }

    }
}