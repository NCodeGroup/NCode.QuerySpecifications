using System;
using System.Linq.Expressions;

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

		public static IQueryConfigurator<TEntity> OrderBy<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression, bool descending = false)
			where TEntity : class
		{
			if (configurator == null)
				throw new ArgumentNullException(nameof(configurator));
			if (expression == null)
				throw new ArgumentNullException(nameof(expression));

			var specification = new OrderByQuerySpecification<TEntity, TProperty>(expression, @descending);
			configurator.AddSpecification(specification);

			return configurator;
		}

		public static IQueryConfigurator<TEntity> OrderByDescending<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> expression)
			where TEntity : class
		{
			return OrderBy(configurator, expression, true);
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
			if (configurator == null)
				throw new ArgumentNullException(nameof(configurator));

			var specification = new DistinctQuerySpecification<TEntity>();
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

			var transformSpecification = new SelectTransformSpecification<TIn, TOut>(expression);

			var inputSpecifications = configurator.OutputConfiguration.Specifications;

			return new SelectTransformConfigurator<TIn, TOut>(transformSpecification, inputSpecifications);
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