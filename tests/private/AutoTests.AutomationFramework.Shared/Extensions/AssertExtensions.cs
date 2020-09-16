using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace AutoTests.AutomationFramework.Shared.Extensions
{
    public class ExtendedAssert
    {
        public static void AreEqual(object expected, object actual, string objName)
        {
            Assert.That(actual, Is.EqualTo(expected), $"The current {objName} does not correspond to the expected.");
        }

        public static void Empty(IEnumerable collection, string objName)
        {
            Assert.That(collection, new EmptyCollectionConstraint(), $"{objName} must be empty.");
        }

        public static void NotEmpty(IEnumerable collection, string objName)
        {
            Assert.That(collection, Is.Not.Empty, $"{objName} can't be empty.");
        }

        public static void Null(object anObject, string objName)
        {
            Assert.That(anObject, Is.Null, $"{objName} must be null.");
        }

        public static void NotNull(object anObject, string objName)
        {
            Assert.That(anObject, Is.Not.Null, $"{objName} can't be null.");
        }

        public static void NotNullOrEmpty<T>(IEnumerable<T> anObjects, string objsName)
        {
            Assert.That(anObjects != null && anObjects.Any(), $"{objsName} can't be null or empty.");
        }

        public static void NullOrEmpty<T>(IEnumerable<T> anObjects, string objsName)
        {
            Assert.That(anObjects == null || !anObjects.Any(), $"{objsName} should be null or empty.");
        }

        public static void NotNullOrWhiteSpace(string text, string objName)
        {
            Assert.That(!string.IsNullOrWhiteSpace(text), $"{objName} can't be null or white space.");
        }

        public static void NullOrWhiteSpace(string text, string objName)
        {
            Assert.That(string.IsNullOrWhiteSpace(text), $"{objName} should be null or white space.");
        }

        public static void InRange<T>(T value, T from, T to, string objName)
        {
            Assert.That(value, Is.InRange(from, to), $"{objName} should be null or white space.");
        }

        public static void LessOrEqual<T>(T value1, T value2, string objName)
        where T : IComparable
        {
            Assert.LessOrEqual(value1, value2, $"{objName}={value1} should be less or equal {value2}.");
        }
    }
}