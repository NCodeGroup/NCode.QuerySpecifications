using System;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
	public interface IWhereQuerySpecification<TEntity> : IQuerySpecification<TEntity>
		where TEntity : class
	{
		Expression<Func<TEntity, bool>> Expression { get; }
	}

	public class WhereQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>, IWhereQuerySpecification<TEntity>
		where TEntity : class
	{
		public override string Name => QueryNames.Where;

		public Expression<Func<TEntity, bool>> Expression { get; }

		public WhereQuerySpecification(Expression<Func<TEntity, bool>> expression)
		{
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

	}
}