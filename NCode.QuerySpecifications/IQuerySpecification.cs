namespace NCode.QuerySpecifications
{
	public interface IQuerySpecification<TEntity> : IQueryName
		where TEntity : class
	{
		// nothing
	}

	public interface IQuerySpecification2<TEntity> : IQueryName, IQuerySpecification<TEntity>, IQueryConfiguration<TEntity>
		where TEntity : class
	{
		// nothing
	}
}