namespace NCode.QuerySpecifications.Providers
{
	public interface IQueryTransformFactory : IQueryName
	{
		bool TryCreate<TIn, TOut>(ITransformSpecification<TIn, TOut> specification, out IQueryTransform<TIn, TOut> transform)
			where TIn : class
			where TOut : class;
	}
}