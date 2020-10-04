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
using System.Linq.Expressions;
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.Pipes;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications
{
    public static class ConfiguratorExtensions
    {
        public static IQueryConfigurator<T> UseQuerySpecification<T>(this IQueryConfigurator<T> configurator, IQuerySpecification<T, T> querySpecification)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (configurator == null)
                throw new ArgumentNullException(nameof(querySpecification));

            configurator.AddSpecification(querySpecification);

            return configurator;
        }

        public static ITransformConfigurator<TIn, TOut> UseTransformSpecification<TIn, TOut>(this IQueryConfigurator<TIn> configurator, IQuerySpecification<TIn, TOut> transformSpecification)
            where TIn : class
            where TOut : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (transformSpecification == null)
                throw new ArgumentNullException(nameof(transformSpecification));

            var nextConfigurator = new TransformConfigurator<TIn, TOut>(transformSpecification, configurator);

            return nextConfigurator;
        }

        public static IQueryConfigurator<T> UseQuery<T>(this IQueryConfigurator<T> configurator, IQueryPipe<T, T> queryPipe)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (queryPipe == null)
                throw new ArgumentNullException(nameof(queryPipe));

            var specification = new PipeQuerySpecification<T>(queryPipe);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static ITransformConfigurator<TIn, TOut> UseTransform<TIn, TOut>(this IQueryConfigurator<TIn> configurator, IQueryPipe<TIn, TOut> transformPipe)
            where TIn : class
            where TOut : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (transformPipe == null)
                throw new ArgumentNullException(nameof(transformPipe));

            var transformSpecification = new TransformQuerySpecification<TIn, TOut>(transformPipe);
            var nextConfigurator = new TransformConfigurator<TIn, TOut>(transformSpecification, configurator);

            return nextConfigurator;
        }

        public static IQueryConfigurator<T> Where<T>(this IQueryConfigurator<T> configurator, Expression<Func<T, bool>> predicate)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var specification = new WhereQuerySpecification<T>(predicate);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IOrderedQueryConfigurator<T> OrderBy<T, TProperty>(this IQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector)
            where T : class
        {
            return OrderBy(configurator, keySelector, null, false);
        }

        public static IOrderedQueryConfigurator<T> OrderBy<T, TProperty>(this IQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector, IComparer<TProperty> comparer)
            where T : class
        {
            return OrderBy(configurator, keySelector, comparer, false);
        }

        public static IOrderedQueryConfigurator<T> OrderBy<T, TProperty>(this IQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector, IComparer<TProperty> comparer, bool descending)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            const bool isRoot = true;

            var specification = new OrderByQuerySpecification<T, TProperty>(keySelector, comparer, descending, isRoot);
            configurator.AddSpecification(specification);

            var nextConfigurator = new OrderedQueryConfigurator<T>(configurator);
            return nextConfigurator;
        }

        public static IOrderedQueryConfigurator<T> OrderByDescending<T, TProperty>(this IQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector)
            where T : class
        {
            return OrderBy(configurator, keySelector, null, true);
        }

        public static IOrderedQueryConfigurator<T> OrderByDescending<T, TProperty>(this IQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector, IComparer<TProperty> comparer)
            where T : class
        {
            return OrderBy(configurator, keySelector, comparer, true);
        }

        public static IOrderedQueryConfigurator<T> ThenBy<T, TProperty>(this IOrderedQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector)
            where T : class
        {
            return ThenBy(configurator, keySelector, null, false);
        }

        public static IOrderedQueryConfigurator<T> ThenBy<T, TProperty>(this IOrderedQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector, IComparer<TProperty> comparer)
            where T : class
        {
            return ThenBy(configurator, keySelector, comparer, false);
        }

        public static IOrderedQueryConfigurator<T> ThenBy<T, TProperty>(this IOrderedQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector, IComparer<TProperty> comparer, bool descending)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            const bool isRoot = false;

            var specification = new OrderByQuerySpecification<T, TProperty>(keySelector, comparer, descending, isRoot);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IOrderedQueryConfigurator<T> ThenByDescending<T, TProperty>(this IOrderedQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector)
            where T : class
        {
            return ThenBy(configurator, keySelector, null, true);
        }

        public static IOrderedQueryConfigurator<T> ThenByDescending<T, TProperty>(this IOrderedQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> keySelector, IComparer<TProperty> comparer)
            where T : class
        {
            return ThenBy(configurator, keySelector, comparer, true);
        }

        public static IQueryConfigurator<T> Page<T>(this IQueryConfigurator<T> configurator, int skip, int take)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new PageQuerySpecification<T>(skip, take);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<T> Distinct<T>(this IQueryConfigurator<T> configurator)
            where T : class
        {
            return Distinct(configurator, null);
        }

        public static IQueryConfigurator<T> Distinct<T>(this IQueryConfigurator<T> configurator, IEqualityComparer<T> comparer)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new DistinctQuerySpecification<T>(comparer);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static ITransformConfigurator<TIn, TOut> Select<TIn, TOut>(this IQueryConfigurator<TIn> configurator, Expression<Func<TIn, TOut>> selector)
            where TIn : class
            where TOut : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            var transformSpecification = new SelectQuerySpecification<TIn, TOut>(selector);
            var nextConfigurator = new TransformConfigurator<TIn, TOut>(transformSpecification, configurator);

            return nextConfigurator;
        }

        public static ITransformConfigurator<TIn, TOut> Output<TIn, TOut>(this ITransformConfigurator<TIn, TOut> configurator, Action<IQueryConfigurator<TOut>> configureCallback)
            where TIn : class
            where TOut : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (configureCallback == null)
                throw new ArgumentNullException(nameof(configureCallback));

            configureCallback(configurator.OutputConfigurator);

            return configurator;
        }

    }
}