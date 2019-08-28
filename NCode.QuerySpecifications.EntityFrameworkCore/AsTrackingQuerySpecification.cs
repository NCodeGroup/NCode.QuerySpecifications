namespace NCode.QuerySpecifications.EntityFrameworkCore
{
	public class AsTrackingQuerySpecification<TEntity> : IQuerySpecification<TEntity>
		where TEntity : class
	{
		public string Name => EntityFrameworkCoreQueryNames.AsTracking;
	}
}