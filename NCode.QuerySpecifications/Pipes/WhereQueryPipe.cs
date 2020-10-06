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

using System;
using System.Linq;
using System.Linq.Expressions;
using NCode.QuerySpecifications.Introspection;

namespace NCode.QuerySpecifications.Pipes
{
    internal class WhereQueryPipe<T> : IQueryPipe<T, T>
        where T : class
    {
        private readonly Expression<Func<T, bool>> _predicate;

        public WhereQueryPipe(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public IQueryable<T> Apply(IQueryable<T> queryRoot)
        {
            return queryRoot.Where(_predicate);
        }

        public void Probe(IProbeContext context)
        {
            var scope = context.CreateScope("where");

            scope.Add("predicate", _predicate.ToString());
        }

    }
}