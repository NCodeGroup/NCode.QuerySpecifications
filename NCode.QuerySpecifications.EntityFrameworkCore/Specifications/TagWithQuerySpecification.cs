using System;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface ITagWithQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        string Tag { get; }
    }

    public class TagWithQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>, ITagWithQuerySpecification<TEntity>
        where TEntity : class
    {
        public TagWithQuerySpecification(string tag)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        public override string Name => EntityFrameworkCoreQueryNames.TagWith;

        public string Tag { get; }
    }
}