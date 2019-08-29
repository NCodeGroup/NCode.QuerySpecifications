using System;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Configurators;

namespace NCode.QuerySpecifications
{
    public static class Query<TEntity>
        where TEntity : class
    {
        public static IQueryConfiguration<TEntity> Build(Func<IQueryConfigurator<TEntity>, IQueryConfigurator<TEntity>> callback)
        {
            var input = new QueryConfiguration<TEntity>();

            var output = callback(input);

            return output.OutputConfiguration;
        }

        public static ITransformConfiguration<TEntity, TOut> Build<TOut>(Func<IQueryConfigurator<TEntity>, ITransformConfigurator<TEntity, TOut>> callback)
            where TOut : class
        {
            var input = new QueryConfiguration<TEntity>();

            var output = callback(input);

            return output.TransformConfiguration;
        }

    }
}