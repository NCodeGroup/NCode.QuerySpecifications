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
using System.Linq;

namespace NCode.QuerySpecifications.Pipes
{
    internal class CompositeQueryTransform<TIn, TOut> : IQueryPipe<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        private readonly IQueryPipe<TIn, TIn> _inputPipe;
        private readonly IQueryPipe<TOut, TOut> _outputPipe;
        private readonly IQueryPipe<TIn, TOut> _transformPipe;

        public CompositeQueryTransform(IQueryPipe<TIn, TIn> inputPipe, IQueryPipe<TOut, TOut> outputPipe, IQueryPipe<TIn, TOut> transformPipe)
        {
            _inputPipe = inputPipe ?? throw new ArgumentNullException(nameof(inputPipe));
            _outputPipe = outputPipe ?? throw new ArgumentNullException(nameof(outputPipe));
            _transformPipe = transformPipe ?? throw new ArgumentNullException(nameof(transformPipe));
        }

        public IQueryable<TOut> Apply(IQueryable<TIn> queryRoot)
        {
            var inputQuery = _inputPipe.Apply(queryRoot);

            var transformQuery = _transformPipe.Apply(inputQuery);

            var outputQuery = _outputPipe.Apply(transformQuery);

            return outputQuery;
        }

    }
}