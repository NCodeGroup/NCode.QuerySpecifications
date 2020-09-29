using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class DistinctQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.Distinct;

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (specification is IDistinctQuerySpecification<TEntity> distinctSpec)
            {
                pipe = new DistinctQueryPipe<TEntity>(distinctSpec.Comparer);
                return true;
            }

            pipe = null;
            return false;
        }

    }
}