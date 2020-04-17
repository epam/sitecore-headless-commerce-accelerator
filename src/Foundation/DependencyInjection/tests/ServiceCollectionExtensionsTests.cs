//    Copyright 2020 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace HCA.Foundation.DependencyInjection.Tests
{
    using Microsoft.Extensions.DependencyInjection;

    using NSubstitute;

    using Xunit;

    public class ServiceCollectionExtensionsTests
    {
        [Theory]
        [InlineData("*AnotherSimpleTestClass")]
        [InlineData("*Another*")]
        [InlineData("HCA.*Another*")]
        public void AddByWildcard_ValidTypes_ServicesCollectionWithMatchingType(string pattern)
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            var simpleTestClassType = typeof(AddByWildcardTestClasses.SimpleTestClass);
            var anotherSimpleTestClassType = typeof(AddByWildcardTestClasses.AnotherSimpleTestClass);

            var assembly = Substitute.For<FakeAssembly>();
            assembly.ExportedTypes.Returns(new[] { simpleTestClassType, anotherSimpleTestClassType });
            assembly.GetExportedTypes().Returns(x => assembly.ExportedTypes);

            serviceCollection.AddByWildcard(Lifetime.Singleton, pattern, assembly);

            Assert.Equal(1, serviceCollection.Count);

            Assert.Contains(
                serviceCollection,
                x => x.ImplementationType == anotherSimpleTestClassType && x.Lifetime == ServiceLifetime.Singleton);
            Assert.Contains(
                serviceCollection,
                x => x.ServiceType == anotherSimpleTestClassType && x.Lifetime == ServiceLifetime.Singleton);
        }

        private static class AddByWildcardTestClasses
        {
            public class AnotherSimpleTestClass
            {
            }

            public class SimpleTestClass
            {
            }
        }

        private static class AddClassesWithServiceAttributeTestClasses
        {
            [Service(typeof(InterfaceClass), Lifetime = Lifetime.Singleton)]
            public class ImplementationClass : InterfaceClass
            {
            }

            public class InterfaceClass
            {
            }

            [Service(Lifetime = Lifetime.Transient)]
            public class SelfRegistratingClass
            {
            }
        }

        [Fact]
        public void AddClassesWithServiceAttribute_TypesWithServiceRegistration_ServicesCollectionIsConfigured()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            var interfaceType = typeof(AddClassesWithServiceAttributeTestClasses.InterfaceClass);
            var implementationType = typeof(AddClassesWithServiceAttributeTestClasses.ImplementationClass);

            var selfRegistratingType = typeof(AddClassesWithServiceAttributeTestClasses.SelfRegistratingClass);

            var assembly = Substitute.For<FakeAssembly>();
            assembly.ExportedTypes.Returns(new[] { interfaceType, implementationType, selfRegistratingType });
            assembly.GetExportedTypes().Returns(x => assembly.ExportedTypes);

            serviceCollection.AddClassesWithServiceAttribute(assembly);

            Assert.Equal(2, serviceCollection.Count);

            Assert.Contains(
                serviceCollection,
                x => x.ImplementationType == implementationType && x.Lifetime == ServiceLifetime.Singleton);
            Assert.Contains(
                serviceCollection,
                x => x.ServiceType == interfaceType && x.Lifetime == ServiceLifetime.Singleton);

            Assert.Contains(
                serviceCollection,
                x => x.ImplementationType == selfRegistratingType && x.Lifetime == ServiceLifetime.Transient);
            Assert.Contains(
                serviceCollection,
                x => x.ServiceType == selfRegistratingType && x.Lifetime == ServiceLifetime.Transient);
        }
    }
}