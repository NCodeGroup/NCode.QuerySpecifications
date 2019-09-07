using NCode.QuerySpecifications.Builder.Transforms;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class SelectQueryTransformFactory : IQueryTransformFactory
    {
        public string Name => QueryNames.Select;

        public virtual bool TryCreate<TIn, TOut>(ITransformSpecification<TIn, TOut> specification, out IQueryTransform<TIn, TOut> transform)
            where TIn : class
            where TOut : class
        {
            if (specification is ISelectTransformSpecification<TIn, TOut> transformSpec)
            {
                transform = new SelectQueryTransform<TIn, TOut>(transformSpec.Selector);
                return true;
            }

            transform = null;
            return true;
        }

    }
}