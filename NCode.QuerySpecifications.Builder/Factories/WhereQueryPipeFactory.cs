using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class WhereQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.Where;

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (specification is IWhereQuerySpecification<TEntity> whereSpec)
            {
                pipe = new WhereQueryPipe<TEntity>(whereSpec.Predicate);
                return true;
            }

            pipe = null;
            return false;
        }

    }
}