using System.ComponentModel;

namespace NCode.QuerySpecifications
{
	public interface ITransformConfigurator<TIn, TOut> : IQueryConfigurator<TOut>
		where TIn : class
		where TOut : class
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		ITransformConfiguration<TIn, TOut> TransformConfiguration { get; }
	}
}