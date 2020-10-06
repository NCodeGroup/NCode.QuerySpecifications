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

using System.Collections.Generic;
using System.Linq;
using NCode.QuerySpecifications.Introspection;

namespace NCode.QuerySpecifications.Pipes
{
    internal class DistinctQueryPipe<T> : IQueryPipe<T, T>
        where T : class
    {
        private readonly IEqualityComparer<T> _comparer;

        public DistinctQueryPipe(IEqualityComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public IQueryable<T> Apply(IQueryable<T> queryRoot)
        {
            return _comparer == null
                ? queryRoot.Distinct()
                : queryRoot.Distinct(_comparer);
        }

        public void Probe(IProbeContext context)
        {
            var scope = context.CreateScope("distinct");

            scope.Add("comparerType", _comparer?.GetType());
        }

    }
}