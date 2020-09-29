using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NCode.QuerySpecifications.Builder.Pipes;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes
{
    public class TagWithQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly string _tag;

        public TagWithQueryPipe(string tag)
        {
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.TagWith(_tag);
        }

    }
}