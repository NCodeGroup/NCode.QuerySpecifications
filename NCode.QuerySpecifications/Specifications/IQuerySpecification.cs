namespace NCode.QuerySpecifications.Specifications
{
    public interface IQuerySpecification<TEntity> : IQueryName
        where TEntity : class
    {
        // nothing
    }

    public interface IQuerySpecification<TIn, TOut> : IQueryName
        where TIn : class
        where TOut : class
    {
        // nothing
    }
}