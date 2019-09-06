using System;
using System.Linq;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class ChainQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly IQueryPipe<TEntity> _current;
        private readonly IQueryPipe<TEntity> _next;

        public ChainQueryPipe(IQueryPipe<TEntity> current, IQueryPipe<TEntity> next)
        {
            _current = current ?? throw new ArgumentNullException(nameof(current));
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            var query = _current.Apply(queryRoot);

            query = _next.Apply(query);

            return query;
        }

    }
}