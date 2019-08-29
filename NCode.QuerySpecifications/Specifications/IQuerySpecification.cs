using NCode.QuerySpecifications.Configuration;

namespace NCode.QuerySpecifications.Specifications
{
    public interface IQuerySpecification<TEntity> : IQueryName, IQueryConfiguration<TEntity>
        where TEntity : class
    {
        // nothing
    }
}