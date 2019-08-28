using System;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications
{
    public class WhereQuerySpecification<TEntity> : IQuerySpecification<TEntity>
	    where TEntity : class
	{
        public string Name => QueryNames.Where;

        public Expression<Func<TEntity, bool>> Expression { get; }

        public WhereQuerySpecification(Expression<Func<TEntity, bool>> expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

	}
}