using System.ComponentModel;

namespace NCode.QuerySpecifications
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