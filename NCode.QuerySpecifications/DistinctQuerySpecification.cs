namespace NCode.QuerySpecifications
{
    public class DistinctQuerySpecification<TEntity> : IQuerySpecification<TEntity>
	    where TEntity : class
	{
        public string Name => QueryNames.Distinct;
    }
}