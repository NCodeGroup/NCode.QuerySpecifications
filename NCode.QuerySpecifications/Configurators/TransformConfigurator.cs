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
using System.ComponentModel;
using NCode.QuerySpecifications.Introspection;
using NCode.QuerySpecifications.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.Configurators
{
    public interface ITransformConfigurator<in TIn, TOut> : IQuerySpecification<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        IQueryConfigurator<TOut> OutputConfigurator { get; }
    }

    internal class TransformConfigurator<TIn, TOut> : ITransformConfigurator<TIn, TOut>
        where TIn : class
        where TOut : class
    {
        public TransformConfigurator(IQuerySpecification<TIn, TOut> transformSpecification, IQueryConfigurator<TIn> inputConfigurator)
        {
            TransformSpecification = transformSpecification ?? throw new ArgumentNullException(nameof(transformSpecification));
            InputConfigurator = inputConfigurator ?? throw new ArgumentNullException(nameof(inputConfigurator));
            OutputConfigurator = new QueryConfigurator<TOut>();
        }

        public IQuerySpecification<TIn, TOut> TransformSpecification { get; }

        public IQueryConfigurator<TIn> InputConfigurator { get; }

        public IQueryConfigurator<TOut> OutputConfigurator { get; }

        public IQueryPipe<TIn, TOut> Build()
        {
            var inputPipe = InputConfigurator.Build();
            var outputPipe = OutputConfigurator.Build();
            var transformPipe = TransformSpecification.Build();

            return new CompositeQueryTransform<TIn, TOut>(inputPipe, outputPipe, transformPipe);
        }

        public void Probe(IProbeContext context)
        {
            var scope = context.CreateScope("transformConfigurator");

            scope.Add("inputType", typeof(TIn));
            scope.Add("outputType", typeof(TOut));

            InputConfigurator.Probe(scope);
            TransformSpecification.Probe(scope);
            OutputConfigurator.Probe(scope);
        }

    }
}