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
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NCode.QuerySpecifications.Configurators;
using NCode.QuerySpecifications.EntityFrameworkCore.Configurators;
using NCode.QuerySpecifications.EntityFrameworkCore.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore
{
    public static class ConfiguratorExtensions
    {
        public static IQueryConfigurator<T> AsNoTracking<T>(this IQueryConfigurator<T> configurator)
            where T : class
        {
            return AsTracking(configurator, QueryTrackingBehavior.NoTracking);
        }

        public static IQueryConfigurator<T> AsTracking<T>(this IQueryConfigurator<T> configurator)
            where T : class
        {
            return AsTracking(configurator, QueryTrackingBehavior.TrackAll);
        }

        public static IQueryConfigurator<T> AsTracking<T>(this IQueryConfigurator<T> configurator, QueryTrackingBehavior queryTrackingBehavior)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new TrackingQuerySpecification<T>(queryTrackingBehavior);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<T> IgnoreQueryFilters<T>(this IQueryConfigurator<T> configurator)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new IgnoreQueryFiltersQuerySpecification<T>();
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<T> TagWith<T>(this IQueryConfigurator<T> configurator, string tag)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            var specification = new TagWithQuerySpecification<T>(tag);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<T> Include<T>(this IQueryConfigurator<T> configurator, string navigationPropertyPath)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePathQuerySpecification<T>(navigationPropertyPath);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IIncludableQueryConfigurator<T> Include<T, TProperty>(this IQueryConfigurator<T> configurator, Expression<Func<T, TProperty>> navigationPropertyPath)
            where T : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePropertyRootQuerySpecification<T, TProperty>(navigationPropertyPath);
            configurator.AddSpecification(specification);

            var nextConfigurator = new IncludableQueryConfigurator<T>(configurator);
            return nextConfigurator;
        }

        public static IIncludableQueryConfigurator<T> ThenInclude<T, TInputProperty, TOutputProperty>(this IIncludableQueryConfigurator<T> configurator, Expression<Func<TInputProperty, TOutputProperty>> navigationPropertyPath)
            where T : class
            where TInputProperty : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePropertyThenQuerySpecification<T, TInputProperty, TOutputProperty>(navigationPropertyPath);
            configurator.AddSpecification(specification);

            return configurator;
        }

    }
}