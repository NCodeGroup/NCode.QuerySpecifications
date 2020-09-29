using System;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace NCode.QuerySpecifications.TestUtilities
{
	public class InMemoryTestStoreFactory : ITestStoreFactory
	{
		public static InMemoryTestStoreFactory Instance { get; } = new InMemoryTestStoreFactory();

		protected InMemoryTestStoreFactory()
		{
			// nothing
		}

		public TestStore Create(string storeName)
			=> InMemoryTestStore.Create(storeName);

		public TestStore GetOrCreate(string storeName)
			=> InMemoryTestStore.GetOrCreate(storeName);

		public IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
			=> serviceCollection.AddEntityFrameworkInMemoryDatabase().AddSingleton<TestStoreIndex>();

		public ListLoggerFactory CreateListLoggerFactory(Func<string, bool> shouldLogCategory)
			=> new ListLoggerFactory(shouldLogCategory);

	}
}