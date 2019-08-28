using System;
using System.Linq;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Providers
{
	public class SelectQueryTransform<TIn, TOut> : IQueryTransform<TIn, TOut>
		where TIn : class
		where TOut : class
	{
		private readonly Expression<Func<TIn, TOut>> _expression;

		public SelectQueryTransform(Expression<Func<TIn, TOut>> expression)
		{
			_expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

		public virtual IQueryable<TOut> Apply(IQueryable<TIn> queryRoot)
		{
			return queryRoot.Select(_expression);
		}

	}
}