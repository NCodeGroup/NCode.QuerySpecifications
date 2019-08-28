namespace NCode.QuerySpecifications.Providers
{
	public class SelectQueryTransformFactory : IQueryTransformFactory
	{
		public string Name => QueryNames.Select;

		public virtual bool TryCreate<TIn, TOut>(ITransformSpecification<TIn, TOut> specification, out IQueryTransform<TIn, TOut> transform)
			where TIn : class
			where TOut : class
		{
			if (specification is SelectTransformSpecification<TIn, TOut> selectSpec)
			{
				transform = new SelectQueryTransform<TIn, TOut>(selectSpec.Expression);
				return true;
			}

			transform = null;
			return true;
		}

	}
}