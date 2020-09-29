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

using System.Collections.Generic;
using System.Linq;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public interface ICompositeQueryPipeFactory : IQueryPipeFactory, IQueryPipeTransformFactory
    {
        // nothing
    }

    public class CompositeQueryPipeFactory : ICompositeQueryPipeFactory
    {
        private readonly IReadOnlyDictionary<string, IQueryPipeFactory[]> _pipeFactories;
        private readonly IReadOnlyDictionary<string, IQueryPipeTransformFactory[]> _transformFactories;

        public CompositeQueryPipeFactory(IEnumerable<IQueryPipeFactory> pipeFactories, IEnumerable<IQueryPipeTransformFactory> transformFactories)
        {
            _pipeFactories = pipeFactories
                .GroupBy(factory => factory.Name)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.ToArray());

            _transformFactories = transformFactories
                .GroupBy(factory => factory.Name)
                .ToDictionary(grouping => grouping.Key, grouping => grouping.ToArray());
        }

        public string Name => "Composite";

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (_pipeFactories.TryGetValue(specification.Name, out var factories))
            {
                foreach (var factory in factories)
                {
                    if (factory.TryCreate(specification, out pipe))
                    {
                        return true;
                    }
                }
            }

            pipe = null;
            return false;
        }

        public bool TryCreate<TIn, TOut>(IQuerySpecification<TIn, TOut> specification, out IQueryPipe<TIn, TOut> pipe)
            where TIn : class
            where TOut : class
        {
            if (_transformFactories.TryGetValue(specification.Name, out var factories))
            {
                foreach (var factory in factories)
                {
                    if (factory.TryCreate(specification, out pipe))
                    {
                        return true;
                    }
                }
            }

            pipe = null;
            return false;
        }
    }
}