using System;
using System.Collections.Generic;
using System.Linq;

namespace NCode.QuerySpecifications
{
	public class SelectTransformConfigurator<TIn, TOut> : ITransformConfigurator<TIn, TOut>, ITransformConfiguration<TIn, TOut>, IQueryConfiguration<TOut>
		where TIn : class
		where TOut : class
	{
		private readonly List<IQuerySpecification<TOut>> _outputSpecifications = new List<IQuerySpecification<TOut>>();

		public SelectTransformConfigurator(ITransformSpecification<TIn, TOut> transformSpecification, IEnumerable<IQuerySpecification<TIn>> inputSpecifications)
		{
			if (inputSpecifications == null)
				throw new ArgumentNullException(nameof(inputSpecifications));

			TransformSpecification = transformSpecification ?? throw new ArgumentNullException(nameof(transformSpecification));
			InputSpecifications = inputSpecifications.ToList();
		}

		#region IQueryConfigurator<TOut> Members

		void IQueryConfigurator<TOut>.AddSpecification(IQuerySpecification<TOut> specification)
		{
			_outputSpecifications.Add(specification);
		}

		#endregion

		#region IQueryConfiguration<TOut> Members

		IQueryConfiguration<TOut> IQueryConfigurator<TOut>.OutputConfiguration => this;

		IReadOnlyList<IQuerySpecification<TOut>> IQueryConfiguration<TOut>.Specifications => _outputSpecifications;

		#endregion

		#region ITransformConfigurator<TIn, TOut> Members

		public ITransformConfiguration<TIn, TOut> TransformConfiguration => this;

		#endregion

		#region ITransformConfiguration<TIn, TOut> Members

		public ITransformSpecification<TIn, TOut> TransformSpecification { get; }

		public IReadOnlyList<IQuerySpecification<TIn>> InputSpecifications { get; }

		public IReadOnlyList<IQuerySpecification<TOut>> OutputSpecifications => _outputSpecifications;

		#endregion

	}
}