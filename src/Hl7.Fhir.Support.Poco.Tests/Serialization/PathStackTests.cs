using FluentAssertions;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Support.Poco.Tests;

[TestClass]
public class PathStackTests
{

    [TestMethod]
    public void TestPathNavigation()
    {
        var ps = new PocoDeserializerState();

        ps.EnterResource("A");
        t("A");

        ps.EnterElement("b");
        t("A.b");
        ps.EnterElement("c", true);
        t("A.b.c[0]");
        ps.EnterResource("D");
        t("A.b.c[0]");
        ps.EnterElement("e", true);
        t("A.b.c[0].e[0]");

        Assert.Throws<InvalidOperationException>(() => ps.ExitResource());
        ps.ExitElement();
        t("A.b.c[0]");

        Assert.Throws<InvalidOperationException>(() => ps.ExitElement());
        ps.ExitResource();
        t("A.b.c[0]");

        ps.ExitElement();
        t("A.b");
        ps.ExitElement();
        t("A");

        Assert.Throws<InvalidOperationException>(() => ps.ExitElement());
        ps.ExitResource();
        t("$this");

        void t(string e) => ps.GetInstancePath().Should().Be(e);
    }

    [TestMethod]
    public void TestPathStackContainedResources()
    {
        var ps = new PocoDeserializerState();

        ps.EnterResource("Patient");
        tip("Patient");

        ps.EnterElement("contained", true);
        tip("Patient.contained[0]");
        ps.EnterResource("RelatedPerson");
        tip("Patient.contained[0]");
        ps.EnterElement("id");
        tip("Patient.contained[0].id");
        ps.ExitElement();
        tip("Patient.contained[0]");
        ps.ExitResource();
        tip("Patient.contained[0]");
        ps.ExitElement();
        tip("Patient");

        void tip(string e) => ps.GetInstancePath().Should().Be(e);
    }

    [TestMethod]
    public void TestPathStackPrimitiveProperties()
    {
        var ps = new PocoDeserializerState();

        ps.EnterResource("Patient");
        tip("Patient");
        ps.EnterElement("id");
        tip("Patient.id");
        ps.ExitElement();
        tip("Patient");

        void tip(string e) => ps.GetInstancePath().Should().Be(e);
    }

    [TestMethod]
    public void TestPathStackArrays2()
    {
        var ps = new PocoDeserializerState();

        ps.EnterResource("Patient");
        tip("Patient");
        tdp("Patient");

        ps.EnterElement("contained", true);
        tip("Patient.contained[0]");
        tdp("Patient.contained");
        ps.EnterResource("RelatedPerson");
        tip("Patient.contained[0]");
        tdp("RelatedPerson");
        ps.EnterElement("id");
        tip("Patient.contained[0].id");
        tdp("RelatedPerson.id");
        ps.ExitElement();
        tip("Patient.contained[0]");
        tdp("RelatedPerson");
        ps.ExitResource();
        tip("Patient.contained[0]");
        tdp("Patient.contained");

        ps.SetIndex(1);
        tip("Patient.contained[1]");
        tdp("Patient.contained");
        ps.EnterResource("RelatedPerson");
        tip("Patient.contained[1]");
        tdp("RelatedPerson");
        ps.EnterElement("id");
        tip("Patient.contained[1].id");
        tdp("RelatedPerson.id");
        ps.ExitElement();
        tip("Patient.contained[1]");
        tdp("RelatedPerson");
        ps.ExitResource();
        tip("Patient.contained[1]");
        tdp("Patient.contained");

        ps.ExitElement();
        tip("Patient");
        tdp("Patient");

        ps.EnterElement("name", true);
        tip("Patient.name[0]");
        ps.EnterElement("family");
        tip("Patient.name[0].family");
        ps.ExitElement();
        tip("Patient.name[0]");

        ps.EnterElement("given", true);
        tip("Patient.name[0].given[0]");
        ps.SetIndex(1);
        tip("Patient.name[0].given[1]");
        tdp("Patient.name.given");
        ps.SetIndex(2);
        tip("Patient.name[0].given[2]");
        tdp("Patient.name.given");
        ps.ExitElement();
        tip("Patient.name[0]");

        ps.EnterElement("given", true);
        tip("Patient.name[0].given[0]");
        ps.ExitElement();
        tip("Patient.name[0]");
        ps.SetIndex(1);
        tip("Patient.name[1]");

        ps.EnterElement("family");
        tip("Patient.name[1].family");
        ps.ExitElement();
        tip("Patient.name[1]");

        ps.ExitElement();
        tip("Patient");

        static void tdp(string e) { }
        void tip(string e) => ps.GetInstancePath().Should().Be(e);
    }
}