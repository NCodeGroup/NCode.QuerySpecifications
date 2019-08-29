namespace NCode.QuerySpecifications.Specifications
{
    public class PageQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>
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