using System;
using System.Linq;

namespace NCode.QuerySpecifications.Providers
{
	public class ChainQueryTransform<TIn, TOut> : IQueryTransform<TIn, TOut>
		where TIn : class
		where TOut : class
	{
		private readonly IQueryPipe<TIn> _inputPipe;
		private readonly IQueryPipe<TOut> _outputPipe;
		private readonly IQueryTransform<TIn, TOut> _transform;

		public ChainQueryTransform(IQueryPipe<TIn> inputPipe, IQueryPipe<TOut> outputPipe, IQueryTransform<TIn, TOut> transform)
		{
			_inputPipe = inputPipe ?? throw new ArgumentNullException(nameof(inputPipe));
			_outputPipe = outputPipe ?? throw new ArgumentNullException(nameof(outputPipe));
			_transform = transform ?? throw new ArgumentNullException(nameof(transform));
		}

		public virtual IQueryable<TOut> Apply(IQueryable<TIn> queryRoot)
		{
			var inputQuery = _inputPipe.Apply(queryRoot);

			var transformQuery = _transform.Apply(inputQuery);

			var outputQuery = _outputPipe.Apply(transformQuery);

			return outputQuery;
		}

	}
}