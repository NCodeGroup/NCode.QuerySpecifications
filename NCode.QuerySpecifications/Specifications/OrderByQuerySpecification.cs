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
using NCode.QuerySpecifications.Pipes;

namespace NCode.QuerySpecifications.Specifications
{
    internal class OrderByQuerySpecification<T, TProperty> : IQuerySpecification<T, T>
        where T : class
    {
        public OrderByQuerySpecification(Expression<Func<T, TProperty>> keySelector, IComparer<TProperty> comparer, bool @descending, bool isRoot)
        {
            KeySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
            Comparer = comparer;
            Descending = @descending;
            IsRoot = isRoot;
        }

        public Expression<Func<T, TProperty>> KeySelector { get; }

        public IComparer<TProperty> Comparer { get; }

        public bool Descending { get; }

        public bool IsRoot { get; }

        public IQueryPipe<T, T> Build()
        {
            return new OrderByQueryPipe<T, TProperty>(KeySelector, Comparer, Descending, IsRoot);
        }

    }
}