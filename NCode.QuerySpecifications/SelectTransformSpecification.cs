using System;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications
{
	public class SelectTransformSpecification<TIn, TOut> : ITransformSpecification<TIn, TOut>
		where TIn : class
		where TOut : class
	{
		public string Name => QueryNames.Select;

		public Expression<Func<TIn, TOut>> Expression { get; }

		public SelectTransformSpecification(Expression<Func<TIn, TOut>> expression)
		{
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

	}
}