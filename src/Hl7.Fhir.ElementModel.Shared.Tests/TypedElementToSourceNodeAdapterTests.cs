using FluentAssertions;
using Hl7.Fhir.ElementModel.Adapters;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class TypedElementToSourceNodeAdapterTests
    {
        /// <summary>
        /// This test proves issue https://github.com/FirelyTeam/firely-net-sdk/issues/888
        /// </summary>
        [TestMethod]
        public void AnnotationsTest()
        {
            var patient = new Patient() { Active = true };
            var typedElement = patient.ToTypedElementLegacy();
            var sourceNode = typedElement.ToSourceNode();

            var result = sourceNode.Annotation<ISourceNode>();
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(TypedElementToSourceNodeAdapter), result.GetType());
            Assert.AreSame(sourceNode, result);

            var result2 = sourceNode.Annotation<IResourceTypeSupplier>();
            Assert.IsNotNull(result2);
            Assert.AreEqual("TypedElementToSourceNodeAdapter", result2.GetType().Name); // I use the classname here, because PocoElementNode is internal in Hl7.Fhir.Core
            Assert.AreSame(sourceNode, result2 as ISourceNode);
        }

        [TestMethod]
        public async Tasks.Task AnnotationsFromParsingTest()
        {
            var _sdsProvider = new PocoStructureDefinitionSummaryProvider();
            var patientJson = "{\"resourceType\":\"Patient\", \"active\":\"true\"}";
            var patient = await FhirJsonNode.ParseAsync(patientJson);
            var typedPatient = patient.ToTypedElement(_sdsProvider, "Patient");
            var sourceNode = typedPatient.ToSourceNode();

            var result = patient.Annotation<ISourceNode>();
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(FhirJsonNode), result.GetType(), "ISourceNode is provided by FhirJsonNode");
            Assert.AreSame(patient, result);

            var result2 = sourceNode.Annotation<ISourceNode>();
            Assert.IsNotNull(result2);
            Assert.AreEqual(typeof(TypedElementToSourceNodeAdapter), result2.GetType(), "Now ISourceNode is provided by TypedElementToSourceNodeAdapter");
            Assert.AreSame(sourceNode, result2);

            var result3 = sourceNode.Annotation<IResourceTypeSupplier>();
            Assert.IsNotNull(result3);
            Assert.AreEqual(typeof(TypedElementToSourceNodeAdapter), result3.GetType());
        }

        [TestMethod]
        public async Tasks.Task SourceNodeFromElementNodeReturnsResourceTypeSupplier()
        {
            var _sdsProvider = new PocoStructureDefinitionSummaryProvider();
            var patientJson = "{\"resourceType\":\"Patient\", \"active\":\"true\"}";
            var patientNode = await FhirJsonNode.ParseAsync(patientJson);
            var typedPatient = patientNode.ToTypedElement(_sdsProvider, "Patient");

            var elementNode = ElementNode.FromElement(typedPatient);
            var adapter = elementNode.ToSourceNode();

            Assert.AreEqual(typeof(TypedElementToSourceNodeAdapter), adapter.GetType(), "ISourceNode is provided by TypedElementToSourceNodeAdapter");

            var result = adapter.Annotation<IResourceTypeSupplier>();
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(TypedElementToSourceNodeAdapter), result.GetType());
            Assert.AreEqual("Patient", adapter.GetResourceTypeIndicator());
            Assert.AreSame(adapter, result as ISourceNode);
        }
        
        [TestMethod]
        public void SourceNodeToTypedElementToSourceNode_WithChoiceType_RoundTrips_Location()
        {
            var sourceNode = SourceNode.Resource("Patient", "Patient", 
                SourceNode.Node("extension",
                SourceNode.Valued("url", "http://hl7.org/fhir/StructureDefinition/patient-birthTime"),
                SourceNode.Valued("valueDateTime", "2021-01-01T00:00:00Z")));
            var extensionValueSourceLocation = 
                sourceNode.Children("extension").First().Children("valueDateTime").First().Location;

            var sdsProvider = new PocoStructureDefinitionSummaryProvider();
            var typedElement = sourceNode.ToTypedElement(sdsProvider);

            var result = typedElement.ToSourceNode();
            result.Children("extension").First().Children("valueDateTime").First().Location.Should()
                .Be(extensionValueSourceLocation, 
                    "On a SourceNode from a TypedElement, a choice type should again have the same Location as the original SourceNode");
        }

        [TestMethod]
        public void PocoToSourceNode_WithChoiceType_HasSameLocationAsSourceNode()
        {
            var sourceNode = SourceNode.Resource("Patient", "Patient",
                SourceNode.Node("extension",
                    SourceNode.Valued("url", "http://hl7.org/fhir/StructureDefinition/patient-birthTime"),
                    SourceNode.Valued("valueDateTime", "2021-01-01T00:00:00Z")));
            var extensionValueSourceLocation =
                sourceNode.Children("extension").First().Children("valueDateTime").First().Location;

            var poco = new Patient();
            poco.Extension.Add(new Extension("http://hl7.org/fhir/StructureDefinition/patient-birthTime", new FhirDateTime("2021-01-01T00:00:00Z")));

            var result = poco.ToSourceNode(ModelInfo.ModelInspector);
            result.Children("extension").First().Children("valueDateTime").First().Location.Should()
                .Be(extensionValueSourceLocation,
                    "On a SourceNode from a TypedElement, a choice type should again have the same Location as if it was constructed as SourceNode");
        }
        
        [TestMethod]
        public void UnknownResource_ContainsPatient_CanRetrievePoco()
        {
            var sourceNode = 
                SourceNode.Resource("Test", "Unknown", 
                SourceNode.Resource("resource", "Patient",
                SourceNode.Valued("active", "true"),
                SourceNode.Node("extension",
                SourceNode.Valued("url", "http://hl7.org/fhir/StructureDefinition/patient-birthTime"),
                SourceNode.Valued("valueDateTime", "2021-01-01T00:00:00Z"))));

            var sourcePoco = sourceNode.Children("resource").First().ToPoco<Resource>();
            var typedElement = sourceNode.ToTypedElement(ModelInfo.ModelInspector);
            ISourceNode fhirPath = typedElement.Select("resource").First().ToPocoNode();
            var patient = fhirPath.ToPoco<Patient>();
            patient.Should().NotBeNull();
            patient.Active.Should().BeTrue();
        }
        
        [TestMethod]
        public void DynamicDataType_CanConvertToPoco()
        {
            var sourceNode = SourceNode.Resource("Test", "CustomCodeableConcept", 
                SourceNode.Node("coding",
                    SourceNode.Valued("system", "http://loinc.org"),
                    SourceNode.Valued("code", "8302-2"),
                    SourceNode.Valued("display", "Testing")
                    ));
            var pn = sourceNode.ToTypedElement().Select("coding").First().ToPocoNode();
            pn.Poco.Should().BeOfType<DynamicDataType>();
            ISourceNode sn = pn;
            ITypedElement te = pn;
            var teCC = te.ToPoco<Coding>();
            var cc = sn.ToPoco<Coding>();
            cc.HasOverflow.Should().Be(teCC.HasOverflow);
            cc.Display.Should().Be("Testing").And.Be(teCC.Display);
            cc.System.Should().Be("http://loinc.org").And.Be(teCC.System);
            cc.Code.Should().Be("8302-2").And.Be(teCC.Code);
        }
    }
}