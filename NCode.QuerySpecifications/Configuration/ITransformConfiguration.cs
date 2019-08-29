using System.Collections.Generic;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configuration
{
    public interface ITransformConfiguration<TIn, TOut> : IQueryConfiguration<TOut>
        where TIn : class
        where TOut : class
    {
        ITransformSpecification<TIn, TOut> TransformSpecification { get; }

        IReadOnlyList<IQuerySpecification<TIn>> InputSpecifications { get; }
    }
}