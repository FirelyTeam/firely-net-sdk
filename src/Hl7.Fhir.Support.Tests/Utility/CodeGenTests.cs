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
    }
}