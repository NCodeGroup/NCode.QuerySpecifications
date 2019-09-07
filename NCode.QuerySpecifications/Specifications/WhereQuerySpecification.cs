using System;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public interface IWhereQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> Predicate { get; }
    }

    public class WhereQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>, IWhereQuerySpecification<TEntity>
        where TEntity : class
    {
        public override string Name => QueryNames.Where;

        public Expression<Func<TEntity, bool>> Predicate { get; }

        public WhereQuerySpecification(Expression<Func<TEntity, bool>> expression)
        {
            Predicate = expression ?? throw new ArgumentNullException(nameof(expression));
        }

    }
}