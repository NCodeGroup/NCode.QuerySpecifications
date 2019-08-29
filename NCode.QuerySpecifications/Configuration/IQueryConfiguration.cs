using System.Collections.Generic;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configuration
{
    public interface IQueryConfiguration<TEntity>
        where TEntity : class
    {
        IReadOnlyList<IQuerySpecification<TEntity>> OutputSpecifications { get; }
    }
}