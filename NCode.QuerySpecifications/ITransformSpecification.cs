namespace NCode.QuerySpecifications
{
	public interface ITransformSpecification<TIn, TOut> : IQueryName
		where TIn : class
		where TOut : class
	{
		// nothing
	}
}