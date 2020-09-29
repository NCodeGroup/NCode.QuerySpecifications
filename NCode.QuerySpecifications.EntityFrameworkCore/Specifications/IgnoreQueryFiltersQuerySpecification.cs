using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface IIgnoreQueryFiltersQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class IgnoreQueryFiltersQuerySpecification<TEntity> : IIgnoreQueryFiltersQuerySpecification<TEntity>
        where TEntity : class
    {
        public string Name => EntityFrameworkCoreQueryNames.IgnoreQueryFilters;
    }
}