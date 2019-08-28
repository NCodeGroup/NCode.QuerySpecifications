using System.Collections.Generic;

namespace NCode.QuerySpecifications
{
	public interface ITransformConfiguration<TIn, TOut>
		where TIn : class
		where TOut : class
	{
		ITransformSpecification<TIn, TOut> TransformSpecification { get; }

		IReadOnlyList<IQuerySpecification<TIn>> InputSpecifications { get; }

		IReadOnlyList<IQuerySpecification<TOut>> OutputSpecifications { get; }
	}
}