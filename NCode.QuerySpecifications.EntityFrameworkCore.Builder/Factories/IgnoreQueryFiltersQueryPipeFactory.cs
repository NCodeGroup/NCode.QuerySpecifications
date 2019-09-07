using NCode.QuerySpecifications.Builder.Factories;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes;
using NCode.QuerySpecifications.EntityFrameworkCore.Specifications;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Factories
{
    public class IgnoreQueryFiltersQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => EntityFrameworkCoreQueryNames.IgnoreQueryFilters;

        public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class
        {
            if (specification is IgnoreQueryFiltersQuerySpecification<TEntity>)
            {
                queryPipe = new IgnoreQueryFiltersQueryPipe<TEntity>();
                return true;
            }

            queryPipe = null;
            return false;
        }

    }
}