namespace NCode.QuerySpecifications.Specifications
{
    public interface ITransformSpecification<TIn, TOut> : IQueryName
        where TIn : class
        where TOut : class
    {
        // nothing
    }
}