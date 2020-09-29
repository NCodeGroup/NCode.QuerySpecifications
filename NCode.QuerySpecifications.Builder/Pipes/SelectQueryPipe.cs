﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class SelectQueryPipe<TIn, TOut> : IQueryPipe<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        private readonly Expression<Func<TIn, TOut>> _selector;

        public SelectQueryPipe(Expression<Func<TIn, TOut>> selector)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public IQueryable<TOut> Apply(IQueryable<TIn> queryRoot)
        {
            return queryRoot.Select(_selector);
        }

    }
}