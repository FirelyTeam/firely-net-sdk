/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Patch.Exceptions;
using Hl7.Fhir.Patch.Operations;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Patch.Tests
{
    [TestClass]
    public class PatchDocumentTests
    {
        readonly IStructureDefinitionSummaryProvider provider = new PocoStructureDefinitionSummaryProvider();
        readonly FhirPathCompiler compiler = new FhirPathCompiler();

        private Patient createPatient ()
        {
            return new Patient
            {
                // Single simple property
                Active = true,

                // Single complex property
                MaritalStatus = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", "M", "Married", null),

                // Collection with single element
                Name = new List<HumanName>
                {
                    new HumanName().WithGiven("Will").AndFamily("Smith")
                },

                // Collection with multiple elements
                Identifier = new List<Identifier>
                {
                    new Identifier("http://nu.nl", "1234567"),
                    new Identifier("http://toen.nl", "7654321")
                }
            };
        }

        
        private T ApplyPatch<T> (T patient, PatchDocument patch, Action<PatchError> errorReporter)
            where T : Base
        {
            return patient.ToTypedElement().Apply(patch, errorReporter).ToPoco<T>();
        }

        private T ApplyPatch<T> (T patient, PatchDocument patch)
            where T : Base
        {
            return patient.ToTypedElement().Apply(patch).ToPoco<T>();
        }

        [TestMethod]
        public void ApplyAddPatchOperation_AtRoot_ShouldAddSimpleElement ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient");

            var operation = new AddOperation(fhirPath, "active", new FhirBoolean(true).ToTypedElement());
            patch.Add(operation);

            // Act
            var patchedPatient = ApplyPatch(new Patient(), patch);

            // Assert
            Assert.AreEqual(true, patchedPatient.Active);
        }

        [TestMethod]
        public void ApplyAddPatchOperation_AtRoot_ShouldAddComplexElement ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient");

            CodeableConcept value = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", "M", "Married", null);
            var operation = new AddOperation(fhirPath, "maritalStatus", value.ToTypedElement());
            patch.Add(operation);

            // Act
            var patchedPatient = ApplyPatch(new Patient(), patch);

            // Assert
            Assert.AreEqual(value.ToJson(), patchedPatient.MaritalStatus.ToJson());
        }

        [TestMethod]
        public void ApplyAddPatchOperation_AtRoot_ShouldAppendToCollection ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient");

            var value = new Identifier("http://test.sys", "test123");
            var operation = new AddOperation(fhirPath, "identifier", value.ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            // Act
            var patchedPatient = ApplyPatch(patient, patch);

            // Assert
            Assert.AreEqual(patient.Identifier.Count + 1, patchedPatient.Identifier.Count);
            Assert.AreEqual(value.ToJson(), patchedPatient.Identifier[patchedPatient.Identifier.Count - 1].ToJson());
        }

        [TestMethod]
        public void ApplyAddPatchOperation_ToNestedProperty_ShouldAddSimpleElement ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.maritalStatus");

            var operation = new AddOperation(fhirPath, "text", new FhirString("test123").ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            // Act
            var patchedPatient = ApplyPatch(patient, patch);

            // Assert
            Assert.AreEqual("test123", patchedPatient.MaritalStatus.Text);
        }

        [TestMethod]
        public void ApplyAddPatchOperation_ToNonExistentProperty_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.address");

            var operation = new AddOperation(fhirPath, "text", new FhirString("test123").ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyAddPatchOperation_ToCollectionProperty_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.Name");

            var operation = new AddOperation(fhirPath, "given", new FhirString("test123").ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyAddPatchOperation_WithInvalidName_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient");

            var operation = new AddOperation(fhirPath, "fakeProperty", new FhirString("test123").ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyAddPatchOperation_WithExistingTargetProperty_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient");

            var operation = new AddOperation(fhirPath, "active", new FhirBoolean(true).ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyAddPatchOperation_WithInvalidValue_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient");

            var operation = new AddOperation(fhirPath, "active", new FhirString("test123").ToTypedElement());
            patch.Add(operation);

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(new Patient(), patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyInsertPatchOperation_ShouldInsertElementAtSpecifiedIndex ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.identifier");

            var value = new Identifier("http://test.sys", "test123");
            var operation = new InsertOperation(fhirPath, 1, value.ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            // Act
            var patchedPatient = ApplyPatch(patient, patch);

            // Assert
            Assert.AreEqual(patient.Identifier.Count + 1, patchedPatient.Identifier.Count);
            Assert.AreEqual(value.ToJson(), patchedPatient.Identifier[1].ToJson());
        }

        [TestMethod]
        public void ApplyInsertPatchOperation_ShouldAppendElement ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.identifier");

            var value = new Identifier("http://test.sys", "test123");
            var operation = new InsertOperation(fhirPath, 2, value.ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            // Act
            var patchedPatient = ApplyPatch(patient, patch);

            // Assert
            Assert.AreEqual(patient.Identifier.Count + 1, patchedPatient.Identifier.Count);
            Assert.AreEqual(value.ToJson(), patchedPatient.Identifier[patchedPatient.Identifier.Count - 1].ToJson());
        }

        [TestMethod]
        public void ApplyInsertPatchOperation_ToCollectionWithSingleElement_ShouldInsertElementAtSpecifiedIndex ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.name");

            var value = new HumanName().WithGiven("Sandy").AndFamily("Wilson");
            var operation = new InsertOperation(fhirPath, 0, value.ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            // Act
            var patchedPatient = ApplyPatch(patient, patch);

            // Assert
            Assert.AreEqual(2, patchedPatient.Identifier.Count);
            Assert.AreEqual(value.ToJson(), patchedPatient.Name[0].ToJson());
        }

        [TestMethod]
        public void ApplyInsertPatchOperation_ToEmptyCollection_Throws ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.name");

            var value = new HumanName().WithGiven("Sandy").AndFamily("Wilson");
            var operation = new InsertOperation(fhirPath, 0, value.ToTypedElement());
            patch.Add(operation);

            // Act & Assert
            Assert.ThrowsException<PatchException>(() => ApplyPatch(new Patient(), patch));
        }

        [TestMethod]
        public void ApplyInsertPatchOperation_ToNonExistentCollection_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.address");

            var operation = new InsertOperation(fhirPath, 0, new Address().ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyInsertPatchOperation_ToNonCollection_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.active");

            var operation = new InsertOperation(fhirPath, 0, new FhirBoolean(true).ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyInsertPatchOperation_WithOutOfBoundIndex_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.identifier");

            var value = new Identifier("http://test.sys", "test123");
            var operation = new InsertOperation(fhirPath, 999, value.ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyInsertPatchOperation_WithInvalidValue_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.identifier");

            var value = new HumanName().WithGiven("Sandy").AndFamily("Wilson");
            var operation = new InsertOperation(fhirPath, 1, value.ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyDeletePatchOperation_ShouldDeleteSimpleElement ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.active");
            var operation = new DeleteOperation(fhirPath);
            patch.Add(operation);

            // Act
            var patchedPatient = ApplyPatch(createPatient(), patch);

            // Assert
            Assert.IsNull(patchedPatient.Active);
        }

        [TestMethod]
        public void ApplyDeletePatchOperation_ShouldDeleteComplexElement ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.maritalStatus");
            var operation = new DeleteOperation(fhirPath);
            patch.Add(operation);

            // Act
            var patchedPatient = ApplyPatch(createPatient(), patch);

            // Assert
            Assert.IsNull(patchedPatient.MaritalStatus);
        }

        [TestMethod]
        public void ApplyDeletePatchOperation_ShouldDeleteCollectionWithSingleElement ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.name");
            var operation = new DeleteOperation(fhirPath);
            patch.Add(operation);

            // Act
            var patchedPatient = ApplyPatch(createPatient(), patch);

            // Assert
            Assert.AreEqual(0, patchedPatient.Name.Count);
        }

        [TestMethod]
        public void ApplyDeletePatchOperation_ShouldDeleteSingleElementFromCollection ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.identifier[1]");
            var operation = new DeleteOperation(fhirPath);
            patch.Add(operation);

            var patient = createPatient();

            // Act
            var patchedPatient = ApplyPatch(patient, patch);

            // Assert
            Assert.AreEqual(patient.Identifier.Count - 1, patchedPatient.Identifier.Count);
            Assert.AreEqual(patient.Identifier[0].ToJson(), patchedPatient.Identifier[0].ToJson());
        }

        [TestMethod]
        public void ApplyDeletePatchOperation_ToNonExistentElement_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.active");

            var operation = new DeleteOperation(fhirPath);
            patch.Add(operation);

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(new Patient(), patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyDeletePatchOperation_ToCollectionWithMultipleElements_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.identifier");

            var operation = new DeleteOperation(fhirPath);
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyReplacePatchOperation_ShouldReplaceSimpleElement ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.active");
            var operation = new ReplaceOperation(fhirPath, new FhirBoolean(false).ToTypedElement());
            patch.Add(operation);

            // Act
            var patchedPatient = ApplyPatch(createPatient(), patch);

            // Assert
            Assert.AreEqual(false, patchedPatient.Active);
        }

        [TestMethod]
        public void ApplyReplacePatchOperation_ShouldReplaceComplexElement ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.maritalStatus");

            CodeableConcept value = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-MaritalStatus", "D", "Divorced", null);
            var operation = new ReplaceOperation(fhirPath, value.ToTypedElement());
            patch.Add(operation);

            // Act
            var patchedPatient = ApplyPatch(createPatient(), patch);

            // Assert
            Assert.AreEqual(value.ToJson(), patchedPatient.MaritalStatus.ToJson());
        }

        [TestMethod]
        public void ApplyReplacePatchOperation_ShouldReplaceSingleElementFromCollection ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.identifier[1]");

            var value = new Identifier("http://test.sys", "test123");
            var operation = new ReplaceOperation(fhirPath, value.ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            // Act
            var patchedPatient = ApplyPatch(patient, patch);

            // Assert
            Assert.AreEqual(patient.Identifier.Count, patchedPatient.Identifier.Count);
            Assert.AreEqual(value.ToJson(), patchedPatient.Identifier[1].ToJson());
        }

        [TestMethod]
        public void ApplyReplacePatchOperation_ToNonExistentElement_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.active");

            var operation = new ReplaceOperation(fhirPath, new FhirBoolean(true).ToTypedElement());
            patch.Add(operation);

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(new Patient(), patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyReplacePatchOperation_ToCollectionWithMultipleElements_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.identifier");

            var value = new Identifier("http://test.sys", "test123");
            var operation = new ReplaceOperation(fhirPath, value.ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyReplacePatchOperation_WithInvalidValue_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.active");

            var operation = new ReplaceOperation(fhirPath, new FhirString("test123").ToTypedElement());
            patch.Add(operation);

            var patient = createPatient();

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(patient, patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyMovePatchOperation_ShouldMoveElementAtSourceIndexToDestinationIndex ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.identifier");

            var operation = new MoveOperation(fhirPath, 1, 0);
            patch.Add(operation);

            var patient = createPatient();

            // Act
            var patchedPatient = ApplyPatch(patient, patch);

            // Assert
            Assert.AreEqual(patient.Identifier.Count, patchedPatient.Identifier.Count);
            Assert.AreEqual(patient.Identifier[1].ToJson(), patchedPatient.Identifier[0].ToJson());
        }

        [TestMethod]
        public void ApplyMovePatchOperation_ToNonExistentSourceElement_ShouldReportError ()
        {
            // Arange
            var patch = new PatchDocument() { Provider = provider };
            var fhirPath = compiler.Compile("Patient.address");

            var operation = new MoveOperation(fhirPath, 1, 0);
            patch.Add(operation);

            var errorList = new List<PatchError>();
            Action<PatchError> errorReporter = err => errorList.Add(err);

            // Act
            ApplyPatch(new Patient(), patch, errorReporter);

            // Assert
            Assert.AreEqual(1, errorList.Count);
            Assert.AreEqual(operation, errorList[0].Operation);
        }

        [TestMethod]
        public void ApplyMultiplePatchOperations_ShouldAllBeAppliedSequentially ()
        {
            // Arrange
            var patch = new PatchDocument() { Provider = provider };
            
            var maritalStatusPath = compiler.Compile("Patient.maritalStatus");
            var deleteOperation = new DeleteOperation(maritalStatusPath);
            patch.Add(deleteOperation);

            var rootPath = compiler.Compile("Patient");
            var value = new Identifier("http://test.sys", "test123");
            var addOperation = new AddOperation(rootPath, "identifier", value.ToTypedElement());
            patch.Add(addOperation);

            var patient = createPatient();

            // Act
            var patchedPatient = ApplyPatch(patient, patch);

            // Assert
            Assert.IsNull(patchedPatient.MaritalStatus);
            Assert.AreEqual(patient.Identifier.Count + 1, patchedPatient.Identifier.Count);
            Assert.AreEqual(value.ToJson(), patchedPatient.Identifier[patchedPatient.Identifier.Count - 1].ToJson());
        }


    }
}
