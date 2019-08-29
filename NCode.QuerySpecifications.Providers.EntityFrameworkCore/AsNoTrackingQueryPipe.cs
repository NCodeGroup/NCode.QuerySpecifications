using System.Linq;
using Microsoft.EntityFrameworkCore;
using NCode.QuerySpecifications.Provider.Pipes;

namespace NCode.QuerySpecifications.Providers.EntityFrameworkCore
{
	public class AsNoTrackingQueryPipe<TEntity> : IQueryPipe<TEntity>
		where TEntity : class
	{
		public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
		{
			return queryRoot.AsNoTracking();
		}

	}
}