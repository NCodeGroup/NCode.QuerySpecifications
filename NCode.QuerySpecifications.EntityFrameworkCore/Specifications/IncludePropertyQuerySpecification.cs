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

    public class IncludePropertyQuerySpecification<TEntity, TInputProperty, TOutputProperty> : IIncludePropertyQuerySpecification<TEntity, TInputProperty, TOutputProperty>
        where TEntity : class
    {
        public string Name => EntityFrameworkCoreQueryNames.Include;

        public bool IsRoot { get; }

        public bool IsEnumerable { get; }

        public Type InputPropertyType => typeof(TInputProperty);

        public Type OutputPropertyType => typeof(TOutputProperty);

        public Expression<Func<TInputProperty, TOutputProperty>> NavigationPropertyPath { get; }

        public IncludePropertyQuerySpecification(Expression<Func<TInputProperty, TOutputProperty>> navigationPropertyPath, bool isRoot, bool isEnumerable)
        {
            NavigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));

            IsRoot = isRoot;
            IsEnumerable = isEnumerable;
        }

    }
}