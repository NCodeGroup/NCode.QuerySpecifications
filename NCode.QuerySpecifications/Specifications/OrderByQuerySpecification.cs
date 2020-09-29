using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public interface IOrderByQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        Type PropertyType { get; }

        bool Descending { get; }

        bool IsRoot { get; }
    }

    public interface IOrderByQuerySpecification<TEntity, TProperty> : IOrderByQuerySpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, TProperty>> KeySelector { get; }

        IComparer<TProperty> Comparer { get; }
    }

    public class OrderByQuerySpecification<TEntity, TProperty> : IOrderByQuerySpecification<TEntity, TProperty>
        where TEntity : class
    {
        public string Name => QueryNames.OrderBy;

        public Type PropertyType => typeof(TProperty);

        public Expression<Func<TEntity, TProperty>> KeySelector { get; }

        public IComparer<TProperty> Comparer { get; }

        public bool Descending { get; }

        public bool IsRoot { get; }

        public OrderByQuerySpecification(Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer, bool @descending, bool isRoot)
        {
            KeySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
            Comparer = comparer;
            Descending = @descending;
            IsRoot = isRoot;
        }

    }
}