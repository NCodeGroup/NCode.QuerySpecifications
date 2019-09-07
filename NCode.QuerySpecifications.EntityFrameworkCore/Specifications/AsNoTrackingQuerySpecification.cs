using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface IAsNoTrackingQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class AsNoTrackingQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>, IAsNoTrackingQuerySpecification<TEntity>
        where TEntity : class
    {
        public override string Name => EntityFrameworkCoreQueryNames.AsNoTracking;
    }
}