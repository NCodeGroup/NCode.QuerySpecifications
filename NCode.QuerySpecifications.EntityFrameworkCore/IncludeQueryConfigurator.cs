using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore
{
    public interface IIncludeQueryConfigurator<TEntity, TProperty> : IQueryConfigurator<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class IncludeQueryConfigurator<TEntity, TInputProperty, TOutputProperty> :
        IIncludeQueryConfigurator<TEntity, TOutputProperty>,
        IIncludeQuerySpecification<TEntity, TInputProperty, TOutputProperty>
        where TEntity : class
    {
        private readonly List<IQuerySpecification<TEntity>> _specifications = new List<IQuerySpecification<TEntity>>();

        public IncludeQueryConfigurator(IQueryConfiguration<TEntity> outputConfiguration, Expression<Func<TInputProperty, TOutputProperty>> navigationPropertyPath, bool isRoot, bool isEnumerable)
        {
            OutputConfiguration = outputConfiguration ?? throw new ArgumentNullException(nameof(outputConfiguration));
            NavigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));

            IsRoot = isRoot;
            IsEnumerable = isEnumerable;
        }

        public string Name => EntityFrameworkCoreQueryNames.Include;

        public bool IsRoot { get; }

        public bool IsEnumerable { get; }

        public Type InputPropertyType => typeof(TInputProperty);

        public Type OutputPropertyType => typeof(TOutputProperty);

        public IQueryConfiguration<TEntity> OutputConfiguration { get; }

        public Expression<Func<TInputProperty, TOutputProperty>> NavigationPropertyPath { get; }

        public IReadOnlyList<IQuerySpecification<TEntity>> OutputSpecifications => _specifications;

        void IQueryConfigurator<TEntity>.AddSpecification(IQuerySpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            _specifications.Add(specification);
        }

    }
}