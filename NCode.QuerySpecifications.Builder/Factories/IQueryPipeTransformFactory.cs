using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public interface IQueryPipeTransformFactory : IQueryName
    {
        bool TryCreate<TIn, TOut>(IQuerySpecification<TIn, TOut> specification, out IQueryPipe<TIn, TOut> pipe)
            where TIn : class
            where TOut : class;
    }
}