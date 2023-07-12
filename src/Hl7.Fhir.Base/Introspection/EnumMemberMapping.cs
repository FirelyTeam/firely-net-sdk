/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Reflection;

namespace Hl7.Fhir.Introspection
{
    /// <summary>
    /// A container for the metadata of a FHIR code from a valueset as present on the .NET Enum member.
    /// </summary>
    public class EnumMemberMapping
    {
        private EnumMemberMapping(FieldInfo fieldInfo, string code, string? system, object value, string? description, string? defaultSystem)
        {
            Code = code;
            System = system ?? defaultSystem;
            Description = description;
            Value = value;
            NativeField = fieldInfo;
        }

        /// <summary>
        /// The code that is represented by this member.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// The coding system that is associated with the code.
        /// </summary>
        public string? System { get; }

        /// <summary>
        /// A description of the concept.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// The .NET enum value for this enum member.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// The original <see cref="FieldInfo"/> the metadata was extracted from.
        /// </summary>
        public FieldInfo NativeField { get; }

        /// <summary>
        /// Inspects the given enum member, extracting metadata from its attributes and creating a new <see cref="EnumMemberMapping"/>.
        /// </summary>
        public static bool TryCreate(FieldInfo member, out EnumMemberMapping? result, FhirRelease release = (FhirRelease)int.MaxValue, string? defaultSystem = null)
        {
            result = null;
            if (ClassMapping.GetAttribute<EnumLiteralAttribute>(member, release) is not { } ela) return false;

            var code = ela.Literal ?? member.Name;
            var value = (Enum)member.GetValue(null)!;
            var desc = ClassMapping.GetAttribute<DescriptionAttribute>(member, release)?.Description;

            result = new EnumMemberMapping(member, code, ela.System, value, desc, defaultSystem);
            return true;
        }
    }
}

#nullable restore