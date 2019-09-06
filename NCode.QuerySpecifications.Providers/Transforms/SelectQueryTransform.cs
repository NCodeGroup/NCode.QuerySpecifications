using System;
using System.Linq;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Provider.Transforms
{
    public class SelectQueryTransform<TIn, TOut> : IQueryTransform<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        private readonly Expression<Func<TIn, TOut>> _selector;

        public SelectQueryTransform(Expression<Func<TIn, TOut>> selector)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public virtual IQueryable<TOut> Apply(IQueryable<TIn> queryRoot)
        {
            return queryRoot.Select(_selector);
        }

    }
}