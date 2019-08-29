using System.Collections.Generic;

namespace NCode.QuerySpecifications.Specifications
{
    public interface IDistinctQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        IEqualityComparer<TEntity> Comparer { get; }
    }

    public class DistinctQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>, IDistinctQuerySpecification<TEntity>
        where TEntity : class
    {
        public DistinctQuerySpecification(IEqualityComparer<TEntity> comparer)
        {
            Comparer = comparer;
        }

        public override string Name => QueryNames.Distinct;

        public IEqualityComparer<TEntity> Comparer { get; }
    }
}