using NCode.QuerySpecifications.Provider.Transforms;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Provider.Factories
{
    public interface IQueryTransformFactory : IQueryName
    {
        bool TryCreate<TIn, TOut>(ITransformSpecification<TIn, TOut> specification, out IQueryTransform<TIn, TOut> transform)
            where TIn : class
            where TOut : class;
    }
}