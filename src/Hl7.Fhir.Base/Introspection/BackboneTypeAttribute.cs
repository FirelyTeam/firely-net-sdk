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
    /// This attribute is applied to classes that represent Backbone types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class BackboneTypeAttribute : Attribute
    {
        public BackboneTypeAttribute(string definitionPath)
        {
            DefinitionPath = definitionPath ?? throw new ArgumentNullException(nameof(definitionPath));
        }

        /// <summary>
        /// The path in the StructureDefinition where the backbone was defined.
        /// </summary>
        /// <remarks>When the backbone is reused in the resource, and thus appears on multiple paths,
        /// this is the initial path where the backbone was defined.</remarks>
        public string DefinitionPath { get; private set; }
    }
}

#nullable restore