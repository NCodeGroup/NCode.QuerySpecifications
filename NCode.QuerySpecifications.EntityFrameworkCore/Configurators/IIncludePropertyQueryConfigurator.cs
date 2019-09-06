using NCode.QuerySpecifications.Configurators;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Configurators
{
	public interface IIncludePropertyQueryConfigurator<TEntity, TProperty> : IQueryConfigurator<TEntity>
		where TEntity : class
	{
		// nothing
	}
}