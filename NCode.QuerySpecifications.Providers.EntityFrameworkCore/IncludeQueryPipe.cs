using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace NCode.QuerySpecifications.Providers.EntityFrameworkCore
{
	public class IncludeQueryPipe<TEntity, TProperty> : IQueryPipe<TEntity>
		where TEntity : class
	{
		private readonly Expression<Func<TEntity, TProperty>> _expression;

		public IncludeQueryPipe(Expression<Func<TEntity, TProperty>> expression)
		{
			_expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

		public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
		{
			return queryRoot.Include(_expression);
		}

	}

	public class ThenIncludeQueryPipe<TEntity, TInputProperty, TOutputProperty> : IQueryPipe<TEntity>
		where TEntity : class
	{
		private readonly Expression<Func<TInputProperty, TOutputProperty>> _expression;
		private readonly bool _isEnumerable;

		public ThenIncludeQueryPipe(Expression<Func<TInputProperty, TOutputProperty>> expression, bool isEnumerable)
		{
			_expression = expression ?? throw new ArgumentNullException(nameof(expression));
			_isEnumerable = isEnumerable;
		}

		public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
		{
			IQueryable<TEntity> output;

			if (_isEnumerable)
			{
				// ReSharper disable once IdentifierTypo
				var includable = (IIncludableQueryable<TEntity, IEnumerable<TInputProperty>>)queryRoot;
				output = includable.ThenInclude(_expression);
			}
			else
			{
				// ReSharper disable once IdentifierTypo
				var includable = (IIncludableQueryable<TEntity, TInputProperty>)queryRoot;
				output = includable.ThenInclude(_expression);
			}

			return output;
		}
	}
}