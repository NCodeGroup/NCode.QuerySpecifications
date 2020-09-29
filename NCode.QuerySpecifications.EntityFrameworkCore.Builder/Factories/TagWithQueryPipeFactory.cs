using NCode.QuerySpecifications.Builder.Factories;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes;
using NCode.QuerySpecifications.EntityFrameworkCore.Specifications;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Factories
{
    public class TagWithQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => EntityFrameworkCoreQueryNames.TagWith;

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (specification is ITagWithQuerySpecification<TEntity> tagWithSpec)
            {
                pipe = new TagWithQueryPipe<TEntity>(tagWithSpec.Tag);
                return true;
            }

            pipe = null;
            return false;
        }

    }
}