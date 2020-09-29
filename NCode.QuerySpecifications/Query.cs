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
using NCode.QuerySpecifications.Configurators;

namespace NCode.QuerySpecifications
{
    public static class Query<TEntity>
        where TEntity : class
    {
        public static IQueryConfiguration<TEntity> Configure(Func<IQueryConfigurator<TEntity>, IQueryConfigurator<TEntity>> configureCallback)
        {
            if (configureCallback == null)
                throw new ArgumentNullException(nameof(configureCallback));

            var inputConfigurator = new QueryConfigurator<TEntity>();

            var outputConfigurator = configureCallback(inputConfigurator);

            return outputConfigurator.OutputConfiguration;
        }

        public static IQueryConfiguration<TEntity, TOut> Configure<TOut>(Func<IQueryConfigurator<TEntity>, IQueryConfigurator<TEntity, TOut>> configureCallback)
            where TOut : class
        {
            if (configureCallback == null)
                throw new ArgumentNullException(nameof(configureCallback));

            var inputConfigurator = new QueryConfigurator<TEntity>();

            var transformConfigurator = configureCallback(inputConfigurator);

            return transformConfigurator.TransformConfiguration;
        }
    }
}