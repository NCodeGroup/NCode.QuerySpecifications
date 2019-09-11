using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NCode.QuerySpecifications.Builder.Factories;

namespace NCode.QuerySpecifications.Builder
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryBuilder(this IServiceCollection services, Action<IServiceCollectionQueryBuilder> callback)
        {
	        if (services == null)
		        throw new ArgumentNullException(nameof(services));

	        services.TryAddTransient<IQueryBuilder, QueryBuilder>();
	        services.TryAddTransient<ICompositeQueryFactory, CompositeQueryFactory>();

	        IServiceCollectionQueryBuilder builder = new ServiceCollectionQueryBuilder(services);

	        callback(builder);

	        // IQueryPipeFactory
	        builder.AddPipeFactory<WhereQueryPipeFactory>();
	        builder.AddPipeFactory<PageQueryPipeFactory>();
	        builder.AddPipeFactory<OrderByQueryPipeFactory>();
	        builder.AddPipeFactory<DistinctQueryPipeFactory>();

	        // IQueryTransformFactory
	        builder.AddTransformFactory<SelectQueryTransformFactory>();

	        return services;
        }

	}
}