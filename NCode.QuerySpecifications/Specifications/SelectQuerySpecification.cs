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
using System.Linq.Expressions;
using NCode.QuerySpecifications.Introspection;
using NCode.QuerySpecifications.Pipes;

namespace NCode.QuerySpecifications.Specifications
{
    internal class SelectQuerySpecification<TIn, TOut> : IQuerySpecification<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        public SelectQuerySpecification(Expression<Func<TIn, TOut>> selector)
        {
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public Expression<Func<TIn, TOut>> Selector { get; }

        public IQueryPipe<TIn, TOut> Build()
        {
            return new SelectQueryPipe<TIn, TOut>(Selector);
        }

        public void Probe(IProbeContext context)
        {
            var scope = context.CreateScope("select");

            scope.Add("selector", Selector.ToString());
        }

    }
}