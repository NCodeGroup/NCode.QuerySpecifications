using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface IAsTrackingQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class AsTrackingQuerySpecification<TEntity> : IAsTrackingQuerySpecification<TEntity>
        where TEntity : class
    {
        public string Name => EntityFrameworkCoreQueryNames.AsTracking;
    }
}