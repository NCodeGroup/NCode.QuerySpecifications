using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class WhereQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.Where;

        public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class
        {
            if (specification is IWhereQuerySpecification<TEntity> whereSpec)
            {
                queryPipe = new WhereQueryPipe<TEntity>(whereSpec.Predicate);
                return true;
            }

            queryPipe = null;
            return false;
        }

    }
}