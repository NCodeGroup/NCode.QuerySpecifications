﻿#region Copyright Preamble
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

using NCode.QuerySpecifications.Builder.Factories;
using NCode.QuerySpecifications.Builder.Pipes;
using NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes;
using NCode.QuerySpecifications.EntityFrameworkCore.Specifications;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Factories
{
    public class IgnoreQueryFiltersQueryPipeFactory : IQueryPipeFactory
    {
        public string Name => EntityFrameworkCoreQueryNames.IgnoreQueryFilters;

        public bool TryCreate<TEntity>(IQuerySpecification<TEntity> specification, out IQueryPipe<TEntity> pipe)
            where TEntity : class
        {
            if (specification is IIgnoreQueryFiltersQuerySpecification<TEntity>)
            {
                pipe = new IgnoreQueryFiltersQueryPipe<TEntity>();
                return true;
            }

            pipe = null;
            return false;
        }
    }
}