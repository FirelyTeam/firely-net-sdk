using FluentAssertions;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Support.Poco.Tests
{

    [TestClass]
    public class PathStackTests
    {
        [TestMethod]
        public void TestPathNavigation()
        {
            var ps = new PathStack();

            ps.EnterResource("A"); t("A");

            ps.EnterElement("b", 0, false); t("A.b");
            ps.EnterElement("c", 0, false); t("A.b.c");
            ps.EnterResource("D"); t("D");
            ps.EnterElement("e", 0, false); t("D.e");

            Assert.ThrowsException<InvalidOperationException>(() => ps.ExitResource());
            ps.ExitElement(); t("D");

            Assert.ThrowsException<InvalidOperationException>(() => ps.ExitElement());
            ps.ExitResource(); t("A.b.c");

            ps.ExitElement(); t("A.b");
            ps.ExitElement(); t("A");

            Assert.ThrowsException<InvalidOperationException>(() => ps.ExitElement());
            ps.ExitResource(); t("$this");

            void t(string e) => ps.GetPath().Should().Be(e);
        }

        [TestMethod]
        public void TestPathStackContainedResources()
        {
            var ps = new PathStack();

            ps.EnterResource("Patient"); tip("Patient"); tdp("Patient");

            ps.EnterElement("contained", 0, false); tip("Patient.contained[0]"); tdp("Patient.contained");
            ps.EnterResource("RelatedPerson"); tip("Patient.contained[0]"); tdp("RelatedPerson");
            ps.EnterElement("id", null, false); tip("Patient.contained[0].id"); tdp("RelatedPerson.id");
            ps.ExitElement(); tip("Patient.contained[0]"); tdp("RelatedPerson");
            ps.ExitResource(); tip("Patient.contained[0]"); tdp("Patient.contained");
            ps.ExitElement(); tip("Patient"); tdp("Patient");

            void tdp(string e) => ps.GetPath().Should().Be(e);
            void tip(string e) => ps.GetInstancePath().Should().Be(e);
        }

        [TestMethod]
        public void TestPathStackPrimitiveProperties()
        {
            var ps = new PathStack();

            ps.EnterResource("Patient"); tip("Patient"); tdp("Patient");
            ps.EnterElement("id", null, false); tip("Patient.id"); tdp("Patient.id");
            ps.EnterElement("value", null, true); tip("Patient.id"); tdp("Patient.id.value");
            ps.ExitElement(); tip("Patient.id"); tdp("Patient.id");
            ps.ExitElement(); tip("Patient"); tdp("Patient");

            void tdp(string e) => ps.GetPath().Should().Be(e);
            void tip(string e) => ps.GetInstancePath().Should().Be(e);
        }

        [TestMethod]
        public void TestPathStackArrays2()
        {
            var ps = new PathStack();

            ps.EnterResource("Patient"); tip("Patient"); tdp("Patient");

            ps.EnterElement("contained", 0, false); tip("Patient.contained[0]"); tdp("Patient.contained");
            ps.EnterResource("RelatedPerson"); tip("Patient.contained[0]"); tdp("RelatedPerson");
            ps.EnterElement("id", null, false); tip("Patient.contained[0].id"); tdp("RelatedPerson.id");
            ps.ExitElement(); tip("Patient.contained[0]"); tdp("RelatedPerson");
            ps.ExitResource(); tip("Patient.contained[0]"); tdp("Patient.contained");

            ps.IncrementIndex(); tip("Patient.contained[1]"); tdp("Patient.contained");
            ps.EnterResource("RelatedPerson"); tip("Patient.contained[1]"); tdp("RelatedPerson");
            ps.EnterElement("id", null, false); tip("Patient.contained[1].id"); tdp("RelatedPerson.id");
            ps.ExitElement(); tip("Patient.contained[1]"); tdp("RelatedPerson");
            ps.ExitResource(); tip("Patient.contained[1]"); tdp("Patient.contained");

            ps.ExitElement(); tip("Patient"); tdp("Patient");

            ps.EnterElement("name", 0, false); tip("Patient.name[0]");
            ps.EnterElement("family", null, false); tip("Patient.name[0].family");
            ps.EnterElement("value", null, true); tip("Patient.name[0].family"); tdp("Patient.name.family.value");
            ps.ExitElement(); tip("Patient.name[0].family");
            ps.ExitElement(); tip("Patient.name[0]");

            ps.EnterElement("given", 0, false); tip("Patient.name[0].given[0]");
            ps.IncrementIndex(); tip("Patient.name[0].given[1]"); tdp("Patient.name.given");
            ps.IncrementIndex(); tip("Patient.name[0].given[2]"); tdp("Patient.name.given");
            ps.ExitElement(); tip("Patient.name[0]");

            ps.EnterElement("given", 0, false); tip("Patient.name[0].given[0]");
            ps.ExitElement(); tip("Patient.name[0]");
            ps.IncrementIndex(); tip("Patient.name[1]");

            ps.EnterElement("family", null, false); tip("Patient.name[1].family");
            ps.ExitElement(); tip("Patient.name[1]");

            ps.ExitElement(); tip("Patient");

            void tdp(string e) => ps.GetPath().Should().Be(e);
            void tip(string e) => ps.GetInstancePath().Should().Be(e);
        }
    }

}
