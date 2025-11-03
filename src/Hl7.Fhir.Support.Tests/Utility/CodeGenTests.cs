using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Hl7.Fhir.Utility.Tests
{
    [TestClass]
    public class CodeGenTests
    {
        [TestMethod]
        public void FactoryTests()
        {
            Func<object> factory = typeof(OperationOutcome).BuildFactoryMethod();
            factory().Should().BeOfType<OperationOutcome>();

            Func<IList> listFactory = typeof(OperationOutcome).BuildListFactoryMethod();
            listFactory().Should().BeOfType<List<OperationOutcome>>();

            // These tests run against a target that support codegen.
            PropertyInfoExtensions.NoCodeGenSupport.Should().BeFalse();
        }

        internal PropertyInfo UrlPropInfo = typeof(Extension).GetProperty("Url");
        internal PropertyInfo ValuePropInfo = typeof(Extension).GetProperty("Value");

        [TestMethod]
        public void GetterSetterNetPrimitive()
        {
            var testee = new Extension();

            var urlSetter = UrlPropInfo.GetValueSetter<Extension>();
            urlSetter(testee, "test");

            var urlGetter = UrlPropInfo.GetValueGetter<Extension>();
            urlGetter(testee).Should().Be("test");

            // These tests run against a target that support codegen.
            PropertyInfoExtensions.NoCodeGenSupport.Should().BeFalse();
        }

        [TestMethod]
        public void GetterSetterNetComplex()
        {
            var testee = new Extension();

            var valueSetter = ValuePropInfo.GetValueSetter<Extension>();
            valueSetter(testee, new FhirDecimal(3.14m));

            var valueGetter = ValuePropInfo.GetValueGetter<Extension>();
            valueGetter(testee).Should().BeOfType<FhirDecimal>().Which.Value.Should().Be(3.14m);

            // These tests run against a target that support codegen.
            PropertyInfoExtensions.NoCodeGenSupport.Should().BeFalse();
        }
    }
}