using System;
using System.Collections.Generic;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public class OrderByQueryConfigurator<TEntity> : IOrderByQueryConfigurator<TEntity>
        where TEntity : class
    {
        private readonly List<IQuerySpecification<TEntity>> _specifications = new List<IQuerySpecification<TEntity>>();

        public OrderByQueryConfigurator(IQueryConfiguration<TEntity> outputConfiguration)
        {
            OutputConfiguration = outputConfiguration ?? throw new ArgumentNullException(nameof(outputConfiguration));
        }

        public IQueryConfiguration<TEntity> OutputConfiguration { get; }

        public IReadOnlyList<IQuerySpecification<TEntity>> Specifications => _specifications;

        void IQueryConfigurator<TEntity>.AddSpecification(IQuerySpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            _specifications.Add(specification);
        }

    }
}