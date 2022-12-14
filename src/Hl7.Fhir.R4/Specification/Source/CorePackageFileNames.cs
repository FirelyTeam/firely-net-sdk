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
        public const string CORE_PACKAGENAME = "hl7.fhir.r4.corexml.tgz";
        public const string EXPANSIONS_PACKAGENAME = "hl7.fhir.r4.expansions.tgz";
        internal const string FHIR_CORE_PACKAGE_NAME = "hl7.fhir.r4.corexml@4.0.1";
        internal const string FHIR_CORE_EXPANSIONS_PACKAGE_NAME = "hl7.fhir.r4.expansions@4.0.1";
        internal const string FHIR_PACKAGE_SERVER = "http://packages.fhir.org";
    }
}

#nullable restore
