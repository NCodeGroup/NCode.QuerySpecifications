using System;
using System.Collections.Generic;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public class TransformConfigurator<TIn, TOut> : ITransformConfigurator<TIn, TOut>, ITransformConfiguration<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        private readonly List<IQuerySpecification<TOut>> _outputSpecifications;

        public TransformConfigurator(ITransformSpecification<TIn, TOut> transformSpecification, IReadOnlyList<IQuerySpecification<TIn>> inputSpecifications, List<IQuerySpecification<TOut>> outputSpecifications)
        {
            TransformSpecification = transformSpecification ?? throw new ArgumentNullException(nameof(transformSpecification));
            InputSpecifications = inputSpecifications ?? throw new ArgumentNullException(nameof(inputSpecifications));
            _outputSpecifications = outputSpecifications ?? throw new ArgumentNullException(nameof(outputSpecifications));
        }

        #region IQueryConfigurator<TOut> Members

        void IQueryConfigurator<TOut>.AddSpecification(IQuerySpecification<TOut> specification)
        {
            _outputSpecifications.Add(specification);
        }

        #endregion

        #region IQueryConfiguration<TOut> Members

        public IQueryConfiguration<TOut> OutputConfiguration => this;

        public IReadOnlyList<IQuerySpecification<TOut>> OutputSpecifications => _outputSpecifications;

        #endregion

        #region ITransformConfigurator<TIn, TOut> Members

        public ITransformConfiguration<TIn, TOut> TransformConfiguration => this;

        #endregion

        #region ITransformConfiguration<TIn, TOut> Members

        public ITransformSpecification<TIn, TOut> TransformSpecification { get; }

        public IReadOnlyList<IQuerySpecification<TIn>> InputSpecifications { get; }

        #endregion

    }
}