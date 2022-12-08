using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Utility.Tests
{
    [TestClass]
    public class DebugDisplayTests
    {
        [TestMethod]
        public void GetDebugDisplay()
        {
            var testee = new ClassWithDebugDisplay();
            Assert.AreEqual("You found it",testee.DebuggerDisplayString());

            var testee2 = new ClassWithDebugDisplay2();
            Assert.AreEqual("You found it", testee2.DebuggerDisplayString());

            var testee3 = new ClassWithDebugDisplay3();
            Assert.AreEqual("You found me too", testee3.DebuggerDisplayString());
        }
    }


    internal class ClassWithDebugDisplay
    {
        internal protected virtual string DebuggerDisplay => "You found it";
    }

    internal class ClassWithDebugDisplay2 : ClassWithDebugDisplay
    {

    }

    internal class ClassWithDebugDisplay3 : ClassWithDebugDisplay
    {
        protected internal override string DebuggerDisplay => "You found me too";
    }

}