namespace NCode.QuerySpecifications.Specifications
{
    public interface IQuerySpecification<TEntity> : IQueryName
        where TEntity : class
    {
        // nothing
    }
}