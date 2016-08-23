/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
//extern alias dstu2;

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.ElementModel;
using Hl7.FluentPath;
using Hl7.FluentPath.Expressions;
using Xunit;
using Hl7.Fhir.Model;
using Hl7.Fhir.Introspection;

namespace Hl7.FluentPath.Tests
{
    public class ElementNodeTests
    {
        ElementNode patient;

        public ElementNodeTests()
        {
            patient = ElementNode.Node("Patient", "Patient",                
                ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral(),
                   ElementNode.Valued("id", "myId1"),
                   ElementNode.Valued("id", "myId2"),
                   ElementNode.Node("extension",
                       ElementNode.Valued("value", 4, "integer")),
                   ElementNode.Node("extension",
                       ElementNode.Valued("value", "world!", "string"))));
        }

        [Fact]
        public void TestConstruction()
        {
            var data = patient.Children[0];
            Assert.Equal("active", data.Name);
            Assert.Equal(true, data.Value);
            Assert.Equal("boolean", data.TypeName);
            Assert.Equal(4, data.Children.Count);
        }


        [Fact]
        public void TestPath()
        {
            Assert.Equal("Patient", patient.Path);
            Assert.Equal("Patient.active[0]", patient.Children[0].Path);
            Assert.Equal("Patient.active[0].id[0]", patient.Children[0].Children[0].Path);
            Assert.Equal("Patient.active[0].id[1]", patient.Children[0].Children[1].Path);
            Assert.Equal("Patient.active[0].extension[0].value[0]", patient.Children[0].Children[2].Children[0].Path);
            Assert.Equal("Patient.active[0].extension[1].value[0]", patient.Children[0].Children[3].Children[0].Path);
        }

        [Fact]
        public void TestNavigation()
        {
            var nav = patient.ToNavigator();

            Assert.Equal(nav.Name, "Patient");
            Assert.True(nav.MoveToFirstChild());
            Assert.Equal(nav.Name, "active");
            Assert.Equal("boolean", nav.TypeName);
            Assert.False(nav.MoveToNext());

            Assert.Equal(true, nav.Value);
            Assert.True(nav.MoveToFirstChild());
            Assert.Equal(nav.Name, "id");
            Assert.False(nav.MoveToFirstChild());
            Assert.True(nav.MoveToNext());
            Assert.Equal(nav.Name, "id");
            Assert.True(nav.MoveToNext());
            Assert.Equal(nav.Name, "extension");
            Assert.True(nav.MoveToFirstChild());
            Assert.Equal(nav.Name, "value");
        }
    }
}