using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Hl7.Fhir.Navigation
{
    class TestValueProvider<T> : IValueProvider<T>
    {
        public TestValueProvider(T value) { Value = value; }
        public Type ValueType { get { return typeof(T); } }
        public object ObjectValue { get { return Value; } }
        public T Value { get; set; }
    }

    static class TestValueProviderExtensions
    {
        public static string Test<T>(this IValueProvider<T> instance) { return "T"; }

        public static string Test(this IValueProvider<string> instance) { return "string"; }

        public static string Test(this IValueProvider<object> instance) { return "object"; }
    }

    [TestClass]
    public class ValueProviderTest
    {
        // Trivial implementation of IValueProvider<T> for testing purposes
        [TestMethod]
        public void TestValueProviderCovariance()
        {
            // Example: IEnumerable covariance
            // IEnumerable<object> objSequence = Enumerable.Empty<string>(); // Legal, class System.String is derived from System.Object
            // IEnumerable<object> objSequence = Enumerable.Empty<int>();    // Illegal, struct System.Int32 is not derived from System.Object

            // Test covariant down cast from IValueProvider<string> to IValueProvider<object>
            const string orgValue = "Foo";
            IValueProvider<string> stringProvider = new TestValueProvider<string>(orgValue);
            IValueProvider<object> objProvider = stringProvider;
            Assert.AreEqual(stringProvider.Value, objProvider.Value);
            Assert.AreEqual(orgValue, objProvider.Value);

            Assert.AreEqual("string", stringProvider.Test());   // Calls the extension method for IValueProvider<string>
            Assert.AreEqual("object", objProvider.Test());      // Calls the extension method for IValueProvider<object>

            // Test covariant interface call - call GetValue<object> on IValueProvider<string>
            var objValue = stringProvider.GetValue<object>();
            Assert.AreEqual(orgValue, objValue);
            Assert.IsTrue(object.ReferenceEquals(objValue, orgValue));
            Assert.AreEqual(objProvider.Value, objValue);

            // Upcast back from IValueProvider<object> to IValueProvider<string> needs explicit (runtime) cast
            // Succeeds because the underlying object actually is an IValueProvider<string>
            stringProvider = (IValueProvider<string>)objProvider;
            Assert.AreEqual(orgValue, stringProvider.Value);

            // Call GetValue<string> on on IValueProvider<object> interface that actually references an IValueProvider<string> instance
            // Runtime upcast to IValueProvider<string> succeeds because the underlying object actually is an IValueProvider<string>
            var stringValue = objProvider.GetValue<string>();
            Assert.AreEqual(orgValue, stringValue);

            // Explicitly initialize an IValueProvider<object> from a string value
            objProvider = new TestValueProvider<object>(orgValue);
            // Runtime upcast to IValueProvider<string> fails because the underlying object doesn't actually support the interface
            stringValue = objProvider.GetValue<string>();
            Assert.IsNull(stringValue);
            // We need to access value via the implemented interface IValueProvider<object> and explicitly cast value back to string
            stringValue = (string)objProvider.GetValue<object>();
            Assert.AreEqual(orgValue, stringValue);

            // Test cast from IValueProvider<string[]> to IValueProvider<IEnumerable<string>>
            // Succeeds because string[] implements (can be assigned to) IEnumerable<string>
            var arrValue = new string[] { "Foo", "Bar" };
            var stringArrayProvider = new TestValueProvider<string[]>(arrValue);
            IValueProvider<IEnumerable<string>> stringSequenceProvider = stringArrayProvider;
            Assert.IsTrue(stringSequenceProvider.Value.SequenceEqual(arrValue));
            Assert.AreEqual(stringSequenceProvider.Value, arrValue);

            Assert.AreEqual("T", stringArrayProvider.Test());   // Calls the extension method for IValueProvider<T>
        }

        [TestMethod]
        public void TestValueProviderGetAs()
        {
            const string orgValue = "Foo";
            var objProvider = new TestValueProvider<object>(orgValue);
            // Runtime upcast to IValueProvider<string> fails because the underlying object doesn't actually support the interface
            var stringValue = objProvider.GetValue<string>();
            Assert.IsNull(stringValue);
            // We need to access value via the implemented interface IValueProvider<object> and explicitly cast value back to string
            stringValue = (string)objProvider.GetValue<object>();
            Assert.AreEqual(orgValue, stringValue);
            // Or use the GetAs function to perform the upcast for us
            stringValue = objProvider.GetValueAs<string>();
            Assert.AreEqual(orgValue, stringValue);
        }

        [TestMethod]
        public void TestValueProviderCastValue()
        {
            const string orgValue = "Foo";
            var objProvider = new TestValueProvider<object>(orgValue);
            var stringValue = (string)objProvider.GetValue<object>();
            Assert.AreEqual(orgValue, stringValue);
            stringValue = objProvider.CastValue<string>();
            Assert.AreEqual(orgValue, stringValue);
        }
    }
}
