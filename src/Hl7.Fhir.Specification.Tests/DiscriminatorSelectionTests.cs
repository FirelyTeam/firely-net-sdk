using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Hl7.Fhir.FhirPath;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]

    public class DiscriminatorSelectionTests
    {
        [TestMethod]
        public void ParseValidDiscriminatorExpressions()
        {
            var schemas = new ElementSchemaWalker();

            schemas.Walk("active");
            schemas.Walk("active.resolve()");
            schemas.Walk("active.extension('http://somewhere.com')");
            schemas.Walk("active.next.next");
            schemas.Walk("resolve()");
        }

        [TestMethod]
        public void ParseInvalidDiscriminatorExpressions()
        {
            var schemas = new ElementSchemaWalker();

            eval("45");
            eval("active.resolve('bla')");
            eval("active.extension()");
            eval("active.extension(33)");
            eval("active.extension('arg1', 'arg2')");
            eval("active.where(true)");

            void eval(string expr)
            {
                Assert.ThrowsException<DiscriminatorFormatException>(() => schemas.Walk(expr));
            }
        }

    }

}


   