using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

		//

		public static IIncludeQueryConfigurator<TEntity, TProperty> Include<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
			where TEntity : class
		{
			if (configurator == null)
				throw new ArgumentNullException(nameof(configurator));

			var nextConfigurator = new IncludeQueryConfigurator<TEntity, TProperty>(configurator.OutputConfiguration, navigationPropertyPath, true, false);
			configurator.AddSpecification(nextConfigurator);

			return nextConfigurator;
		}

		public static IIncludeQueryConfigurator<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludeQueryConfigurator<TEntity, TPreviousProperty> configurator, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
			where TEntity : class
			where TPreviousProperty : class
		{
			if (configurator == null)
				throw new ArgumentNullException(nameof(configurator));

			var nextConfigurator = new IncludeQueryConfigurator<TEntity, TPreviousProperty, TProperty>(configurator.OutputConfiguration, navigationPropertyPath, false, false);
			configurator.AddSpecification(nextConfigurator);

			return nextConfigurator;
		}

		public static IIncludeQueryConfigurator<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludeQueryConfigurator<TEntity, IEnumerable<TPreviousProperty>> configurator, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
			where TEntity : class
		{
			if (configurator == null)
				throw new ArgumentNullException(nameof(configurator));

			var nextConfigurator = new IncludeQueryConfigurator<TEntity, TPreviousProperty, TProperty>(configurator.OutputConfiguration, navigationPropertyPath, false, true);
			configurator.AddSpecification(nextConfigurator);

			return nextConfigurator;
		}

	}

	public interface IIncludeQueryConfigurator<TEntity, TProperty> : IQueryConfigurator<TEntity>
		where TEntity : class
	{
		// nothing
	}

	public interface IIncludeQuerySpecification<TEntity> : IQuerySpecification2<TEntity>
		where TEntity : class
	{
		bool IsRoot { get; }

		bool IsEnumerable { get; }

		Type InputPropertyType { get; }

		Type OutputPropertyType { get; }
	}

	public interface IIncludeQuerySpecification<TEntity, TInputProperty, TOutputProperty> : IIncludeQuerySpecification<TEntity>
		where TEntity : class
	{
		Expression<Func<TInputProperty, TOutputProperty>> NavigationPropertyPath { get; }
	}

	public class IncludeQueryConfigurator<TEntity, TProperty> : IncludeQueryConfigurator<TEntity, TEntity, TProperty>
		where TEntity : class
	{
		public IncludeQueryConfigurator(IQueryConfiguration<TEntity> outputConfiguration, Expression<Func<TEntity, TProperty>> navigationPropertyPath, bool isRoot, bool isEnumerable)
			: base(outputConfiguration, navigationPropertyPath, isRoot, isEnumerable)
		{
			// nothing
		}

	}

	public class IncludeQueryConfigurator<TEntity, TInputProperty, TOutputProperty> :
		IIncludeQueryConfigurator<TEntity, TOutputProperty>,
		IIncludeQuerySpecification<TEntity, TInputProperty, TOutputProperty>
		where TEntity : class
	{
		private readonly List<IQuerySpecification<TEntity>> _specifications = new List<IQuerySpecification<TEntity>>();

		public IncludeQueryConfigurator(IQueryConfiguration<TEntity> outputConfiguration, Expression<Func<TInputProperty, TOutputProperty>> navigationPropertyPath, bool isRoot, bool isEnumerable)
		{
			OutputConfiguration = outputConfiguration ?? throw new ArgumentNullException(nameof(outputConfiguration));
			NavigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));

			IsRoot = isRoot;
			IsEnumerable = isEnumerable;
		}

		public string Name => EntityFrameworkCoreQueryNames.Include;

		public bool IsRoot { get; }

		public bool IsEnumerable { get; }

		public Type InputPropertyType => typeof(TInputProperty);

		public Type OutputPropertyType => typeof(TOutputProperty);

		public IQueryConfiguration<TEntity> OutputConfiguration { get; }

		public Expression<Func<TInputProperty, TOutputProperty>> NavigationPropertyPath { get; }

		public IReadOnlyList<IQuerySpecification<TEntity>> Specifications => _specifications;

		void IQueryConfigurator<TEntity>.AddSpecification(IQuerySpecification<TEntity> specification)
		{
			_specifications.Add(specification);
		}

	}

}