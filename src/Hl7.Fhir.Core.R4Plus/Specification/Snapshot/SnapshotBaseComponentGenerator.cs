/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
 
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Snapshot
{
    // Methods to (re-)generate the ElementDefinition.Base components from the associated base profile
    // Uses annotations on ElementDefinition.Base to suppress duplicate (re-)generation
    // Behavior is controlled by _settings.NormalizeElementBase

    partial class SnapshotGenerator
    {
        private T.Task ensureSnapshotBaseComponents(StructureDefinition structureDef, bool force = false) =>
            ensureBaseComponents(structureDef.Snapshot.Element, structureDef.BaseDefinition, force);

        /// <summary>(Re-)generate the <see cref="ElementDefinition.Base"/> components.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances.</param>
        /// <param name="baseProfileUrl">The canonical url of the base profile, as defined by the <see cref="StructureDefinition.BaseDefinition"/> property.</param>
        /// <param name="force">If <c>true</c>, then always (re-)generate the Base component, even if it exists.</param>
        private async T.Task ensureBaseComponents(IList<ElementDefinition> elements, string baseProfileUrl, bool force = false)
        {
            var nav = new ElementDefinitionNavigator(elements);
            if (nav.MoveToFirstChild() && !string.IsNullOrEmpty(baseProfileUrl))
            {
                var sd = await AsyncResolver.FindStructureDefinitionAsync(baseProfileUrl).ConfigureAwait(false);
                if (await ensureSnapshot(sd, baseProfileUrl).ConfigureAwait(false))
                {
                    var baseNav = new ElementDefinitionNavigator(sd);
                    if (baseNav.MoveToFirstChild())
                    {
                        nav.Current.EnsureBaseComponent(baseNav.Current, force);

                        if (nav.MoveToFirstChild() && baseNav.MoveToFirstChild())
                        {
                            do
                            {
                                await ensureBaseComponents(nav, baseNav, force).ConfigureAwait(false);
                            } while (nav.MoveToNext());
                        }
                    }

                }
            }
        }

        private async T.Task ensureBaseComponents(ElementDefinitionNavigator nav, ElementDefinitionNavigator baseNav, bool force = false)
        {
            // Debug.Print($"[nameof(generateElementBase)}] Path = '{nav.Path}'  Base = '{baseNav.Path}'");
            var elem = nav.Current;
            Debug.Assert(elem != null);

            // Determine if the current element matches the current base element
            if (baseNav.PathName == nav.PathName || ElementDefinitionNavigator.IsRenamedChoiceTypeElement(baseNav.PathName, nav.PathName))
            {
                // Match!

                // Initialize Base component
                elem.EnsureBaseComponent(baseNav.Current, force);

                // Recurse child elements
                var navBm = nav.Bookmark();
                var baseNavBm = baseNav.Bookmark();
                if (nav.MoveToFirstChild() && baseNav.MoveToFirstChild())
                {
                    do
                    {
                        await ensureBaseComponents(nav, baseNav, force).ConfigureAwait(false);
                    } while (nav.MoveToNext());

                    nav.ReturnToBookmark(navBm);
                    baseNav.ReturnToBookmark(baseNavBm);
                }

                // Consume the matched base element
                baseNav.MoveToNext();

                return;
            }
            else
            {
                // Drill down base profile
                var baseUrl = baseNav.StructureDefinition.BaseDefinition;
                if (baseUrl != null)
                {
                    var baseDef = await AsyncResolver.FindStructureDefinitionAsync(baseUrl).ConfigureAwait(false);
                    if (await ensureSnapshot(baseDef, baseUrl, elem.Path).ConfigureAwait(false))
                    {
                        baseNav = new ElementDefinitionNavigator(baseDef);
                        if (baseNav.MoveToFirstChild())
                        {
                            await ensureBaseComponents(nav, baseNav, force).ConfigureAwait(false);
                            return;
                        }
                    }
                }
            }

            // No match... try base profile
            // Debug.Print($"[nameof(generateElementBase)}] Path = '{nav.Path}'  (no base)");
        }

    }

    /// <summary>Internal extension method for initializing the <see cref="ElementDefinition.Base"/> component.</summary>
    static class SnapshotGeneratorBaseComponentExtensionMethods
    {
        /// <summary>Ensure that the <see cref="ElementDefinition.Base"/> component is properly initialized.</summary>
        /// <param name="elem">An <see cref="ElementDefinition"/> instance.</param>
        /// <param name="baseElem">The associated base <see cref="ElementDefinition"/> instance.</param>
        /// <param name="force">If <c>true</c>, then always (re-)generate the Base component, even if it exists.</param>
        public static void EnsureBaseComponent(this ElementDefinition elem, ElementDefinition baseElem, bool force = false)
        {
            Debug.Assert(elem != null);

            // [WMR 20190130] R4: Root element base refers to self (.Path = .Base.Path)
            if (elem.IsRootElement())
            {
                // Base component references self
                elem.Base = createBaseComponent(
                    elem.MaxElement,
                    elem.MinElement,
                    elem.PathElement
                );
            }
            else if (force || elem.Base == null || !elem.Base.IsCreatedBySnapshotGenerator())
            {
                // [WMR 20190130] STU3
                // [WMR 20160903] Explicitly exclude root types (Resource and Element), they have no base
                //if (isRootTypeElementPath(elem.Path))
                //{
                //    return;
                //}

                if (baseElem.Base != null)
                {
                    // Inherit Base component from base element
                    elem.Base = createBaseComponent(
                        baseElem.Base.MaxElement,
                        baseElem.Base.MinElement,
                        baseElem.Base.PathElement
                    );
                }
                else
                {
                    // Initialize new Base component from base element
                    elem.Base = createBaseComponent(
                        baseElem.MaxElement,
                        baseElem.MinElement,
                        baseElem.PathElement
                    );
                }

                // Debug.WriteLine($"[{nameof(EnsureBaseComponent)}] #{elem.GetHashCode()} Path = {elem.Path}  Base = {elem.Base.Path}");
                Debug.Assert(elem.Base == null || elem.Base.IsCreatedBySnapshotGenerator());
            }
        }

        //static bool isRootTypeElementPath(string path)
        //{
        //    var root = ElementDefinitionNavigator.GetPathRoot(path);
        //    // Note: the API could provide a utility method to determine if a type name represents a root type (StructureDef.Base == null)
        //    return root == "Resource" || root == "Element";
        //}

        static ElementDefinition.BaseComponent createBaseComponent(FhirString maxElement, UnsignedInt minElement, FhirString pathElement)
        {
            var result = new ElementDefinition.BaseComponent()
            {
                MaxElement = (FhirString)maxElement?.DeepCopy(),
                MinElement = (UnsignedInt)minElement?.DeepCopy(),
                PathElement = (FhirString)pathElement?.DeepCopy()
            };
            result.SetCreatedBySnapshotGenerator();
            return result;
        }

    }
}
