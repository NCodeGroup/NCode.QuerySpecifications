using NCode.QuerySpecifications.Provider.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Provider.Factories
{
    public class WhereQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.Where;

        public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class
        {
            if (specification is WhereQuerySpecification<TEntity> whereSpec)
            {
                queryPipe = new WhereQueryPipe<TEntity>(whereSpec.Expression);
                return true;
            }

            queryPipe = null;
            return false;
        }

    }
}