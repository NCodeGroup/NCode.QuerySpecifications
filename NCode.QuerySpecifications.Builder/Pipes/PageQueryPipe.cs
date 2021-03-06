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

using System.Linq;

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class PageQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly int _skip;
        private readonly int _take;

        public PageQueryPipe(int skip, int take)
        {
            _skip = skip;
            _take = take;
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.Skip(_skip).Take(_take);
        }
    }
}