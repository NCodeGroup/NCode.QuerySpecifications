using System.Linq;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public interface IQueryPipe<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot);
    }

    public interface IQueryPipe<in TIn, out TOut>
        where TIn : class
        where TOut : class
    {
        IQueryable<TOut> Apply(IQueryable<TIn> queryRoot);
    }
}