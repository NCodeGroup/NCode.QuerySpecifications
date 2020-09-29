#region Copyright Preamble
// 
//    Copyright @ 2020 NCode Group
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public interface IQueryConfigurator<TEntity>
        where TEntity : class
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        IQueryConfiguration<TEntity> OutputConfiguration { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void AddSpecification(IQuerySpecification<TEntity> specification);
    }

    public interface IQueryConfigurator<TIn, TOut> : IQueryConfigurator<TOut>
        where TIn : class
        where TOut : class
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        IQueryConfiguration<TIn, TOut> TransformConfiguration { get; }
    }

    public class QueryConfigurator<TEntity> : IQueryConfigurator<TEntity>, IQueryConfiguration<TEntity>
        where TEntity : class
    {
        private readonly List<IQuerySpecification<TEntity>> _specifications = new List<IQuerySpecification<TEntity>>();

        public IReadOnlyList<IQuerySpecification<TEntity>> OutputSpecifications => _specifications;

        public IQueryConfiguration<TEntity> OutputConfiguration => this;

        void IQueryConfigurator<TEntity>.AddSpecification(IQuerySpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            _specifications.Add(specification);
        }
    }

    public class QueryConfigurator<TIn, TOut> : IQueryConfigurator<TIn, TOut>, IQueryConfiguration<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        private readonly List<IQuerySpecification<TOut>> _outputSpecifications = new List<IQuerySpecification<TOut>>();
        private readonly IQueryConfigurator<TIn> _parentConfigurator;

        public QueryConfigurator(IQueryConfigurator<TIn> parentConfigurator, IQuerySpecification<TIn, TOut> transformSpecification)
        {
            _parentConfigurator = parentConfigurator ?? throw new ArgumentNullException(nameof(parentConfigurator));
            TransformSpecification = transformSpecification ?? throw new ArgumentNullException(nameof(transformSpecification));
        }

        public IQuerySpecification<TIn, TOut> TransformSpecification { get; }

        public IReadOnlyList<IQuerySpecification<TIn>> InputSpecifications => _parentConfigurator.OutputConfiguration.OutputSpecifications;

        public IReadOnlyList<IQuerySpecification<TOut>> OutputSpecifications => _outputSpecifications;

        public IQueryConfiguration<TOut> OutputConfiguration => this;

        public IQueryConfiguration<TIn, TOut> TransformConfiguration => this;

        void IQueryConfigurator<TOut>.AddSpecification(IQuerySpecification<TOut> specification)
        {
            _outputSpecifications.Add(specification);
        }
    }
}