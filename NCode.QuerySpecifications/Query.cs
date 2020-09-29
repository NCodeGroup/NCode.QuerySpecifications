using System;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Configurators;

namespace NCode.QuerySpecifications
{
    public static class Query<TEntity>
        where TEntity : class
    {
        public static IQueryConfiguration<TEntity> Configure(Func<IQueryConfigurator<TEntity>, IQueryConfigurator<TEntity>> configureCallback)
        {
            if (configureCallback == null)
                throw new ArgumentNullException(nameof(configureCallback));

            var inputConfigurator = new QueryConfigurator<TEntity>();

            var outputConfigurator = configureCallback(inputConfigurator);

            return outputConfigurator.OutputConfiguration;
        }

        public static ITransformConfiguration<TEntity, TOut> Configure<TOut>(Func<IQueryConfigurator<TEntity>, ITransformConfigurator<TEntity, TOut>> configureCallback)
            where TOut : class
        {
            if (configureCallback == null)
                throw new ArgumentNullException(nameof(configureCallback));

            var inputConfigurator = new QueryConfigurator<TEntity>();

            var transformConfigurator = configureCallback(inputConfigurator);

            return transformConfigurator.TransformConfiguration;
        }

    }
}