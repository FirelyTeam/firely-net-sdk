/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Snapshot
{
    internal static class ElementDefinitionExtensions
    {
        /// <summary>
        /// If the element is constrained to a single common type (i.e. if all the existing
        /// <see cref="ElementDefinition.TypeRefComponent"/> items share a common type code),
        /// then return that common type code, otherwise return <c>null</c>.
        /// </summary>
        /// <param name="elem">An element definition.</param>
        /// <returns>A type code.</returns>
        public static string CommonTypeCode(this ElementDefinition elem) => elem?.Type.CommonTypeCode();


        /// <summary>Returns <c>true</c> if the element represents an extension with a custom extension profile url, or <c>false</c> otherwise.</summary>
        public static bool IsMappedExtension(this ElementDefinition defn)
        {
            return defn.IsExtension() && defn.PrimaryTypeProfile() != null;
        }

        /// <summary>Returns the primary element type, if it exists.</summary>
        /// <param name="defn">An <see cref="ElementDefinition"/> instance.</param>
        /// <returns>A <see cref="ElementDefinition.TypeRefComponent"/> instance, or <c>null</c>.</returns>
        public static ElementDefinition.TypeRefComponent PrimaryType(this ElementDefinition defn)
        {
            if (defn.Type.IsNullOrEmpty())
                return null;
            else if (defn.Type.Count == 1)
                return defn.Type[0];
            else  //if there are multiple types (value[x]), try to get a common type, otherwise, use Element as the common datatype             
            {
                var distinctTypeCode = defn.CommonTypeCode() ?? FHIRAllTypes.Element.GetLiteral();
                return new ElementDefinition.TypeRefComponent() { Code = distinctTypeCode };
            }

        }

        /// <summary>
        /// Returns the type profile reference of the primary element type, if it exists, or <c>null</c>
        /// </summary>
        public static string PrimaryTypeProfile(this ElementDefinition elem)
        {
            if (elem.Type != null)
            {
                var primaryType = elem.Type.FirstOrDefault();
                if (primaryType != null)
                {
                    return primaryType.Profile;
                }
            }
            return null;
        }

        /// <summary>Returns the type code of the primary element type, or <c>null</c>.</summary>
        public static FHIRAllTypes? PrimaryTypeCode(this ElementDefinition elem)
        {
            if (elem.Type != null)
            {
                var type = elem.Type.FirstOrDefault();
                if (type != null && !string.IsNullOrEmpty(type.Code))
                {
                    return Utility.EnumUtility.ParseLiteral<FHIRAllTypes>(type.Code);
                    // return (FHIRAllTypes)Enum.Parse(typeof(FHIRAllTypes), type.Code);
                }
            }
            return null;
        }

        /// <summary>
        /// If the element is constrained to a single common type (i.e. if all the existing
        /// <see cref="ElementDefinition.TypeRefComponent"/> items share a common type code),
        /// then return that common type code, otherwise return <c>null</c>.
        /// </summary>
        /// <param name="types">A list of element types.</param>
        /// <returns>A type code.</returns>
        public static string CommonTypeCode(this IList<ElementDefinition.TypeRefComponent> types)
        {
            if (types != null)
            {
                var cnt = types.Count;
                if (cnt > 0)
                {
                    var firstCode = types[0].Code;
                    for (int i = 1; i < cnt; i++)
                    {
                        var code = types[i].Code;
                        // Ignore empty codes (invalid, Type.code is required)
                        if (code != null && code != firstCode)
                        {
                            return null;
                        }
                    }
                    return firstCode;
                }
            }
            return null;
        }


    }

}
