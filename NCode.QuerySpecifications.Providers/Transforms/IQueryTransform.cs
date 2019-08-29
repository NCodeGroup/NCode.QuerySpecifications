using System.Linq;

namespace NCode.QuerySpecifications.Provider.Transforms
{
    public interface IQueryTransform<in TIn, out TOut>
        where TIn : class
        where TOut : class
    {
        IQueryable<TOut> Apply(IQueryable<TIn> queryRoot);
    }
}