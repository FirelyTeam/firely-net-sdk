/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Hl7.Fhir.Specification.Navigation
{
    internal static class ElementDefinitionNavigatorExtensions
    {
        internal static string? GetFhirPathConstraint(this ElementDefinition.ConstraintComponent cc)
        {
            // This was required for 3.0.0, but was rectified in the 3.0.1 technical update
            //if (cc.Key == "ele-1")
            //    return "(children().count() > id.count()) | hasValue()";
            return cc.Expression;
        }

        internal static string ConstraintDescription(this ElementDefinition.ConstraintComponent cc)
        {
            var desc = cc.Key ?? "(unnamed)";

            if (cc.Human != null)
                desc += " \"" + cc.Human + "\"";

            return desc;
        }

        /// <summary>
        /// Resolve a the contentReference in a navigator and returns a navigator that is located on the target of the contentReference.
        /// </summary>
        /// <remarks>The current navigator must be located at an element that contains a contentReference.</remarks>
        public static bool TryFollowContentReference(this ElementDefinitionNavigator sourceNavigator, Func<string, StructureDefinition?> resolver, [NotNullWhen(true)] out ElementDefinitionNavigator? targetNavigator)
        {
            targetNavigator = null;

            var reference = sourceNavigator.Current.ContentReference;
            if (reference is null) return false;

            var profileRef = ProfileReference.Parse(reference);

            if (profileRef.IsAbsolute && profileRef.CanonicalUrl != sourceNavigator.StructureDefinition.Url)
            {
                // an external reference (e.g. http://hl7.org/fhir/StructureDefinition/Questionnaire#Questionnaire.item)

                var profile = resolver(profileRef.CanonicalUrl!);
                if (profile is null) return false;
                targetNavigator = ElementDefinitionNavigator.ForSnapshot(profile);
            }
            else
            {
                // a local reference
                targetNavigator = sourceNavigator.ShallowCopy();
            }

            return targetNavigator.JumpToNameReference("#" + profileRef.ElementName);
        }

    }
}