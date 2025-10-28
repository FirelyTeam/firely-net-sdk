/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.Fhir.Tests.Model;

[TestClass]
public class PocoNodeTests
{
    [TestMethod]
    public void PocoNode_ImplementationBasedLocations_ReturnCorrectLocations()
    {
        var ext = new Extension() { Url = "http://example.org/fhir/test", Value = new ResourceReference("Patient/john-doe") };
        var pn = ext.ToPocoNode(ModelInfo.ModelInspector);
        var navigate = pn.NavigateTo("value.reference").First();
        string typedElementLocation = ((ITypedElement)navigate).Location;
        string sourceNodeLocation = ((ISourceNode)navigate).Location;
        
        typedElementLocation.Should().Be("Extension.value[0].reference[0]");
        sourceNodeLocation.Should().Be("Extension.valueReference[0].reference[0]");
    }
}