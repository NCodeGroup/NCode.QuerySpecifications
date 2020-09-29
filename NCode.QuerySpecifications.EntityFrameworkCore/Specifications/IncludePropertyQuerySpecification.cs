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
using System.Linq.Expressions;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface IIncludePropertyQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        bool IsRoot { get; }

        bool IsEnumerable { get; }

        Type InputPropertyType { get; }

        Type OutputPropertyType { get; }
    }

    public interface IIncludePropertyQuerySpecification<TEntity, TInputProperty, TOutputProperty> : IIncludePropertyQuerySpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TInputProperty, TOutputProperty>> NavigationPropertyPath { get; }
    }

    public class IncludePropertyQuerySpecification<TEntity, TInputProperty, TOutputProperty> : IIncludePropertyQuerySpecification<TEntity, TInputProperty, TOutputProperty>
        where TEntity : class
    {
        public IncludePropertyQuerySpecification(Expression<Func<TInputProperty, TOutputProperty>> navigationPropertyPath, bool isRoot, bool isEnumerable)
        {
            NavigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));

            IsRoot = isRoot;
            IsEnumerable = isEnumerable;
        }

        public string Name => EntityFrameworkCoreQueryNames.Include;

        public bool IsRoot { get; }

        public bool IsEnumerable { get; }

        public Type InputPropertyType => typeof(TInputProperty);

        public Type OutputPropertyType => typeof(TOutputProperty);

        public Expression<Func<TInputProperty, TOutputProperty>> NavigationPropertyPath { get; }
    }
}