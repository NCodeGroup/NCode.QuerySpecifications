using System.Collections.Generic;
using System.Linq;

namespace NCode.QuerySpecifications.Provider.Pipes
{
    public class DistinctQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly IEqualityComparer<TEntity> _comparer;

        public DistinctQueryPipe(IEqualityComparer<TEntity> comparer)
        {
            _comparer = comparer;
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return _comparer == null
                ? queryRoot.Distinct()
                : queryRoot.Distinct(_comparer);
        }

    }
}