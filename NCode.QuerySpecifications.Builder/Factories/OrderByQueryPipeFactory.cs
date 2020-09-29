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
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Builder.Factories
{
    public class OrderByQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => QueryNames.OrderBy;

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (specification is IOrderByQuerySpecification<TEntity> orderBySpec)
            {
                var factoryType = typeof(Factory<>).MakeGenericType(orderBySpec.PropertyType);
                var factory = (IQueryPipeFactory)Activator.CreateInstance(factoryType);
                return factory.TryCreate(specification, out pipe);
            }

            pipe = null;
            return false;
        }

        private class Factory<TProperty> : IQueryPipeFactory
        {
            public string Name => QueryNames.OrderBy;

            public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
                where TEntity : class
            {
                if (specification is IOrderByQuerySpecification<TEntity, TProperty> orderBySpec)
                {
                    pipe = new OrderByQueryPipe<TEntity, TProperty>(orderBySpec.KeySelector, orderBySpec.Comparer, orderBySpec.Descending, orderBySpec.IsRoot);
                    return true;
                }

                pipe = null;
                return false;
            }
        }
    }
}