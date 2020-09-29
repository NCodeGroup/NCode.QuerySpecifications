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

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class OrderByQueryPipe<TEntity, TProperty> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly IComparer<TProperty> _comparer;
        private readonly bool _descending;
        private readonly bool _isRoot;
        private readonly Expression<Func<TEntity, TProperty>> _keySelector;

        public OrderByQueryPipe(Expression<Func<TEntity, TProperty>> keySelector, IComparer<TProperty> comparer, bool @descending, bool isRoot)
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));

            _comparer = comparer;
            _descending = @descending;
            _isRoot = isRoot;
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            if (!_isRoot && queryRoot is IOrderedQueryable<TEntity> orderedQueryable)
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