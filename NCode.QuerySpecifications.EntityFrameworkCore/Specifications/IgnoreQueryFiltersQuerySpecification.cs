using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface IIgnoreQueryFiltersQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class IgnoreQueryFiltersQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>, IIgnoreQueryFiltersQuerySpecification<TEntity>
        where TEntity : class
    {
        public override string Name => EntityFrameworkCoreQueryNames.IgnoreQueryFilters;
    }
}