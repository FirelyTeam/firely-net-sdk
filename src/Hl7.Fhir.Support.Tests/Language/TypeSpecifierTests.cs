using Hl7.Fhir.Language;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Support.Tests.Language
{
    [TestClass]
    public class TypeSpecifierTests
    {
        [TestMethod]
        public void TestConstruction()
        {
            var sysT = TypeSpecifier.GetByName("Concept");
            Assert.AreEqual("System", sysT.Namespace);
            Assert.AreEqual("Concept", sysT.Name);
            Assert.IsTrue(Object.ReferenceEquals(sysT, TypeSpecifier.Concept));

            sysT = TypeSpecifier.GetByName("AsYetUnknown");
            Assert.AreEqual("System", sysT.Namespace);
            Assert.AreEqual("AsYetUnknown", sysT.Name);

            var cusT = TypeSpecifier.GetByName("MySpace", "MyType");
            Assert.AreEqual("MySpace", cusT.Namespace);
            Assert.AreEqual("MyType", cusT.Name);
        }

        [TestMethod]
        public void TestEquality()
        {
            Assert.AreEqual(TypeSpecifier.Concept, TypeSpecifier.Concept);
            Assert.AreNotEqual(TypeSpecifier.Concept, TypeSpecifier.Code);
#pragma warning disable CS1718 // Comparison made to same variable - we're testing the '==' operator here
            Assert.IsTrue(TypeSpecifier.Concept == TypeSpecifier.Concept);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.IsTrue(TypeSpecifier.Concept != TypeSpecifier.Code);
            Assert.AreEqual(TypeSpecifier.Concept, TypeSpecifier.GetByName("Concept"));
            Assert.AreEqual(TypeSpecifier.Concept, TypeSpecifier.GetByName("System","Concept"));

            Assert.AreNotEqual(TypeSpecifier.Concept, TypeSpecifier.GetByName("System", "concept"));
            Assert.AreNotEqual(TypeSpecifier.GetByName("System", "Concept"), TypeSpecifier.GetByName("System", "concept"));
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("System.Concept", TypeSpecifier.Concept.ToString());
            Assert.AreEqual("`dot.net`.String", TypeSpecifier.GetByName("dot.net", "String").ToString());
            Assert.AreEqual("DotNet.`System.Guid`", TypeSpecifier.GetByName("DotNet", "System.Guid").ToString());
            Assert.AreEqual(@"`dot.\`net\``.String", TypeSpecifier.GetByName("dot.`net`", "String").ToString());
            Assert.AreEqual(@"`dot\`net`.String", TypeSpecifier.GetByName("dot`net", "String").ToString());
        }

        [TestMethod]
        public void TestForNativeType()
        {
            Assert.AreEqual(TypeSpecifier.Boolean,TypeSpecifier.ForNativeType(typeof(bool)));
            Assert.AreEqual(TypeSpecifier.DateTime, TypeSpecifier.ForNativeType(typeof(P.DateTime)));
            Assert.AreEqual(TypeSpecifier.Concept, TypeSpecifier.ForNativeType(typeof(P.Concept)));
            Assert.AreEqual(TypeSpecifier.Any, TypeSpecifier.ForNativeType(typeof(object)));
            Assert.AreEqual(TypeSpecifier.GetByName("DotNet", "System.Guid"), TypeSpecifier.ForNativeType(typeof(Guid)));
            Assert.AreEqual(TypeSpecifier.GetByName("DotNet", "System.Collections.Generic.IEnumerable`1[System.Guid]"), 
                TypeSpecifier.ForNativeType(typeof(IEnumerable<Guid>)));
        }

    }
}
