using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.EntityFrameworkCore.Configurators;
using NCode.QuerySpecifications.EntityFrameworkCore.Specifications;

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

        public static IQueryConfigurator<TEntity> IgnoreQueryFilters<TEntity>(this IQueryConfigurator<TEntity> configurator)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new IgnoreQueryFiltersQuerySpecification<TEntity>();
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<TEntity> TagWith<TEntity>(this IQueryConfigurator<TEntity> configurator, string tag)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            var specification = new TagWithQuerySpecification<TEntity>(tag);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<TEntity> Include<TEntity>(this IQueryConfigurator<TEntity> configurator, string navigationPropertyPath)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePathQuerySpecification<TEntity>(navigationPropertyPath);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IIncludableQueryConfigurator<TEntity, TProperty> Include<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePropertyQuerySpecification<TEntity, TEntity, TProperty>(navigationPropertyPath, true, false);
            configurator.AddSpecification(specification);

            var nextConfigurator = new IncludableQueryConfigurator<TEntity, TProperty>(configurator);
            return nextConfigurator;
        }

        public static IIncludableQueryConfigurator<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludableQueryConfigurator<TEntity, TPreviousProperty> configurator, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TEntity : class
            where TPreviousProperty : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePropertyQuerySpecification<TEntity, TPreviousProperty, TProperty>(navigationPropertyPath, false, false);
            configurator.AddSpecification(specification);

            var nextConfigurator = new IncludableQueryConfigurator<TEntity, TProperty>(configurator);
            return nextConfigurator;
        }

        public static IIncludableQueryConfigurator<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludableQueryConfigurator<TEntity, IEnumerable<TPreviousProperty>> configurator, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePropertyQuerySpecification<TEntity, TPreviousProperty, TProperty>(navigationPropertyPath, false, true);
            configurator.AddSpecification(specification);

            var nextConfigurator = new IncludableQueryConfigurator<TEntity, TProperty>(configurator);
            return nextConfigurator;
        }

    }
}