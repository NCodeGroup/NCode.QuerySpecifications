using NCode.QuerySpecifications.EntityFrameworkCore;

namespace NCode.QuerySpecifications.Providers.EntityFrameworkCore
{
	public class AsNoTrackingQueryPipeFactory : IQueryPipeFactory
	{
		public string Name => EntityFrameworkCoreQueryNames.AsNoTracking;

		public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
			where TEntity : class
		{
			if (specification is AsNoTrackingQuerySpecification<TEntity>)
			{
				queryPipe = new AsNoTrackingQueryPipe<TEntity>();
				return true;
			}

			queryPipe = null;
			return false;
		}

	}
}