using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
	public class AsNoTrackingQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>
		where TEntity : class
	{
		public override string Name => EntityFrameworkCoreQueryNames.AsNoTracking;
	}
}