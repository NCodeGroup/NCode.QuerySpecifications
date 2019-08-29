using System;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Configuration;

namespace NCode.QuerySpecifications.Specifications
{
    public interface ITransformSpecification<TIn, TOut> : IQueryName, ITransformConfiguration<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        Expression<Func<TIn, TOut>> Expression { get; }
    }
}