/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Validation
{

    public class ValidationSettings
    {
        [Obsolete("Use the CreateDefault() method, as using this static member may cause threading issues.")]
        public static readonly ValidationSettings Default = new ValidationSettings();

        /// <summary>
        /// The resolver to use when references to other resources are encountered in the instance.
        /// </summary>
        public IResourceResolver ResourceResolver { get; set; }

        /// <summary>
        /// The terminology service to use to validate coded instance data.
        /// </summary>
        public ITerminologyService TerminologyService { get; set; }

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

        // Options: validate extension urls
        // FP SymbolTable

        /// <summary>
        /// StructureDefinition may contain FhirPath constraints to enfore invariants in the data that cannot
        /// be expresses using StructureDefinition alone. This validation can be turned off for performance or
        /// debugging purposes. Default is 'false'.
        /// </summary>
        public bool SkipConstraintValidation { get; set; }


        /// <summary>
        /// If a reference is encountered that references to a resource outside of the current instance being validated,
        /// this setting controls whether the validator will call out to the ResourceResolver to try to resolve the
        /// external reference. Note: References that refer to resources inside the current instance (i.e.
        /// contained resources, Bundle entries) will always be followed and validated.
        /// </summary>
        public bool ResolveExteralReferences { get; set; }

        /// <summary>
        /// If set to true (and the XDocument specific overloads of validate() are used), the validator will run
        /// .NET XSD validation prior to running profile validation
        /// </summary>
        public bool EnableXsdValidation { get; set; }

        /// <summary>Default constructor. Creates a new <see cref="ValidationSettings"/> instance with default property values.</summary>
        public ValidationSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="ValidationSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public ValidationSettings(ValidationSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="ValidationSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(ValidationSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.EnableXsdValidation = EnableXsdValidation;
            other.GenerateSnapshot = GenerateSnapshot;
            other.GenerateSnapshotSettings = GenerateSnapshotSettings?.Clone();
            other.ResolveExteralReferences = ResolveExteralReferences;
            other.ResourceResolver = ResourceResolver;
            other.SkipConstraintValidation = SkipConstraintValidation;
            other.TerminologyService = TerminologyService;
            other.Trace = Trace;
        }

        /// <summary>Creates a new <see cref="ValidationSettings"/> object that is a copy of the current instance.</summary>
        public ValidationSettings Clone() => new ValidationSettings(this);

        /// <summary>Creates a new <see cref="ValidationSettings"/> instance with default property values.</summary>
        public static ValidationSettings CreateDefault() => new ValidationSettings();

    }
}