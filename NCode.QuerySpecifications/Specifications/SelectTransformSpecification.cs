using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public interface ISelectTransformSpecification<TIn, TOut> : ITransformSpecification<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        Expression<Func<TIn, TOut>> Selector { get; }
    }

    public class SelectTransformSpecification<TIn, TOut> : ISelectTransformSpecification<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        public string Name => QueryNames.Select;

        public Expression<Func<TIn, TOut>> Selector { get; }

        public SelectTransformSpecification(Expression<Func<TIn, TOut>> selector, IReadOnlyList<IQuerySpecification<TIn>> inputSpecifications, IReadOnlyList<IQuerySpecification<TOut>> outputSpecifications)
        {
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
            InputSpecifications = inputSpecifications ?? throw new ArgumentNullException(nameof(inputSpecifications));
            OutputSpecifications = outputSpecifications ?? throw new ArgumentNullException(nameof(outputSpecifications));
        }

        public ITransformSpecification<TIn, TOut> TransformSpecification => this;

        public IReadOnlyList<IQuerySpecification<TIn>> InputSpecifications { get; }

        public IReadOnlyList<IQuerySpecification<TOut>> OutputSpecifications { get; }
    }
}