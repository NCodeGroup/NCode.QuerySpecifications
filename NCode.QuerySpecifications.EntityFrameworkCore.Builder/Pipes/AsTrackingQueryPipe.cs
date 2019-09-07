using System.Linq;
using Microsoft.EntityFrameworkCore;
using NCode.QuerySpecifications.Builder.Pipes;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes
{
    public class AsTrackingQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.AsTracking();
        }

    }
}