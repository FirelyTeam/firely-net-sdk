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
    /// Used to specify the name of the binding for a bound property.
    /// </summary>
    [CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class BindingAttribute : VersionedAttribute
    {
        public BindingAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
