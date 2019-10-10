using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace NCode.QuerySpecifications.TestUtilities
{
	public class NorthwindQueryInMemoryFixture<TModelCustomizer> : NorthwindQueryFixtureBase<TModelCustomizer>
		where TModelCustomizer : IModelCustomizer, new()
	{
		protected override ITestStoreFactory TestStoreFactory => InMemoryTestStoreFactory.Instance;
	}
}