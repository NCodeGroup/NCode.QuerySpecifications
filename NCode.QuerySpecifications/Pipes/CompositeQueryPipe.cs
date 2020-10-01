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

namespace NCode.QuerySpecifications.Pipes
{
    internal class CompositeQueryPipe<T> : IQueryPipe<T, T>
        where T : class
    {
        private readonly IEnumerable<IQueryPipe<T, T>> _pipes;

        public CompositeQueryPipe(IReadOnlyCollection<IQueryPipe<T, T>> pipes)
        {
            _pipes = pipes ?? throw new ArgumentNullException(nameof(pipes));
        }

        public IQueryable<T> Apply(IQueryable<T> queryRoot)
        {
            return _pipes.Aggregate(queryRoot, (current, pipe) => pipe.Apply(current));
        }

    }
}