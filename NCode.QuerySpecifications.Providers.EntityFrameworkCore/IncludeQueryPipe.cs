using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NCode.QuerySpecifications.Provider.Pipes;

namespace NCode.QuerySpecifications.Providers.EntityFrameworkCore
{
	public class IncludePathQueryPipe<TEntity> : IQueryPipe<TEntity>
		where TEntity : class
	{
		private readonly string _navigationPropertyPath;

		public IncludePathQueryPipe(string navigationPropertyPath)
		{
			_navigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));
		}

		public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
		{
			return queryRoot.Include(_navigationPropertyPath);
		}
	}

	public class IncludePropertyQueryPipe<TEntity, TProperty> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, TProperty>> _navigationPropertyPath;

        public IncludePropertyQueryPipe(Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            _navigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.Include(_navigationPropertyPath);
        }
    }

    public class IncludePropertyThenQueryPipe<TEntity, TInputProperty, TOutputProperty> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TInputProperty, TOutputProperty>> _navigationPropertyPath;
        private readonly bool _isEnumerable;

        public IncludePropertyThenQueryPipe(Expression<Func<TInputProperty, TOutputProperty>> navigationPropertyPath, bool isEnumerable)
        {
            _navigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));
            _isEnumerable = isEnumerable;
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            IQueryable<TEntity> output;

            if (_isEnumerable)
            {
                var query = (IIncludableQueryable<TEntity, IEnumerable<TInputProperty>>)queryRoot;
                output = query.ThenInclude(_navigationPropertyPath);
            }
            else
            {
                var query = (IIncludableQueryable<TEntity, TInputProperty>)queryRoot;
                output = query.ThenInclude(_navigationPropertyPath);
            }

            return output;
        }

    }
}