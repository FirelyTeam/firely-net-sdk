/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.Introspection
{
#if USE_CQLMODEL_ATTRIBUTE
    /// <summary>
    /// Signals that the assembly contains classes that define metadata for 
    /// types used in CQL.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
    public sealed class CqlModelAssemblyAttribute : Attribute
    {
        public CqlModelAssemblyAttribute(string name, string version, string url)
        {
            Name = name;
            Version = version;
            Url = url;
        }

        /// <summary>
        /// The namespace name for all types in the assembly. E.g. "FHIR".
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The version of the model this assembly represents.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// The url that uniquely identifies this namespace.
        /// </summary>
        public string Url { get; }
    }
#endif
}
