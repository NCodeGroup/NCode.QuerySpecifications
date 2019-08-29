using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications
{
    public static class QueryConfiguratorExtensions
    {
        public static IQueryConfigurator<TEntity> Where<TEntity>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, bool>> expression)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var specification = new WhereQuerySpecification<TEntity>(expression);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IOrderByQueryConfigurator<TEntity> OrderBy<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression)
            where TEntity : class
        {
            return OrderBy(configurator, expression, null, false);
        }

        public static IOrderByQueryConfigurator<TEntity> OrderBy<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression, IComparer<TProperty> comparer)
            where TEntity : class
        {
            return OrderBy(configurator, expression, comparer, false);
        }

        public static IOrderByQueryConfigurator<TEntity> OrderBy<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression, IComparer<TProperty> comparer, bool descending)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var nextConfigurator = new OrderByQueryConfigurator<TEntity>(configurator.OutputConfiguration);
            var specification = new OrderByQuerySpecification<TEntity, TProperty>(nextConfigurator.Specifications, expression, comparer, @descending);
            configurator.AddSpecification(specification);

            return nextConfigurator;
        }

        public static IOrderByQueryConfigurator<TEntity> OrderByDescending<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression)
            where TEntity : class
        {
            return OrderBy(configurator, expression, null, true);
        }

        public static IOrderByQueryConfigurator<TEntity> OrderByDescending<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression, IComparer<TProperty> comparer)
            where TEntity : class
        {
            return OrderBy(configurator, expression, comparer, true);
        }

        public static IOrderByQueryConfigurator<TEntity> ThenBy<TEntity, TProperty>(this IOrderByQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression)
            where TEntity : class
        {
            return ThenBy(configurator, expression, null, false);
        }

        public static IOrderByQueryConfigurator<TEntity> ThenBy<TEntity, TProperty>(this IOrderByQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression, IComparer<TProperty> comparer)
            where TEntity : class
        {
            return ThenBy(configurator, expression, comparer, false);
        }

        public static IOrderByQueryConfigurator<TEntity> ThenBy<TEntity, TProperty>(this IOrderByQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression, IComparer<TProperty> comparer, bool descending)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var nextConfigurator = new OrderByQueryConfigurator<TEntity>(configurator.OutputConfiguration);
            var specification = new OrderByQuerySpecification<TEntity, TProperty>(nextConfigurator.Specifications, expression, comparer, @descending);
            configurator.AddSpecification(specification);

            return nextConfigurator;
        }

        public static IOrderByQueryConfigurator<TEntity> ThenByDescending<TEntity, TProperty>(this IOrderByQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression)
            where TEntity : class
        {
            return ThenBy(configurator, expression, null, true);
        }

        public static IOrderByQueryConfigurator<TEntity> ThenByDescending<TEntity, TProperty>(this IOrderByQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression, IComparer<TProperty> comparer)
            where TEntity : class
        {
            return ThenBy(configurator, expression, comparer, true);
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

        public static ITransformConfigurator<TIn, TOut> Select<TIn, TOut>(this IQueryConfigurator<TIn> configurator, Expression<Func<TIn, TOut>> expression)
            where TIn : class
            where TOut : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var inputSpecifications = configurator.OutputConfiguration.OutputSpecifications;
            var outputSpecifications = new List<IQuerySpecification<TOut>>();

            var transformSpecification = new SelectTransformSpecification<TIn, TOut>(expression, inputSpecifications, outputSpecifications);
            var nextConfigurator = new TransformConfigurator<TIn, TOut>(transformSpecification, inputSpecifications, outputSpecifications);

            return nextConfigurator;
        }

        public static ITransformConfigurator<TIn, TOut> Output<TIn, TOut>(this ITransformConfigurator<TIn, TOut> configurator, Action<IQueryConfigurator<TOut>> callback)
            where TIn : class
            where TOut : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            callback(configurator);

            return configurator;
        }

    }
}