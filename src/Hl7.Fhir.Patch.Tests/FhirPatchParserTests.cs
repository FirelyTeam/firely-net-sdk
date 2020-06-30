/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Patch.Tests
{
    [TestClass]
    public class FhirPatchParserTests
    {
        private FhirPatchParameters createFhirPatch ()
        {
            return new FhirPatchParameters()
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
                                Value = new Code("add")
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "path",
                                Value = new FhirString("Patient")
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "name",
                                Value = new FhirString("identifier")
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "value",
                                Value = new Identifier("http://test.sys", "test123")
                            }
                        }
                    },
                    new Parameters.ParameterComponent
                    {
                        Name = "operation",
                        Part = new List<Parameters.ParameterComponent>
                        {
                            new Parameters.ParameterComponent
                            {
                                Name = "type",
                                Value = new Code("insert")
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "path",
                                Value = new FhirString("Patient.identifier")
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "index",
                                Value = new Integer(1)
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "value",
                                Value = new Identifier("http://test.sys", "test456")
                            }
                        }
                    },
                    new Parameters.ParameterComponent
                    {
                        Name = "operation",
                        Part = new List<Parameters.ParameterComponent>
                        {
                            new Parameters.ParameterComponent
                            {
                                Name = "type",
                                Value = new Code("delete")
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "path",
                                Value = new FhirString("Patient.identifier[0]")
                            }
                        }
                    },
                    new Parameters.ParameterComponent
                    {
                        Name = "operation",
                        Part = new List<Parameters.ParameterComponent>
                        {
                            new Parameters.ParameterComponent
                            {
                                Name = "type",
                                Value = new Code("replace")
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "path",
                                Value = new FhirString("Patient.identifier[0]")
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "value",
                                Value = new Identifier("http://test.sys", "test789")
                            }
                        }
                    },
                    new Parameters.ParameterComponent
                    {
                        Name = "operation",
                        Part = new List<Parameters.ParameterComponent>
                        {
                            new Parameters.ParameterComponent
                            {
                                Name = "type",
                                Value = new Code("move")
                            },
                            new Parameters.ParameterComponent
                            {
                                Name = "path",
                                Value = new FhirString("Patient.identifier")
                            },
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
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void GivenITypedElement_ShouldParseFhirPatchPoco ()
        {
            // Arrange
            var fhirPatch = createFhirPatch().ToTypedElement();

            // Act
            var result = fhirPatch.ToPoco<FhirPatchParameters>();

            // Assert
            Assert.IsInstanceOfType(result, typeof(FhirPatchParameters));
        }

        [TestMethod]
        public void GivenITypedElement_ShouldParseGenericPocoAsParameters ()
        {
            // Arrange
            var fhirPatch = createFhirPatch().ToTypedElement();

            // Act
            var result = fhirPatch.ToPoco();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Parameters));
        }

        [TestMethod]
        public void GivenISourceNode_ShouldParseFhirPatchPoco ()
        {
            // Arrange
            var fhirPatch = createFhirPatch().ToTypedElement().ToSourceNode();

            // Act
            var result = fhirPatch.ToPoco<FhirPatchParameters>();

            // Assert
            Assert.IsInstanceOfType(result, typeof(FhirPatchParameters));
        }

        [TestMethod]
        public void GivenISourceNode_ShouldParseGenericPocoAsParameters ()
        {
            // Arrange
            var fhirPatch = createFhirPatch().ToTypedElement().ToSourceNode();

            // Act
            var result = fhirPatch.ToPoco();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Parameters));
        }

        [TestMethod]
        public void GivenISourceNode_ShouldParseITypedElement ()
        {
            // Arrange
            var fhirPatch = createFhirPatch().ToTypedElement().ToSourceNode();

            // Act
            var result = fhirPatch.ToTypedElement(new PocoStructureDefinitionSummaryProvider());
            result.VisitAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ITypedElement));
        }

        [TestMethod]
        public void GivenJsonString_ShouldParseFhirPatchPoco ()
        {
            // Arrange
            var fhirPatch = createFhirPatch().ToJson();

            // Act
            var result = new FhirJsonParser().Parse<FhirPatchParameters>(fhirPatch);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FhirPatchParameters));
        }

        [TestMethod]
        public void GivenJsonString_ShouldParseGenericPocoAsParameters ()
        {
            // Arrange
            var fhirPatch = createFhirPatch().ToJson();

            // Act
            var result = new FhirJsonParser().Parse(fhirPatch);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Parameters));
        }

        [TestMethod]
        public void GivenXmlString_ShouldParseFhirPatchPoco ()
        {
            // Arrange
            var fhirPatch = createFhirPatch().ToXml();

            // Act
            var result = new FhirXmlParser().Parse<FhirPatchParameters>(fhirPatch);

            // Assert
            Assert.IsInstanceOfType(result, typeof(FhirPatchParameters));
        }

        [TestMethod]
        public void GivenXmlString_ShouldParseGenericPocoAsParameters ()
        {
            // Arrange
            var fhirPatch = createFhirPatch().ToXml();

            // Act
            var result = new FhirXmlParser().Parse(fhirPatch);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Parameters));
        }

    }
}
