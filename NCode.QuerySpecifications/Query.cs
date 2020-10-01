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
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.Pipes;

namespace NCode.QuerySpecifications
{
    public static class Query<TIn>
        where TIn : class
    {
        public static IQueryPipe<TIn, TIn> Build(Func<IQueryConfigurator<TIn>, IQueryConfigurator<TIn>> configureCallback)
        {
            if (configureCallback == null)
                throw new ArgumentNullException(nameof(configureCallback));

            var input = new QueryConfigurator<TIn>();

            var output = configureCallback(input);

            return output.Build();
        }

        public static IQueryPipe<TIn, TOut> Build<TOut>(Func<IQueryConfigurator<TIn>, ITransformConfigurator<TIn, TOut>> configureCallback)
            where TOut : class
        {
            if (configureCallback == null)
                throw new ArgumentNullException(nameof(configureCallback));

            var input = new QueryConfigurator<TIn>();

            var output = configureCallback(input);

            return output.Build();
        }

    }
}