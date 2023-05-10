/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;

#nullable enable

namespace Hl7.Fhir.Introspection
{
    /// <summary>
    /// This attribute is applied to classes that represent FHIR datatypes and resources.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class CqlTypeAttribute : VersionedAttribute
    {
        public CqlTypeAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// The name of this type when used in CQL. For FHIR types, this name is normally
        /// prefixed with <c>FHIR.</c>".
        /// </summary>
        public string Name { get; private set; }
    }
}

#nullable restore