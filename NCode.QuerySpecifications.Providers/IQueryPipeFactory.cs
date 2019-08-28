namespace NCode.QuerySpecifications.Providers
{
    public interface IQueryPipeFactory : IQueryName
    {
        bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
	        where TEntity : class;
    }
}