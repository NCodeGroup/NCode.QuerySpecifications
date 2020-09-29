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
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface ITagWithQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        string Tag { get; }
    }

    public class TagWithQuerySpecification<TEntity> : ITagWithQuerySpecification<TEntity>
        where TEntity : class
    {
        public TagWithQuerySpecification(string tag)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        public string Name => EntityFrameworkCoreQueryNames.TagWith;

        public string Tag { get; }
    }
}