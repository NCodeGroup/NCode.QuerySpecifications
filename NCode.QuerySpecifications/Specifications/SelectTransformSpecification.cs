﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public class SelectTransformSpecification<TIn, TOut> : ITransformSpecification<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        public string Name => QueryNames.Select;

        public Expression<Func<TIn, TOut>> Expression { get; }

        public SelectTransformSpecification(Expression<Func<TIn, TOut>> expression, IReadOnlyList<IQuerySpecification<TIn>> inputSpecifications, IReadOnlyList<IQuerySpecification<TOut>> outputSpecifications)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            InputSpecifications = inputSpecifications ?? throw new ArgumentNullException(nameof(inputSpecifications));
            OutputSpecifications = outputSpecifications ?? throw new ArgumentNullException(nameof(outputSpecifications));
        }

        public ITransformSpecification<TIn, TOut> TransformSpecification => this;

        public IReadOnlyList<IQuerySpecification<TIn>> InputSpecifications { get; }

        public IReadOnlyList<IQuerySpecification<TOut>> OutputSpecifications { get; }
    }
}