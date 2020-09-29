using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface IAsNoTrackingQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class AsNoTrackingQuerySpecification<TEntity> : IAsNoTrackingQuerySpecification<TEntity>
        where TEntity : class
    {
        public string Name => EntityFrameworkCoreQueryNames.AsNoTracking;
    }
}