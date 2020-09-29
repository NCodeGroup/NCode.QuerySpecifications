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

namespace NCode.QuerySpecifications.Builder.Pipes
{
    public class CompositeQueryPipe<TEntity> : IQueryPipe<TEntity>
        where TEntity : class
    {
        private readonly IReadOnlyCollection<IQueryPipe<TEntity>> _pipes;

        public CompositeQueryPipe(IEnumerable<IQueryPipe<TEntity>> pipes)
        {
            _pipes = pipes?.ToArray() ?? throw new ArgumentNullException(nameof(pipes));
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryRoot)
        {
            return _pipes.Aggregate(queryRoot, (current, pipe) => pipe.Apply(current));
        }
    }
}