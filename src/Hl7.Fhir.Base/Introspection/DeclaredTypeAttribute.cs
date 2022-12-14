/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Introspection
{
    /// <summary>
    /// Used to indicate that the type for this property in the POCO
    /// does not represent the type in the FHIR specification, but rather the type given
    /// in the constructor to this attribute.
    /// </summary>
    [CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DeclaredTypeAttribute : VersionedAttribute
    {
        public DeclaredTypeAttribute()
        {
        }

        public Type Type { get; set; }
    }
}
