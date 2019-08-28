using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NCode.QuerySpecifications.Providers.EntityFrameworkCore
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