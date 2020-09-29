using System;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public interface ISelectQuerySpecification<TIn, TOut> : IQuerySpecification<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        Expression<Func<TIn, TOut>> Selector { get; }
    }

    public class SelectQuerySpecification<TIn, TOut> : ISelectQuerySpecification<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        public string Name => QueryNames.Select;

        public Expression<Func<TIn, TOut>> Selector { get; }

        public SelectQuerySpecification(Expression<Func<TIn, TOut>> selector)
        {
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

    }
}