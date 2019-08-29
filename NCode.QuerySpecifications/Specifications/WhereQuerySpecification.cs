using System;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public class WhereQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>
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