using System.Linq;

namespace NCode.QuerySpecifications.Providers
{
	public interface IQueryTransform<in TIn, out TOut>
		where TIn : class
		where TOut : class
	{
		IQueryable<TOut> Apply(IQueryable<TIn> queryRoot);
	}
}