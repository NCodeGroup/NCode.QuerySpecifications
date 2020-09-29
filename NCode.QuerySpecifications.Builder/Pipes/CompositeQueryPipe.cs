using System;
using System.Collections.Generic;
using System.Linq;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class CompositeQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly IReadOnlyCollection<IQueryPipe<TEntity>> _pipes;

        public CompositeQueryPipe(IEnumerable<IQueryPipe<TEntity>> pipes)
        {
            _pipes = pipes?.ToArray() ?? throw new ArgumentNullException(nameof(pipes));
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return _pipes.Aggregate(queryRoot, (current, pipe) => pipe.Apply(current));
        }

    }
}