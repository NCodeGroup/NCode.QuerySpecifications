using System.Linq;

namespace NCode.QuerySpecifications.Provider.Pipes
{
    public class IdentityQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot;
        }

    }
}