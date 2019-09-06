using System.Linq;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public interface IQueryPipe<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot);
    }
}