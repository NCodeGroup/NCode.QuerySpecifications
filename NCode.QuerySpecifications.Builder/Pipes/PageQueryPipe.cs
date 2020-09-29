using System.Linq;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class PageQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly int _skip;
        private readonly int _take;

        public PageQueryPipe(int skip, int take)
        {
            _skip = skip;
            _take = take;
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.Skip(_skip).Take(_take);
        }

    }
}