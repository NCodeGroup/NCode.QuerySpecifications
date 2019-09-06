using System.Linq;

namespace NCode.QuerySpecifications.Builder.Pipes
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