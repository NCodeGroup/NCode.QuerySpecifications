using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public interface IOrderByQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        Type PropertyType { get; }
    }

    public interface IOrderByQuerySpecification<TEntity, TProperty> : IOrderByQuerySpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, TProperty>> KeySelector { get; }

        IComparer<TProperty> Comparer { get; }

        bool Descending { get; }
    }
}