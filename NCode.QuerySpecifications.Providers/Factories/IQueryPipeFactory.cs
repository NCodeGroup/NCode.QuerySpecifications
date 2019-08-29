using NCode.QuerySpecifications.Provider.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Provider.Factories
{
    public interface IQueryPipeFactory : IQueryName
    {
        bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
            where TEntity : class;
    }
}