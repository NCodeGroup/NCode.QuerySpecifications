using NCode.QuerySpecifications.Provider.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Provider.Factories
{
    public class DistinctQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.Distinct;

        public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class
        {
            if (specification is IDistinctQuerySpecification<TEntity> distinctSpec)
            {
                queryPipe = new DistinctQueryPipe<TEntity>(distinctSpec.Comparer);
                return true;
            }

            queryPipe = null;
            return false;
        }

    }
}