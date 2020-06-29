/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Patch.Operations;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Patch.Tests
{
    [TestClass]
    public class FhirPatchReaderTests
    {
        readonly FhirPathCompiler compiler = new FhirPathCompiler();

        private ITypedElement createPatient ()
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
            }.ToTypedElement();
        }

        private FhirPatch createFhirPatchBase (string type, string path)
        {
            return new FhirPatch()
            {
                Parameter = new List<Parameters.ParameterComponent>
                {
                    new Parameters.ParameterComponent
                    {
                        Name = "operation",
                        Part = new List<Parameters.ParameterComponent>
                        {
                            new Parameters.ParameterComponent
                            {
                                Name = "type",
                                Value = new Code(type)
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "path",
                                Value = new FhirString(path)
                            }
                        }
                    },

                }
            };
        }

        [TestMethod]
        public void ConvertFhirParameters_AddOperation_ToFhirPatch ()
        {
            // Arrange
            var value = new Identifier("http://test.sys", "test123");

            var fhirPatch = createFhirPatchBase("add", "Patient");
            fhirPatch.Parameter[0].Part.AddRange(new List<Parameters.ParameterComponent>
            {
                new Parameters.ParameterComponent
                {
                    Name = "name",
                    Value = new FhirString("identifier")
                },
                new Parameters.ParameterComponent
                {
                    Name = "value",
                    Value = value
                }
            });

            //Act
            PatchDocument patchDocument = fhirPatch;

            // Assert
            var operations = patchDocument.Operations;
            Assert.AreEqual(1, operations.Count);

            var operation = operations[0] as AddOperation;
            Assert.IsNotNull(operation);
            Assert.AreEqual(OperationType.Add, operation.OperationType);
            Assert.AreEqual("identifier", operation.Name);
            Assert.IsInstanceOfType(operation.Value, typeof(ITypedElement));
            Assert.AreEqual(value.ToJson(), (operation.Value as ITypedElement).ToJson());

            var sampleObject = createPatient();
            Assert.AreEqual(sampleObject.Select("Patient").First().ToJson(), operation.Path(sampleObject, EvaluationContext.CreateDefault()).First().ToJson());
        }


        [TestMethod]
        public void ConvertFhirParameters_InsertOperation_ToFhirPatch ()
        {
            // Arrange
            var value = new Identifier("http://test.sys", "test123");

            var fhirPatch = createFhirPatchBase("insert", "Patient.identifier");
            fhirPatch.Parameter[0].Part.AddRange(new List<Parameters.ParameterComponent>
            {
                new Parameters.ParameterComponent
                {
                    Name = "index",
                    Value = new Integer(1)
                },
                new Parameters.ParameterComponent
                {
                    Name = "value",
                    Value = value
                }
            });

            //Act
            PatchDocument patchDocument = fhirPatch;

            // Assert
            var operations = patchDocument.Operations;
            Assert.AreEqual(1, operations.Count);

            var operation = operations[0] as InsertOperation;
            Assert.IsNotNull(operation);
            Assert.AreEqual(OperationType.Insert, operation.OperationType);
            Assert.AreEqual(1, operation.Index);
            Assert.IsInstanceOfType(operation.Value, typeof(ITypedElement));
            Assert.AreEqual(value.ToJson(), (operation.Value as ITypedElement).ToJson());

            var sampleObject = createPatient();
            Assert.AreEqual(sampleObject.Select("Patient.identifier").First().ToJson(), operation.Path(sampleObject, EvaluationContext.CreateDefault()).First().ToJson());
        }

        [TestMethod]
        public void ConvertFhirParameters_DeleteOperation_ToFhirPatch ()
        {
            // Arrange
            var fhirPatch = createFhirPatchBase("delete", "Patient.identifier[1]");

            //Act
            PatchDocument patchDocument = fhirPatch;

            // Assert
            var operations = patchDocument.Operations;
            Assert.AreEqual(1, operations.Count);

            var operation = operations[0] as DeleteOperation;
            Assert.IsNotNull(operation);
            Assert.AreEqual(OperationType.Delete, operation.OperationType);

            var sampleObject = createPatient();
            Assert.AreEqual(sampleObject.Select("Patient.identifier[1]").First().ToJson(), operation.Path(sampleObject, EvaluationContext.CreateDefault()).First().ToJson());
        }

        [TestMethod]
        public void ConvertFhirParameters_ReplaceOperation_ToFhirPatch ()
        {
            // Arrange
            var value = new Identifier("http://test.sys", "test123");

            var fhirPatch = createFhirPatchBase("replace", "Patient.identifier[1]");
            fhirPatch.Parameter[0].Part.AddRange(new List<Parameters.ParameterComponent>
            {
                new Parameters.ParameterComponent
                {
                    Name = "value",
                    Value = value
                }
            });

            //Act
            PatchDocument patchDocument = fhirPatch;

            // Assert
            var operations = patchDocument.Operations;
            Assert.AreEqual(1, operations.Count);

            var operation = operations[0] as ReplaceOperation;
            Assert.IsNotNull(operation);
            Assert.AreEqual(OperationType.Replace, operations[0].OperationType);
            Assert.IsInstanceOfType(operation.Value, typeof(ITypedElement));
            Assert.AreEqual(value.ToJson(), (operation.Value as ITypedElement).ToJson());

            var sampleObject = createPatient();
            Assert.AreEqual(sampleObject.Select("Patient.identifier[1]").First().ToJson(), operation.Path(sampleObject, EvaluationContext.CreateDefault()).First().ToJson());
        }

        [TestMethod]
        public void ConvertFhirParameters_MoveOperation_ToFhirPatch ()
        {
            // Arrange
            var fhirPatch = createFhirPatchBase("move", "Patient.identifier");
            fhirPatch.Parameter[0].Part.AddRange(new List<Parameters.ParameterComponent>
            {
                new Parameters.ParameterComponent
                {
                    Name = "source",
                    Value = new Integer(1)
                },
                new Parameters.ParameterComponent
                {
                    Name = "destination",
                    Value = new Integer(0)
                }
            });

            //Act
            PatchDocument patchDocument = fhirPatch;

            // Assert
            var operations = patchDocument.Operations;
            Assert.AreEqual(1, operations.Count);

            var operation = operations[0] as MoveOperation;
            Assert.IsNotNull(operation);
            Assert.AreEqual(OperationType.Move, operation.OperationType);
            Assert.AreEqual(1, operation.Source);
            Assert.AreEqual(0, operation.Destination);

            var sampleObject = createPatient();
            Assert.AreEqual(sampleObject.Select("Patient.identifier").First().ToJson(), operation.Path(sampleObject, EvaluationContext.CreateDefault()).First().ToJson());
        }


        [TestMethod]
        public void ConvertFhirParameters_MultipleOperation_ToFhirPatch ()
        {
            // Arrange
            var value = new Identifier("http://test.sys", "test123");

            var fhirPatch = createFhirPatchBase("replace", "Patient.identifier[1]");
            fhirPatch.Parameter[0].Part.AddRange(new List<Parameters.ParameterComponent>
            {
                new Parameters.ParameterComponent
                {
                    Name = "value",
                    Value = value
                }
            });
            fhirPatch.Parameter.Add(createFhirPatchBase("delete", "Patient.active").Parameter[0]);

            //Act
            PatchDocument patchDocument = fhirPatch;

            // Assert
            CollectionAssert.AreEqual(new List<OperationType> { OperationType.Replace, OperationType.Delete }, patchDocument.Operations.Select(x => x.OperationType).ToList());
        }
    }
}
