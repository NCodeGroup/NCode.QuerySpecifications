using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace NCode.QuerySpecifications.TestUtilities
{
    public class InMemoryTestStore : TestStore
    {
        public InMemoryTestStore(string name = null, bool shared = true)
            : base(name, shared)
        {
            // nothing
        }

        public static InMemoryTestStore GetOrCreate(string name)
            => new InMemoryTestStore(name);

        public static InMemoryTestStore GetOrCreateInitialized(string name)
            => new InMemoryTestStore(name).InitializeInMemory(null, (Func<DbContext>)null, null);

        public static InMemoryTestStore Create(string name)
            => new InMemoryTestStore(name, shared: false);

        public static InMemoryTestStore CreateInitialized(string name)
            => new InMemoryTestStore(name, shared: false).InitializeInMemory(null, (Func<DbContext>)null, null);

        public InMemoryTestStore InitializeInMemory(IServiceProvider serviceProvider, Func<DbContext> createContext, Action<DbContext> seed)
            => (InMemoryTestStore)Initialize(serviceProvider, createContext, seed);

        public InMemoryTestStore InitializeInMemory(IServiceProvider serviceProvider, Func<InMemoryTestStore, DbContext> createContext, Action<DbContext> seed)
            => (InMemoryTestStore)Initialize(serviceProvider, () => createContext(this), seed);

        protected override TestStoreIndex GetTestStoreIndex(IServiceProvider serviceProvider)
            => serviceProvider == null
                ? base.GetTestStoreIndex(null)
                : serviceProvider.GetRequiredService<TestStoreIndex>();

        public override DbContextOptionsBuilder AddProviderOptions(DbContextOptionsBuilder builder)
            => builder.UseInMemoryDatabase(Name);

        public override void Clean(DbContext context)
        {
#pragma warning disable EF1001 // Internal EF Core API usage.
            context.GetService<IInMemoryStoreCache>().GetStore(Name).Clear();
#pragma warning restore EF1001 // Internal EF Core API usage.
            context.Database.EnsureCreated();
        }

    }
}