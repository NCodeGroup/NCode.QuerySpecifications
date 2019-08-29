using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Provider.Pipes;
using NCode.QuerySpecifications.Provider.Transforms;

namespace NCode.QuerySpecifications.Provider
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