/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;

namespace Hl7.Fhir.Validation
{

    internal static class ElementDefinitionNavigatorExtensions
    {
        public static string GetFhirPathConstraint(this ElementDefinition.ConstraintComponent cc)
        {
            return cc.GetStringExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression");
        }

        internal static bool IsResourcePlaceholder(this ElementDefinition ed)
        {
            if (ed.Type == null) return false;
            return ed.Type.Any(t => t.Code == FHIRDefinedType.Resource || t.Code == FHIRDefinedType.DomainResource);
        }

        public static string ConstraintDescription(this ElementDefinition.ConstraintComponent cc)
        {
            var desc = cc.Key;

            if (cc.Human != null)
                desc += " \"" + cc.Human + "\"";

            return desc;
        }


        /// <summary>
        /// Builds a fully qualified path for the ElementDefinition.
        /// </summary>
        /// <param name="def"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        /// <remarks>A fully qualified path is the path of the ElementDefinition, prefixed by the canonical of 
        /// the StructureDefinition the ElementDefinition is part of.</remarks>
        public static string QualifiedDefinitionPath(this ElementDefinition def, StructureDefinition parent = null) =>
            parent?.Url != null ?
                $"{{{parent?.Url}}}{def.Path}"
                : $"{def.Path}";

        /// <summary>
        /// Builds a fully qualified path for the ElementDefinition.
        /// </summary>
        /// <remarks>A fully qualified path is the path of the ElementDefinition, prefixed by the canonical of 
        /// the StructureDefinition the ElementDefinition is part of.</remarks>
        public static string QualifiedDefinitionPath(this ElementDefinitionNavigator nav) =>
            QualifiedDefinitionPath(nav.Current, nav.StructureDefinition);
    }

}