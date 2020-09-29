using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class SelectQueryPipeFactory : IQueryPipeTransformFactory
    {
        public string Name => QueryNames.Select;

        public bool TryCreate<TIn, TOut>(IQuerySpecification<TIn, TOut> specification, out IQueryPipe<TIn, TOut> pipe)
            where TIn : class
            where TOut : class
        {
            if (specification is ISelectQuerySpecification<TIn, TOut> selectSpec)
            {
                pipe = new SelectQueryPipe<TIn, TOut>(selectSpec.Selector);
                return true;
            }

            pipe = null;
            return true;
        }

    }
}