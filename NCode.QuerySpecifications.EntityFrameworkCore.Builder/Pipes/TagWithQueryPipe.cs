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
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NCode.QuerySpecifications.Builder.Pipes;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes
{
    public class TagWithQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly string _tag;

        public TagWithQueryPipe(string tag)
        {
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.TagWith(_tag);
        }
    }
}