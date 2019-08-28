using System;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.EntityFrameworkCore
{
	public class IncludeQuerySpecification<TEntity, TProperty> : IQuerySpecification<TEntity>
		where TEntity : class
	{
		public string Name => EntityFrameworkCoreQueryNames.Include;

		public Expression<Func<TEntity, TProperty>> Expression { get; }

		public IncludeQuerySpecification(Expression<Func<TEntity, TProperty>> expression)
		{
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

	}
}