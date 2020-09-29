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
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Specifications
{
    public interface IOrderByQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        Type PropertyType { get; }

        bool Descending { get; }

        bool IsRoot { get; }
    }

    public interface IOrderByQuerySpecification<TEntity, TProperty> : IOrderByQuerySpecification<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, TProperty>> KeySelector { get; }

        IComparer<TProperty> Comparer { get; }
    }

    public class OrderByQuerySpecification<TEntity, TProperty> : IOrderByQuerySpecification<TEntity, TProperty>
        where TEntity : class
    {
        public OrderByQuerySpecification(Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer, bool @descending, bool isRoot)
        {
            KeySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
            Comparer = comparer;
            Descending = @descending;
            IsRoot = isRoot;
        }

        public string Name => QueryNames.OrderBy;

        public Type PropertyType => typeof(TProperty);

        public Expression<Func<TEntity, TProperty>> KeySelector { get; }

        public IComparer<TProperty> Comparer { get; }

        public bool Descending { get; }

        public bool IsRoot { get; }
    }
}