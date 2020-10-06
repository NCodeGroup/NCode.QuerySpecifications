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
using NCode.QuerySpecifications.Introspection;
using NCode.QuerySpecifications.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public interface IOrderedQueryConfigurator<T> : IQueryConfigurator<T>
        where T : class
    {
        // nothing
    }

    internal class OrderedQueryConfigurator<T> : IOrderedQueryConfigurator<T>
        where T : class
    {
        private readonly IQueryConfigurator<T> _parentConfigurator;

        public OrderedQueryConfigurator(IQueryConfigurator<T> parentConfigurator)
        {
            _parentConfigurator = parentConfigurator ?? throw new ArgumentNullException(nameof(parentConfigurator));
        }

        public void AddSpecification(IQuerySpecification<T, T> specification)
        {
            _parentConfigurator.AddSpecification(specification);
        }

        public IQueryPipe<T, T> Build()
        {
            return _parentConfigurator.Build();
        }

        public void Probe(IProbeContext context)
        {
            _parentConfigurator.Probe(context);
        }

    }
}