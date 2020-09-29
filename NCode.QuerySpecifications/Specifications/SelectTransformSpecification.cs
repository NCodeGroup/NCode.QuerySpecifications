using System;
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

        public SelectTransformSpecification(Expression<Func<TIn, TOut>> selector)
        {
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

    }
}