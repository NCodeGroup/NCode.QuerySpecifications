using System.Linq;

namespace NCode.QuerySpecifications.Provider.Pipes
{
    public interface IQueryPipe<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot);
    }
}