using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Configurators;

namespace NCode.QuerySpecifications.EntityFrameworkCore
{
    public static class QueryConfiguratorExtensions
    {
        public static IQueryConfigurator<TEntity> AsNoTracking<TEntity>(this IQueryConfigurator<TEntity> configurator)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new AsNoTrackingQuerySpecification<TEntity>();
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<TEntity> AsTracking<TEntity>(this IQueryConfigurator<TEntity> configurator)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new AsTrackingQuerySpecification<TEntity>();
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IIncludeQueryConfigurator<TEntity, TProperty> Include<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var nextConfigurator = new IncludeQueryConfigurator<TEntity, TEntity, TProperty>(configurator.OutputConfiguration, navigationPropertyPath, true, false);
            configurator.AddSpecification(nextConfigurator);

            return nextConfigurator;
        }

        public static IIncludeQueryConfigurator<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludeQueryConfigurator<TEntity, TPreviousProperty> configurator, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TEntity : class
            where TPreviousProperty : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var nextConfigurator = new IncludeQueryConfigurator<TEntity, TPreviousProperty, TProperty>(configurator.OutputConfiguration, navigationPropertyPath, false, false);
            configurator.AddSpecification(nextConfigurator);

            return nextConfigurator;
        }

        public static IIncludeQueryConfigurator<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludeQueryConfigurator<TEntity, IEnumerable<TPreviousProperty>> configurator, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var nextConfigurator = new IncludeQueryConfigurator<TEntity, TPreviousProperty, TProperty>(configurator.OutputConfiguration, navigationPropertyPath, false, true);
            configurator.AddSpecification(nextConfigurator);

            return nextConfigurator;
        }

    }
}