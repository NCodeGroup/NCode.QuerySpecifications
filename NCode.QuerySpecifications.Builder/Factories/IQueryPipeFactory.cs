using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public interface IQueryPipeFactory : IQueryName
    {
        bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class;
    }
}