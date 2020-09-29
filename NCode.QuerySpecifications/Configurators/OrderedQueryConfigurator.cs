using System;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public interface IOrderedQueryConfigurator<TEntity> : IQueryConfigurator<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class OrderedQueryConfigurator<TEntity> : IOrderedQueryConfigurator<TEntity>
        where TEntity : class
    {
        private readonly IQueryConfigurator<TEntity> _parentConfigurator;

        public IQueryConfiguration<TEntity> OutputConfiguration => _parentConfigurator.OutputConfiguration;

        public OrderedQueryConfigurator(IQueryConfigurator<TEntity> parentConfigurator)
        {
            _parentConfigurator = parentConfigurator ?? throw new ArgumentNullException(nameof(parentConfigurator));
        }

        void IQueryConfigurator<TEntity>.AddSpecification(IQuerySpecification<TEntity> specification)
        {
            _parentConfigurator.AddSpecification(specification);
        }

    }
}