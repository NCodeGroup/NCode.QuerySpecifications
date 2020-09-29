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
using NCode.QuerySpecifications.EntityFrameworkCore.Configurators;
using NCode.QuerySpecifications.EntityFrameworkCore.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore
{
    public static class QueryConfiguratorExtensions
    {
        public static IQueryConfigurator<TEntity> AsNoTracking<TEntity>(this IQueryConfigurator<TEntity> configurator)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new AsNoTrackingQuerySpecification<TEntity>();
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<TEntity> AsTracking<TEntity>(this IQueryConfigurator<TEntity> configurator)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new AsTrackingQuerySpecification<TEntity>();
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<TEntity> IgnoreQueryFilters<TEntity>(this IQueryConfigurator<TEntity> configurator)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new IgnoreQueryFiltersQuerySpecification<TEntity>();
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<TEntity> TagWith<TEntity>(this IQueryConfigurator<TEntity> configurator, string tag)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            var specification = new TagWithQuerySpecification<TEntity>(tag);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IQueryConfigurator<TEntity> Include<TEntity>(this IQueryConfigurator<TEntity> configurator, string navigationPropertyPath)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePathQuerySpecification<TEntity>(navigationPropertyPath);
            configurator.AddSpecification(specification);

            return configurator;
        }

        public static IIncludableQueryConfigurator<TEntity, TProperty> Include<TEntity, TProperty>(this IQueryConfigurator<TEntity> configurator, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePropertyQuerySpecification<TEntity, TEntity, TProperty>(navigationPropertyPath, true, false);
            configurator.AddSpecification(specification);

            var nextConfigurator = new IncludableQueryConfigurator<TEntity, TProperty>(configurator);
            return nextConfigurator;
        }

        public static IIncludableQueryConfigurator<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludableQueryConfigurator<TEntity, TPreviousProperty> configurator, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TEntity : class
            where TPreviousProperty : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePropertyQuerySpecification<TEntity, TPreviousProperty, TProperty>(navigationPropertyPath, false, false);
            configurator.AddSpecification(specification);

            var nextConfigurator = new IncludableQueryConfigurator<TEntity, TProperty>(configurator);
            return nextConfigurator;
        }

        public static IIncludableQueryConfigurator<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IIncludableQueryConfigurator<TEntity, IEnumerable<TPreviousProperty>> configurator, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TEntity : class
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));
            if (navigationPropertyPath == null)
                throw new ArgumentNullException(nameof(navigationPropertyPath));

            var specification = new IncludePropertyQuerySpecification<TEntity, TPreviousProperty, TProperty>(navigationPropertyPath, false, true);
            configurator.AddSpecification(specification);

            var nextConfigurator = new IncludableQueryConfigurator<TEntity, TProperty>(configurator);
            return nextConfigurator;
        }
    }
}