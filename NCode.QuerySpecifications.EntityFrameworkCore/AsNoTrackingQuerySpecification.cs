namespace NCode.QuerySpecifications.EntityFrameworkCore
{
	public class AsNoTrackingQuerySpecification<TEntity> : IQuerySpecification<TEntity>
		where TEntity : class
	{
		public string Name => EntityFrameworkCoreQueryNames.AsNoTracking;
	}
}