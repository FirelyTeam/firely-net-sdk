/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Terminology;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;

#nullable enable

namespace Hl7.Fhir.FhirPath
{
    public class FhirEvaluationContext : EvaluationContext
    {
        /// <summary>Default constructor. Creates a new <see cref="FhirEvaluationContext"/> instance with default property values.</summary>
        public FhirEvaluationContext()
        {
        }

        public ICodeValidationTerminologyService? TerminologyService { get; set; }

        private Func<string, PocoNode?>? _elementResolver;

        public Func<string, PocoNode?>? ElementResolver
        {
            get { return _elementResolver; }
            set { _elementResolver = value; }
        }

        /// <summary>
        /// Checks whether a node conforms to the profile with the given canonical url, used by the FHIRPath
        /// <c>conformsTo()</c> function. The canonical is passed as written in the expression (including any
        /// <c>|version</c>). Return <c>true</c> or <c>false</c> for a profile that could be checked, or <c>null</c>
        /// when the profile cannot be resolved.
        /// </summary>
        /// <remarks>The checker is called for every profile, including the canonicals of the core FHIR types, and any
        /// exception it throws propagates to the caller. When this is not set, or returns <c>null</c>, <c>conformsTo()</c>
        /// only checks the type of the input against the canonicals of the core FHIR types
        /// (e.g. <c>http://hl7.org/fhir/StructureDefinition/Patient</c>) and raises an error for any other profile.</remarks>
        public Func<PocoNode, string, bool?>? ProfileConformanceChecker { get; set; }
    }
}

#nullable restore