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
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace NCode.QuerySpecifications.TestUtilities
{
	public class InMemoryTestStoreFactory : ITestStoreFactory
	{
        protected InMemoryTestStoreFactory()
		{
			// nothing
		}

        public static InMemoryTestStoreFactory Instance { get; } = new InMemoryTestStoreFactory();

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