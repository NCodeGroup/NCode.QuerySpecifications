using System.Linq;

namespace NCode.QuerySpecifications.Providers
{
    public interface IQueryPipe<TEntity>
	    where TEntity : class
	{
        IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot);
    }
}