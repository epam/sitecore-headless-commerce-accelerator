using System.Collections;
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

        public static void IsEmpty(IEnumerable collection, string objName)
        {
            Assert.That(collection, new EmptyCollectionConstraint(), $"{objName} must be empty.");
        }

        public static void IsNotEmpty(IEnumerable collection, string objName)
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

        public static void NotNullOrWhiteSpace(string text, string objName)
        {
            Assert.That(!string.IsNullOrWhiteSpace(text), $"{objName} can't be null or white space.");
        }
    }
}
