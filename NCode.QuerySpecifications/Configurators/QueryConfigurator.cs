using System;
using System.Collections.Generic;
using System.ComponentModel;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public interface IQueryConfigurator<TEntity>
        where TEntity : class
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        IQueryConfiguration<TEntity> OutputConfiguration { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void AddSpecification(IQuerySpecification<TEntity> specification);
    }

    public class QueryConfigurator<TEntity> : IQueryConfigurator<TEntity>, IQueryConfiguration<TEntity>
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