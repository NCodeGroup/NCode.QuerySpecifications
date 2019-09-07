using System;
using NCode.QuerySpecifications.Builder;
using NCode.QuerySpecifications.EntityFrameworkCore.Builder.Factories;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder
{
    public static class ServiceCollectionQueryBuilderExtensions
    {
        public static IServiceCollectionQueryBuilder AddEntityFrameworkCore(this IServiceCollectionQueryBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.AddPipeFactory<AsNoTrackingQueryPipeFactory>();
            builder.AddPipeFactory<AsTrackingQueryPipeFactory>();
            builder.AddPipeFactory<TagWithQueryPipeFactory>();
            builder.AddPipeFactory<IgnoreQueryFiltersQueryPipeFactory>();
            builder.AddPipeFactory<IncludeQueryPipeFactory>();

            return builder;
        }

    }
}