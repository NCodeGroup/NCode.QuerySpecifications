﻿using NCode.QuerySpecifications.Specifications;

namespace NCode.QuerySpecifications.EntityFrameworkCore
{
    public class AsTrackingQuerySpecification<TEntity> : QuerySpecificationBase<TEntity>
        where TEntity : class
    {
        public override string Name => EntityFrameworkCoreQueryNames.AsTracking;
    }
}