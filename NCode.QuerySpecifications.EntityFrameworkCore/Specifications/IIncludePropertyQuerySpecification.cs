using System;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface IIncludePropertyQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        bool IsRoot { get; }

        bool IsEnumerable { get; }

        Type InputPropertyType { get; }

        Type OutputPropertyType { get; }
    }

    public interface IIncludePropertyQuerySpecification<TEntity, TInputProperty, TOutputProperty> : IIncludePropertyQuerySpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TInputProperty, TOutputProperty>> NavigationPropertyPath { get; }
    }
}