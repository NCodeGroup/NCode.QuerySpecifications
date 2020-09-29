using System;
using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Specifications
{
    public interface IIncludePathQuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        string NavigationPropertyPath { get; }
    }

    public class IncludePathQuerySpecification<TEntity> : IIncludePathQuerySpecification<TEntity>
        where TEntity : class
    {
        public string Name => EntityFrameworkCoreQueryNames.Include;

        public string NavigationPropertyPath { get; }

        public IncludePathQuerySpecification(string navigationPropertyPath)
        {
            NavigationPropertyPath = navigationPropertyPath ?? throw new ArgumentNullException(nameof(navigationPropertyPath));
        }

    }
}