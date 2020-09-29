using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class PageQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.Page;

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (specification is IPageQuerySpecification<TEntity> pageSpec)
            {
                pipe = new PageQueryPipe<TEntity>(pageSpec.Skip, pageSpec.Take);
                return true;
            }

            pipe = null;
            return false;
        }

    }
}