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
using NCode.QuerySpecifications.Configuration;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public interface IOrderedQueryConfigurator<TEntity> : IQueryConfigurator<TEntity>
        where TEntity : class
    {
        // nothing
    }

    public class OrderedQueryConfigurator<TEntity> : IOrderedQueryConfigurator<TEntity>
        where TEntity : class
    {
        private readonly IQueryConfigurator<TEntity> _parentConfigurator;

        public OrderedQueryConfigurator(IQueryConfigurator<TEntity> parentConfigurator)
        {
            _parentConfigurator = parentConfigurator ?? throw new ArgumentNullException(nameof(parentConfigurator));
        }

        public IQueryConfiguration<TEntity> OutputConfiguration => _parentConfigurator.OutputConfiguration;

        void IQueryConfigurator<TEntity>.AddSpecification(IQuerySpecification<TEntity> specification)
        {
            _parentConfigurator.AddSpecification(specification);
        }
    }
}