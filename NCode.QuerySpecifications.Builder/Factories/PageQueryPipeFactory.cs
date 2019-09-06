using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class PageQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.Page;

        public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class
        {
            if (specification is IPageQuerySpecification<TEntity> pageSpec)
            {
                queryPipe = new PageQueryPipe<TEntity>(pageSpec.Skip, pageSpec.Take);
                return true;
            }

            queryPipe = null;
            return false;
        }

    }
}