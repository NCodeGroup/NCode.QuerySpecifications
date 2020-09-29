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

namespace NCode.QuerySpecifications.Specifications
{
    public interface IPageQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        int Skip { get; }

        int Take { get; }
    }

    public class PageQuerySpecification<TEntity> : IPageQuerySpecification<TEntity>
        where TEntity : class
    {
        public PageQuerySpecification(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public string Name => QueryNames.Page;

        public int Skip { get; }

        public int Take { get; }
    }
}