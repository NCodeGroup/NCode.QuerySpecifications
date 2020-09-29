using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications
{
    public static class QueryConfiguratorExtensions
    {
        public static IQueryConfigurator<TEntity> Where<TEntity>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var specification = new WhereQuerySpecification<TEntity>(predicate);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IOrderedQueryConfigurator<TEntity> OrderBy<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector)
            where TEntity : class
        {
            return OrderBy(configurator, keySelector, null, false);
        }

        public static IOrderedQueryConfigurator<TEntity> OrderBy<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer)
            where TEntity : class
        {
            return OrderBy(configurator, keySelector, comparer, false);
        }

        public static IOrderedQueryConfigurator<TEntity> OrderBy<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer, bool descending)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            const bool isRoot = true;

            var specification = new OrderByQuerySpecification<TEntity, TProperty>(keySelector, comparer, @descending, isRoot);
            configurator.AddSpecification(specification);

            var nextConfigurator = new OrderedQueryConfigurator<TEntity>(configurator);
            return nextConfigurator;
        }

        public static IOrderedQueryConfigurator<TEntity> OrderByDescending<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector)
            where TEntity : class
        {
            return OrderBy(configurator, keySelector, null, true);
        }

        public static IOrderedQueryConfigurator<TEntity> OrderByDescending<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer)
            where TEntity : class
        {
            return OrderBy(configurator, keySelector, comparer, true);
        }

        public static IOrderedQueryConfigurator<TEntity> ThenBy<TEntity, TProperty>(this IOrderedQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector)
            where TEntity : class
        {
            return ThenBy(configurator, keySelector, null, false);
        }

        public static IOrderedQueryConfigurator<TEntity> ThenBy<TEntity, TProperty>(this IOrderedQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer)
            where TEntity : class
        {
            return ThenBy(configurator, keySelector, comparer, false);
        }

        public static IOrderedQueryConfigurator<TEntity> ThenBy<TEntity, TProperty>(this IOrderedQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer, bool descending)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            const bool isRoot = false;

            var specification = new OrderByQuerySpecification<TEntity, TProperty>(keySelector, comparer, @descending, isRoot);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IOrderedQueryConfigurator<TEntity> ThenByDescending<TEntity, TProperty>(this IOrderedQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector)
            where TEntity : class
        {
            return ThenBy(configurator, keySelector, null, true);
        }

        public static IOrderedQueryConfigurator<TEntity> ThenByDescending<TEntity, TProperty>(this IOrderedQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer)
            where TEntity : class
        {
            return ThenBy(configurator, keySelector, comparer, true);
        }

        public static IQueryConfigurator<TEntity> Page<TEntity>(this IQueryConfigurator<TEntity> configurator, int skip, int take)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new PageQuerySpecification<TEntity>(skip, take);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<TEntity> Distinct<TEntity>(this IQueryConfigurator<TEntity> configurator)
            where TEntity : class
        {
            return Distinct(configurator, null);
        }

        public static IQueryConfigurator<TEntity> Distinct<TEntity>(this IQueryConfigurator<TEntity> configurator, IEqualityComparer<TEntity> comparer)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new DistinctQuerySpecification<TEntity>(comparer);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<TIn, TOut> Select<TIn, TOut>(this IQueryConfigurator<TIn> configurator, Expression<Func<TIn, TOut>> selector)
            where TIn : class
            where TOut : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            var transformSpecification = new SelectQuerySpecification<TIn, TOut>(selector);
            var nextConfigurator = new QueryConfigurator<TIn, TOut>(configurator, transformSpecification);

            return nextConfigurator;
        }

        public static IQueryConfigurator<TIn, TOut> Output<TIn, TOut>(this IQueryConfigurator<TIn, TOut> configurator, Action<IQueryConfigurator<TOut>> configureCallback)
            where TIn : class
            where TOut : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (configureCallback == null)
                throw new ArgumentNullException(nameof(configureCallback));

            configureCallback(configurator);

            return configurator;
        }

    }
}