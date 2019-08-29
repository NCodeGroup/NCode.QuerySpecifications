namespace NCode.QuerySpecifications.Configurators
{
    public interface IOrderByQueryConfigurator<TEntity> : IQueryConfigurator<TEntity>
        where TEntity : class
    {
        // nothing
    }
}