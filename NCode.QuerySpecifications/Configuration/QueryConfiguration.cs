using System;
using System.Collections.Generic;
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configuration
{
    public class QueryConfiguration<TEntity> : IQueryConfigurator<TEntity>, IQueryConfiguration<TEntity>
        where TEntity : class
    {
        private readonly List<IQuerySpecification<TEntity>> _specifications = new List<IQuerySpecification<TEntity>>();

        public IQueryConfiguration<TEntity> OutputConfiguration => this;

        public IReadOnlyList<IQuerySpecification<TEntity>> OutputSpecifications => _specifications;

        void IQueryConfigurator<TEntity>.AddSpecification(IQuerySpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            _specifications.Add(specification);
        }

    }
}