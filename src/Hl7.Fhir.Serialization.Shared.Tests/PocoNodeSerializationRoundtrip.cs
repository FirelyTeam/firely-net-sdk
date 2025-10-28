using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests;

[TestClass]
public class PocoNodeSerializationRoundtrip
{
    [TestMethod]
    public void CanConvertCircularPocoNode()
    {
        var originalJson = @"{""resourceType"":""Patient"",""deceasedBoolean"":true}";
        var sn = FhirJsonNode.Parse(originalJson);
        // build TypedElement with correct type info
        var typed = sn.ToTypedElement(ModelInfo.ModelInspector);
        // then use base when building PocoNode - no information about Patient
        var pnOnTypedElementBase = typed.ToPocoNode(ModelInspector.Base);
        var pnOnTypedElement = typed.ToPocoNode();
        // check SourceNode version too, but if SourceNode has no information
        // it would serialize everything as strings, so we just check relevant version.
        var pnOnSourceNode = sn.ToPoco().ToPocoNode();

        var jsonTypedBase = pnOnTypedElementBase.ToJson();
        var jsonTyped = pnOnTypedElement.ToJson();
        var jsonSourceBase = pnOnSourceNode.ToJson();
        Assert.AreEqual(jsonTyped, jsonSourceBase);
        Assert.AreEqual(jsonTyped, jsonTypedBase);
    }
}
