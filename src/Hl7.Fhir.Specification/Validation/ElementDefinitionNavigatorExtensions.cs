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


//#if NETSTANDARD1_1
//        public static int Count(this string s, Func<char, bool> predicate)
//        {
//            return s.ToCharArray().Where(predicate).Count();
//        }
//#endif

        public static bool IsPrimitiveValueConstraint(this ElementDefinition ed)
        {
            //TODO: There is something smarter for this in STU3
            var path = ed.Path;

            return path.EndsWith(".value") && ed.Type.All(t => t.Code == null);
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


        public static string QualifiedDefinitionPath(this ElementDefinitionNavigator nav)
        {
            string path = "";

            if (nav.StructureDefinition != null && nav.StructureDefinition.Url != null)
                path = "{" + nav.StructureDefinition.Url + "}";

            path += nav.Path;

            return path;
        }
    }

}