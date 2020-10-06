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
using System.Linq;
using NCode.QuerySpecifications.Introspection;
using NCode.QuerySpecifications.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public interface IQueryConfigurator<T> : IQuerySpecification<T, T>
        where T : class
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        void AddSpecification(IQuerySpecification<T, T> specification);
    }

    internal class QueryConfigurator<T> : IQueryConfigurator<T>
        where T : class
    {
        private readonly IList<IQuerySpecification<T, T>> _specifications = new List<IQuerySpecification<T, T>>();

        public void AddSpecification(IQuerySpecification<T, T> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            _specifications.Add(specification);
        }

        public IQueryPipe<T, T> Build()
        {
            if (_specifications.Count == 1)
                return _specifications[0].Build();

            var pipes = _specifications.Select(specification => specification.Build()).ToList();
            return new CompositeQueryPipe<T>(pipes);
        }

        public void Probe(IProbeContext context)
        {
            var scope = context.CreateScope("queryConfigurator");

            scope.Add("inputType", typeof(T));
            scope.Add("count", _specifications.Count);

            foreach (var specification in _specifications)
            {
                specification.Probe(scope);
            }
        }

    }
}