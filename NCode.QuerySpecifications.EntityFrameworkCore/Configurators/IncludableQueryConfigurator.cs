using System;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Configurators
{
    public interface IIncludableQueryConfigurator<TEntity, TProperty> : IQueryConfigurator<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class IncludableQueryConfigurator<TEntity, TProperty> : IIncludableQueryConfigurator<TEntity, TProperty>
        where TEntity : class
    {
        private readonly IQueryConfigurator<TEntity> _parentConfigurator;

        public IQueryConfiguration<TEntity> OutputConfiguration => _parentConfigurator.OutputConfiguration;

        public IncludableQueryConfigurator(IQueryConfigurator<TEntity> parentConfigurator)
        {
            _parentConfigurator = parentConfigurator ?? throw new ArgumentNullException(nameof(parentConfigurator));
        }

        void IQueryConfigurator<TEntity>.AddSpecification(IQuerySpecification<TEntity> specification)
        {
            _parentConfigurator.AddSpecification(specification);
        }

    }
}