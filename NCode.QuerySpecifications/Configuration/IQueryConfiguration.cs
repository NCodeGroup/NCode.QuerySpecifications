using System.Collections.Generic;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configuration
{
    public interface IQueryConfiguration<TEntity>
        where TEntity : class
    {
        IReadOnlyList<IQuerySpecification<TEntity>> OutputSpecifications { get; }
    }

    public interface IQueryConfiguration<TIn, TOut> : IQueryConfiguration<TOut>
        where TIn : class
        where TOut : class
    {
        IQuerySpecification<TIn, TOut> TransformSpecification { get; }

        IReadOnlyList<IQuerySpecification<TIn>> InputSpecifications { get; }
    }
}