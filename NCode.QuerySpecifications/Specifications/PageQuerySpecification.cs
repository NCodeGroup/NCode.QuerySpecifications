namespace NCode.QuerySpecifications.Specifications
{
	public interface IPageQuerySpecification<TEntity> : IQuerySpecification<TEntity>
		where TEntity : class
	{
		int Skip { get; }

		int Take { get; }
	}

	public class PageQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>, IPageQuerySpecification<TEntity>
		where TEntity : class
	{
        public override string Name => QueryNames.Page;

        public int Skip { get; }

        public int Take { get; }

        public PageQuerySpecification(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

    }
}