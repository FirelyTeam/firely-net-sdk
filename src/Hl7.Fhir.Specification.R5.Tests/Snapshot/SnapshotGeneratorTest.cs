/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests
{
    public partial class SnapshotGeneratorTest2
    {
        internal static StructureDefinition MedicationUsageWithSimpleQuantitySlice => new StructureDefinition()
        {
            Type = FHIRAllTypes.MedicationStatement.GetLiteral(),
            BaseDefinition = ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.MedicationStatement),
            Name = "MedicationStatementWithSimpleQuantitySlice",
            Url = @"http://example.org/fhir/StructureDefinition/MedicationStatementWithSimpleQuantitySlice",
            Derivation = StructureDefinition.TypeDerivationRule.Constraint,
            Kind = StructureDefinition.StructureDefinitionKind.Resource,
            Differential = new StructureDefinition.DifferentialComponent()
            {
                Element = new List<ElementDefinition>()
                {
                    new ElementDefinition("MedicationStatement.dosage.dose[x]")
                    {
                        Slicing = new ElementDefinition.SlicingComponent()
                        {
                            Discriminator = ElementDefinition.DiscriminatorComponent.ForTypeSlice().ToList()
                        }
                    },
                    new ElementDefinition("MedicationStatement.dosage.dose[x]")
                    {
                        SliceName = "doseSimpleQuantity",
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRAllTypes.Quantity.GetLiteral(),
                                Profile = new string[] { ModelInfo.CanonicalUriForFhirCoreType("SimpleQuantity") }
                            }
                        }
                    },
                    new ElementDefinition("MedicationStatement.dosage.dose[x]")
                    {
                        SliceName = "dosePeriod",
                        Type = new List<ElementDefinition.TypeRefComponent>()
                        {
                            new ElementDefinition.TypeRefComponent()
                            {
                                Code = FHIRAllTypes.Period.GetLiteral()
                            }
                        }
                    },

                }
            }
        };

    }
}
