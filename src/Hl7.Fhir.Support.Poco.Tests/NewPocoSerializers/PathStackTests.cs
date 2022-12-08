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

            ps.EnterElement("b"); t("A.b");
            ps.EnterElement("c"); t("A.b.c");
            ps.EnterResource("D"); t("D");
            ps.EnterElement("e"); t("D.e");

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

    }

}
