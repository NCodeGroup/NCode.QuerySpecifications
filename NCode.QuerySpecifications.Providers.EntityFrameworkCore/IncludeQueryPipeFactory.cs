using System;
using NCode.QuerySpecifications.EntityFrameworkCore;

namespace NCode.QuerySpecifications.Providers.EntityFrameworkCore
{
	public class IncludeQueryPipeFactory : IQueryPipeFactory
	{
		public string Name => EntityFrameworkCoreQueryNames.Include;

		public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
			where TEntity : class
		{
			if (specification is IIncludeQuerySpecification<TEntity> includeSpec)
			{
				if (includeSpec.IsRoot)
				{
					var factoryType2 = typeof(RootFactory<>).MakeGenericType(includeSpec.OutputPropertyType);
					var factory2 = (IQueryPipeFactory)Activator.CreateInstance(factoryType2);
					return factory2.TryCreate(specification, out queryPipe);
				}

				var factoryType = typeof(ThenFactory<,>).MakeGenericType(includeSpec.InputPropertyType, includeSpec.OutputPropertyType);
				var factory = (IQueryPipeFactory)Activator.CreateInstance(factoryType);
				return factory.TryCreate(specification, out queryPipe);
			}

			queryPipe = null;
			return false;
		}

		private class RootFactory<TProperty> : IQueryPipeFactory
		{
			public string Name => EntityFrameworkCoreQueryNames.Include;

			public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
				where TEntity : class
			{
				if (specification is IIncludeQuerySpecification<TEntity, TEntity, TProperty> includeSpec)
				{
					queryPipe = new IncludeQueryPipe<TEntity, TProperty>(includeSpec.NavigationPropertyPath);
					return true;
				}

				queryPipe = null;
				return false;
			}
		}

		private class ThenFactory<TInputProperty, TOutputProperty> : IQueryPipeFactory
		{
			public string Name => QueryNames.OrderBy;

			public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
				where TEntity : class
			{
				if (specification is IIncludeQuerySpecification<TEntity, TInputProperty, TOutputProperty> includeSpec)
				{
					queryPipe = new ThenIncludeQueryPipe<TEntity, TInputProperty, TOutputProperty>(includeSpec.NavigationPropertyPath, includeSpec.IsEnumerable);
					return true;
				}

				queryPipe = null;
				return false;
			}
		}

	}
}