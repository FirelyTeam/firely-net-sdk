/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Validation
{

    public class ValidationContext
    {
        public IArtifactSource ArtifactSource { get; set; }

        public bool GenerateSnapshot { get; set; }
        public SnapshotGeneratorSettings GenerateSnapshotSettings { get; set; }

        // Containing Bundle, parent Resource?
        // Options: validate_across_references, log verbosity, validate extension urls
        // FP SymbolTable
    }

}