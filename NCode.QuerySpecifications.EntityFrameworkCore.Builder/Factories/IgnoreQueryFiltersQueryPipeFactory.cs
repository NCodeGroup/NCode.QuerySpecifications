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

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (specification is IIgnoreQueryFiltersQuerySpecification<TEntity>)
            {
                pipe = new IgnoreQueryFiltersQueryPipe<TEntity>();
                return true;
            }

            pipe = null;
            return false;
        }

    }
}