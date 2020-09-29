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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NCode.QuerySpecifications.Builder.Pipes;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Pipes
{
    public class IncludePathQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly string _navigationPropertyPath;

        public IncludePathQueryPipe(string navigationPropertyPath)
        {
            _navigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.Include(_navigationPropertyPath);
        }
    }

    public class IncludePropertyRootQueryPipe<TEntity, TProperty> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, TProperty>> _navigationPropertyPath;

        public IncludePropertyRootQueryPipe(Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            _navigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return queryRoot.Include(_navigationPropertyPath);
        }
    }

    public class IncludePropertyThenQueryPipe<TEntity, TInputProperty, TOutputProperty> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly bool _isEnumerable;
        private readonly Expression<Func<TInputProperty, TOutputProperty>> _navigationPropertyPath;

        public IncludePropertyThenQueryPipe(Expression<Func<TInputProperty, TOutputProperty>> navigationPropertyPath, bool isEnumerable)
        {
            _navigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));
            _isEnumerable = isEnumerable;
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            IQueryable<TEntity> output;

            if (_isEnumerable)
            {
                var query = (IIncludableQueryable<TEntity, IEnumerable<TInputProperty>>)queryRoot;
                output = query.ThenInclude(_navigationPropertyPath);
            }
            else
            {
                var query = (IIncludableQueryable<TEntity, TInputProperty>)queryRoot;
                output = query.ThenInclude(_navigationPropertyPath);
            }

            return output;
        }
    }
}