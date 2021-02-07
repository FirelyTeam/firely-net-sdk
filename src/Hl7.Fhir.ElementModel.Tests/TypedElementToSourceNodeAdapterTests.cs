﻿using Hl7.Fhir.ElementModel.Adapters;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var typedElement = patient.ToTypedElement();
            var sourceNode = typedElement.ToSourceNode();

            var result = sourceNode.Annotation<ISourceNode>();
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(TypedElementToSourceNodeAdapter), result.GetType());
            Assert.AreSame(sourceNode, result);

            var result2 = sourceNode.Annotation<IResourceTypeSupplier>();
            Assert.IsNotNull(result2);
            Assert.AreEqual("TypedElementToSourceNodeAdapter", result2.GetType().Name); // I use the classname here, because PocoElementNode is internal in Hl7.Fhir.Core
            Assert.AreSame(sourceNode, result2);
        }

        [TestMethod]
        public void AnnotationsFromParsingTest()
        {
            var _sdsProvider = new PocoStructureDefinitionSummaryProvider();
            var patientJson = "{\"resourceType\":\"Patient\", \"active\":\"true\"}";
            var patient  = FhirJsonNode.Parse(patientJson);
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
        public void SourceNodeFromElementNodeReturnsResourceTypeSupplier()
        {
            var _sdsProvider = new PocoStructureDefinitionSummaryProvider();
            var patientJson = "{\"resourceType\":\"Patient\", \"active\":\"true\"}";
            var patientNode = FhirJsonNode.Parse(patientJson);
            var typedPatient = patientNode.ToTypedElement(_sdsProvider, "Patient");

            var elementNode = ElementNode.FromElement(typedPatient);
            var adapter = elementNode.ToSourceNode();

            Assert.AreEqual(typeof(TypedElementToSourceNodeAdapter), adapter.GetType(), "ISourceNode is provided by TypedElementToSourceNodeAdapter");

            var result = adapter.Annotation<IResourceTypeSupplier>();
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(TypedElementToSourceNodeAdapter), result.GetType());
            Assert.AreEqual("Patient", adapter.GetResourceTypeIndicator());
            Assert.AreSame(adapter, result);
        }
    }
}
