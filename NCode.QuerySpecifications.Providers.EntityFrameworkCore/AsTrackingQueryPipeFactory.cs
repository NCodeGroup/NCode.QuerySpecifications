using NCode.QuerySpecifications.EntityFrameworkCore;

namespace NCode.QuerySpecifications.Providers.EntityFrameworkCore
{
	public class AsTrackingQueryPipeFactory : IQueryPipeFactory
	{
		public string Name => EntityFrameworkCoreQueryNames.AsTracking;

		public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
			where TEntity : class
		{
			if (specification is AsTrackingQuerySpecification<TEntity>)
			{
				queryPipe = new AsTrackingQueryPipe<TEntity>();
				return true;
			}

			queryPipe = null;
			return false;
		}

	}
}