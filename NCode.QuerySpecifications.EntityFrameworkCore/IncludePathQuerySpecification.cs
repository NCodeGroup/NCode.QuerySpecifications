using System;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore
{
	public interface IIncludePathQuerySpecification<TEntity> : IQuerySpecification<TEntity>
		where TEntity : class
	{
		string NavigationPropertyPath { get; }
	}

	public class IncludePathQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>, IIncludePathQuerySpecification<TEntity>
		where TEntity : class
	{
		public IncludePathQuerySpecification(string navigationPropertyPath)
		{
			NavigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));
		}

		public override string Name => EntityFrameworkCoreQueryNames.Include;

		public string NavigationPropertyPath { get; }
	}
}