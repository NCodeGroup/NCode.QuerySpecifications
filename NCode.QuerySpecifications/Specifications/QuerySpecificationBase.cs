using System;
using System.Collections.Generic;

namespace NCode.QuerySpecifications.Specifications
{
    public abstract class QuerySpecificationBase<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        public abstract string Name { get; }

        public virtual IReadOnlyList<IQuerySpecification<TEntity>> OutputSpecifications => Array.Empty<IQuerySpecification<TEntity>>();
    }
}