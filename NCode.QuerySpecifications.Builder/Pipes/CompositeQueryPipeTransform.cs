using System;
using System.Linq;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class CompositeQueryPipeTransform<TIn, TOut> : IQueryPipe<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        private readonly IQueryPipe<TIn> _inputPipe;
        private readonly IQueryPipe<TOut> _outputPipe;
        private readonly IQueryPipe<TIn, TOut> _transformPipe;

        public CompositeQueryPipeTransform(IQueryPipe<TIn> inputPipe, IQueryPipe<TOut> outputPipe, IQueryPipe<TIn, TOut> transformPipe)
        {
            _inputPipe = inputPipe ?? throw new ArgumentNullException(nameof(inputPipe));
            _outputPipe = outputPipe ?? throw new ArgumentNullException(nameof(outputPipe));
            _transformPipe = transformPipe ?? throw new ArgumentNullException(nameof(transformPipe));
        }

        public IQueryable<TOut> Apply(IQueryable<TIn> queryRoot)
        {
            var inputQuery = _inputPipe.Apply(queryRoot);

            var transformQuery = _transformPipe.Apply(inputQuery);

            var outputQuery = _outputPipe.Apply(transformQuery);

            return outputQuery;
        }

    }
}