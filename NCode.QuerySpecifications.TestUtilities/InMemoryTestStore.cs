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