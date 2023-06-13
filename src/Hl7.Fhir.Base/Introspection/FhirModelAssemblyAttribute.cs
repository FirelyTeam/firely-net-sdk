/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Introspection
{
    /// <summary>
    /// Signals that the assembly contains classes that define metadata for the
    /// FHIR resources and datatypes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
    public sealed class FhirModelAssemblyAttribute : VersionedAttribute
    {
        public FhirModelAssemblyAttribute()
        {
            Since = Specification.FhirRelease.STU3;
        }

        public FhirModelAssemblyAttribute(Specification.FhirRelease since)
        {
            Since = since;
        }
    }
}
