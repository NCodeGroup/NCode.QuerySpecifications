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
}