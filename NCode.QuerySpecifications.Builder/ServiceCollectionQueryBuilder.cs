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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NCode.QuerySpecifications.Builder.Factories;

namespace NCode.QuerySpecifications.Builder
{
    public interface IServiceCollectionQueryBuilder
    {
        IServiceCollectionQueryBuilder AddPipeFactory<TFactory>()
            where TFactory : class, IQueryPipeFactory;

        IServiceCollectionQueryBuilder AddPipeTransformFactory<TFactory>()
            where TFactory : class, IQueryPipeTransformFactory;
    }

    public class ServiceCollectionQueryBuilder : IServiceCollectionQueryBuilder
    {
        public ServiceCollectionQueryBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
        }

        public IServiceCollection ServiceCollection { get; }

        public IServiceCollectionQueryBuilder AddPipeFactory<TFactory>()
            where TFactory : class, IQueryPipeFactory
        {
            ServiceCollection.TryAddEnumerable(ServiceDescriptor.Transient<IQueryPipeFactory, TFactory>());
            return this;
        }

        public IServiceCollectionQueryBuilder AddPipeTransformFactory<TFactory>()
            where TFactory : class, IQueryPipeTransformFactory
        {
            ServiceCollection.TryAddEnumerable(ServiceDescriptor.Transient<IQueryPipeTransformFactory, TFactory>());
            return this;
        }
    }
}