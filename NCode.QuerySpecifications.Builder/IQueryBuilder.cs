using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Builder.Transforms;
using NCode.QuerySpecifications.Configuration;

namespace NCode.QuerySpecifications.Builder
{
    public interface IQueryBuilder
    {
        IQueryPipe<TEntity> Build<TEntity>(IQueryConfiguration<TEntity> configuration)
            where TEntity : class;

        IQueryTransform<TIn, TOut> Build<TIn, TOut>(ITransformConfiguration<TIn, TOut> configuration)
            where TIn : class
            where TOut : class;
    }
}