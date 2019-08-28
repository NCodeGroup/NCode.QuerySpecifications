using System.Collections.Generic;
using System.Linq;

namespace NCode.QuerySpecifications.Providers
{
	public interface ICompositeQueryFactory : IQueryPipeFactory, IQueryTransformFactory
	{
		// nothing
	}

	public class CompositeQueryFactory : ICompositeQueryFactory
	{
		private readonly IReadOnlyDictionary<string, IQueryPipeFactory[]> _pipeFactories;
		private readonly IReadOnlyDictionary<string, IQueryTransformFactory[]> _transformFactories;

		public string Name => "Composite";

		public CompositeQueryFactory(IEnumerable<IQueryPipeFactory> pipeFactories, IEnumerable<IQueryTransformFactory> transformFactories)
		{
			_pipeFactories = pipeFactories
				.GroupBy(factory => factory.Name)
				.ToDictionary(grouping => grouping.Key, grouping => grouping.ToArray());

			_transformFactories = transformFactories
				.GroupBy(factory => factory.Name)
				.ToDictionary(grouping => grouping.Key, grouping => grouping.ToArray());
		}

		public virtual bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> queryPipe)
			where TEntity : class
		{
			if (_pipeFactories.TryGetValue(specification.Name, out var factories))
			{
				foreach (var factory in factories)
				{
					if (factory.TryCreate(specification, out queryPipe))
					{
						return true;
					}
				}
			}

			queryPipe = null;
			return false;
		}

		public virtual bool TryCreate<TIn, TOut>(ITransformSpecification<TIn, TOut> specification, out IQueryTransform<TIn, TOut> transform)
			where TIn : class
			where TOut : class
		{
			if (_transformFactories.TryGetValue(specification.Name, out var factories))
			{
				foreach (var factory in factories)
				{
					if (factory.TryCreate(specification, out transform))
					{
						return true;
					}
				}
			}

			transform = null;
			return false;
		}

	}
}