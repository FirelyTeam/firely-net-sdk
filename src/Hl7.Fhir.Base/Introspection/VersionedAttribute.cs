/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Introspection
{
    public abstract class VersionedAttribute : Attribute, IFhirVersionDependent
    {
        /// <inheritdoc cref="IFhirVersionDependent.Since" />
        public FhirRelease Since { get; set; } = (FhirRelease)int.MinValue;
    }

    public abstract class VersionedValidationAttribute : ValidationAttribute, IFhirVersionDependent
    {
        /// <inheritdoc cref="IFhirVersionDependent.Since" />
        public FhirRelease Since { get; set; } = (FhirRelease)int.MinValue;

    }
}
