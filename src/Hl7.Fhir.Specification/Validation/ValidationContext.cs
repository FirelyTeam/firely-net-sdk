/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using System;

namespace Hl7.Fhir.Validation
{

    public class ValidationContext
    {
        public IResourceResolver ResourceResolver { get; set; }

        /// <summary>
        /// The validator needs StructureDefinitions to have a snapshot form to function. If a StructureDefinition
        /// without a snapshot is encountered, should the validator generate the snapshot from the differential
        /// present in the StructureDefinition? Default is 'false'.
        /// </summary>
        public bool GenerateSnapshot { get; set; }

        /// <summary>
        /// If GenerateSnapshot is set to 'true', these settings will allow the user to configure how
        /// snapshot generation is done.
        /// </summary>
        public SnapshotGeneratorSettings GenerateSnapshotSettings { get; set; }

        /// <summary>
        /// Include informational tracing information in the validation output. Useful for debugging purposes. Default is 'false'.
        /// </summary>
        public bool Trace { get; set; }

        // Containing Bundle, parent Resource?
        // Options: validate extension urls
        // FP SymbolTable

        public ReferenceKind ValidateReferencedResources { get; set; }

        /// <summary>
        /// StructureDefinition may contain FluentPath constraints to enfore invariants in the data that cannot
        /// be expresses using StructureDefinition alone. This validation can be turned off for performance or
        /// debugging purposes. Default is 'false'.
        /// </summary>
        public bool SkipConstraintValidation { get; set; }

        // Use external server for some referenced resources
    }

    [Flags]
    public enum ReferenceKind
    {
        None = 0x0,
        Contained = 0x1,
        Bundled = 0x2,
        External = 0x4
    }

}