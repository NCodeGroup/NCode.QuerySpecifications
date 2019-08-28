using System.Collections.Generic;

namespace NCode.QuerySpecifications
{
	public interface IQueryConfiguration<TEntity>
		where TEntity : class
	{
		IReadOnlyList<IQuerySpecification<TEntity>> Specifications { get; }
	}
}