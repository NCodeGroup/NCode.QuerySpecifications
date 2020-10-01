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
using System.Linq;
using System.Linq.Expressions;

namespace NCode.QuerySpecifications.Pipes
{
    internal class OrderByQueryPipe<T, TProperty> : IQueryPipe<T, T>
        where T : class
    {
        private readonly Expression<Func<T, TProperty>> _keySelector;
        private readonly IComparer<TProperty> _comparer;
        private readonly bool _descending;
        private readonly bool _isRoot;

        public OrderByQueryPipe(Expression<Func<T, TProperty>> keySelector, IComparer<TProperty> comparer, bool @descending, bool isRoot)
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));

            _comparer = comparer;
            _descending = @descending;
            _isRoot = isRoot;
        }

        public IQueryable<T> Apply(IQueryable<T> queryRoot)
        {
            if (!_isRoot && queryRoot is IOrderedQueryable<T> orderedQueryable)
            {
                return _descending
                    ? _comparer == null
                        ? orderedQueryable.ThenByDescending(_keySelector)
                        : orderedQueryable.ThenByDescending(_keySelector, _comparer)
                    : _comparer == null
                        ? orderedQueryable.ThenBy(_keySelector)
                        : orderedQueryable.ThenBy(_keySelector, _comparer);
            }

            return _descending
                ? _comparer == null
                    ? queryRoot.OrderByDescending(_keySelector)
                    : queryRoot.OrderByDescending(_keySelector, _comparer)
                : _comparer == null
                    ? queryRoot.OrderBy(_keySelector)
                    : queryRoot.OrderBy(_keySelector, _comparer);

        }

    }
}