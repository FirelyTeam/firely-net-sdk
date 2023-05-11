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
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class CqlElementAttribute : Attribute
    {
        public CqlElementAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Whether this element is the primary code path for this type. This means
        /// the element can implicitly be referred to in a retrieve statement.
        /// </summary>
        public bool IsPrimaryCodePath { get; set; } = false;
        public string Name { get; }
    }
}

#nullable restore