using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.EntityFrameworkCore.Specifications;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Configurators
{
	public class IncludePropertyQueryConfigurator<TEntity, TInputProperty, TOutputProperty> :
        IIncludePropertyQueryConfigurator<TEntity, TOutputProperty>,
        IIncludePropertyQuerySpecification<TEntity, TInputProperty, TOutputProperty>
        where TEntity : class
    {
        private readonly List<IQuerySpecification<TEntity>> _specifications = new List<IQuerySpecification<TEntity>>();

        public IncludePropertyQueryConfigurator(IQueryConfiguration<TEntity> outputConfiguration, Expression<Func<TInputProperty, TOutputProperty>> navigationPropertyPath, bool isRoot, bool isEnumerable)
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