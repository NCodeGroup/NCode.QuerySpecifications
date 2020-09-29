using System;
using System.Collections.Generic;
using System.ComponentModel;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public interface ITransformConfigurator<TIn, TOut> : IQueryConfigurator<TOut>
        where TIn : class
        where TOut : class
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        ITransformConfiguration<TIn, TOut> TransformConfiguration { get; }
    }

    public class TransformConfigurator<TIn, TOut> : ITransformConfigurator<TIn, TOut>, ITransformConfiguration<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        private readonly IQueryConfigurator<TIn> _parentConfigurator;
        private readonly List<IQuerySpecification<TOut>> _outputSpecifications = new List<IQuerySpecification<TOut>>();

        public IQueryConfiguration<TOut> OutputConfiguration => this;

        public ITransformConfiguration<TIn, TOut> TransformConfiguration => this;

        public ITransformSpecification<TIn, TOut> TransformSpecification { get; }

        public IReadOnlyList<IQuerySpecification<TIn>> InputSpecifications => _parentConfigurator.OutputConfiguration.OutputSpecifications;

        public IReadOnlyList<IQuerySpecification<TOut>> OutputSpecifications => _outputSpecifications;

        public TransformConfigurator(IQueryConfigurator<TIn> parentConfigurator, ITransformSpecification<TIn, TOut> transformSpecification)
        {
            _parentConfigurator = parentConfigurator ?? throw new ArgumentNullException(nameof(parentConfigurator));
            TransformSpecification = transformSpecification ?? throw new ArgumentNullException(nameof(transformSpecification));
        }

        void IQueryConfigurator<TOut>.AddSpecification(IQuerySpecification<TOut> specification)
        {
            _outputSpecifications.Add(specification);
        }

    }
}