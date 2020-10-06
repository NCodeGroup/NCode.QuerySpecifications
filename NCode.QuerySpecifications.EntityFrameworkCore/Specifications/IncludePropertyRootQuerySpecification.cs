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
using NCode.QuerySpecifications.EntityFrameworkCore.Pipes;
using NCode.QuerySpecifications.Introspection;
using NCode.QuerySpecifications.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    internal class IncludePropertyRootQuerySpecification<T, TProperty> : IQuerySpecification<T, T>
        where T : class
    {
        public IncludePropertyRootQuerySpecification(Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            NavigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));
        }

        public Expression<Func<T, TProperty>> NavigationPropertyPath { get; }

        public IQueryPipe<T, T> Build()
        {
            return new IncludePropertyRootQueryPipe<T, TProperty>(NavigationPropertyPath);
        }

        public void Probe(IProbeContext context)
        {
            var scope = context.CreateScope("includePropertyRoot");

            scope.Add("navigationPropertyPath", NavigationPropertyPath.ToString());
        }

    }
}