/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// File names of the FHIR packages shipped with the SDK
    /// </summary>
    public static class CorePackageFileNames
    {
        public const string CORE_PACKAGENAME = "hl7.fhir.r4b.corexml.tgz";
        public const string EXPANSIONS_PACKAGENAME = "hl7.fhir.r4b.expansions.tgz";
        internal const string FHIR_CORE_PACKAGE_NAME = "hl7.fhir.r4b.core";
        internal const string FHIR_CORE_EXPANSIONS_PACKAGE_NAME = "hl7.fhir.r4b.expansions";
        internal const string FHIR_PACKAGE_SERVER = "http://packages2.fhir.org/packages";
    }
}

#nullable restore
