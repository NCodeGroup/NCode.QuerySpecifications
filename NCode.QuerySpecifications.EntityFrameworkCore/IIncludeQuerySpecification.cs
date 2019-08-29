using System;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore
{
    public interface IIncludeQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        bool IsRoot { get; }

        bool IsEnumerable { get; }

        Type InputPropertyType { get; }

        Type OutputPropertyType { get; }
    }

    public interface IIncludeQuerySpecification<TEntity, TInputProperty, TOutputProperty> : IIncludeQuerySpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TInputProperty, TOutputProperty>> NavigationPropertyPath { get; }
    }
}