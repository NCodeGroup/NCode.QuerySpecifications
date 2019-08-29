using NCode.QuerySpecifications.Provider.Transforms;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Provider.Factories
{
    public class SelectQueryTransformFactory : IQueryTransformFactory
    {
        public string Name => QueryNames.Select;

        public virtual bool TryCreate<TIn, TOut>(ITransformSpecification<TIn, TOut> specification, out IQueryTransform<TIn, TOut> transform)
            where TIn : class
            where TOut : class
        {
            if (specification is ITransformSpecification<TIn, TOut> transformSpec)
            {
                transform = new SelectQueryTransform<TIn, TOut>(transformSpec.Expression);
                return true;
            }

            transform = null;
            return true;
        }

    }
}