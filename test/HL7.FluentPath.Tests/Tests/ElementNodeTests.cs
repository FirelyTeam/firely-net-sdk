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
        [Fact]
        public void TestPath()
        {
            var data = ElementNode.Valued("active", true, FHIRDefinedType.Boolean.GetLiteral(),
                   ElementNode.Valued("id", "myId1"),
                   ElementNode.Valued("id", "myId2"),
                   ElementNode.Node("extension",
                       ElementNode.Valued("value", 4, "integer")),
                   ElementNode.Node("extension",
                       ElementNode.Valued("value", "world!", "string"))).ToNavigator();

            //var x = data.Children()[0];
        }
    }
}