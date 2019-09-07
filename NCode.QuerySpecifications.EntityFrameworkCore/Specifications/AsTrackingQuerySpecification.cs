using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface IAsTrackingQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class AsTrackingQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>, IAsTrackingQuerySpecification<TEntity>
        where TEntity : class
    {
        public override string Name => EntityFrameworkCoreQueryNames.AsTracking;
    }
}