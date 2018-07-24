/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
 
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    // Methods to (re-)generate the ElementDefinition.Base components from the associated base profile
    // Uses annotations on ElementDefinition.Base to suppress duplicate (re-)generation
    // Behavior is controlled by _settings.NormalizeElementBase

    partial class SnapshotGenerator
    {
        /// <summary>Initialize the <see cref="ElementDefinition.Base"/> components of the <see cref="StructureDefinition.Snapshot"/> component.</summary>
        /// <param name="structureDef">A <see cref="StructureDefinition"/> instance with a valid snapshot component.</param>
        /// <param name="force">If <c>true</c>, then always (re-)generate the Base component, even if it exists.</param>
        public void GenerateSnapshotBaseComponents(StructureDefinition structureDef, bool force = false)
        {
            if (structureDef == null) { throw Error.ArgumentNull(nameof(structureDef)); }
            if (!structureDef.HasSnapshot) { throw Error.Argument(nameof(structureDef), "The StructureDefinition.Snapshot component is null or empty."); }
            clearIssues();
            ensureSnapshotBaseComponents(structureDef, force);
        }

        void ensureSnapshotBaseComponents(StructureDefinition structureDef, bool force = false)
        {
            ensureBaseComponents(structureDef.Snapshot.Element, structureDef.Base, force);
        }

        /// <summary>(Re-)generate the <see cref="ElementDefinition.Base"/> components.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances.</param>
        /// <param name="baseProfileUrl">The canonical url of the base profile, as defined by the <see cref="StructureDefinition.Base"/> property.</param>
        /// <param name="force">If <c>true</c>, then always (re-)generate the Base component, even if it exists.</param>
        void ensureBaseComponents(IList<ElementDefinition> elements, string baseProfileUrl, bool force = false)
        {
            var nav = new ElementDefinitionNavigator(elements);
            if (nav.MoveToFirstChild() && !string.IsNullOrEmpty(baseProfileUrl))
            {
                var sd = _resolver.FindStructureDefinition(baseProfileUrl);
                if (ensureSnapshot(sd, baseProfileUrl))
                {
                    var baseNav = new ElementDefinitionNavigator(sd);
                    if (baseNav.MoveToFirstChild())
                    {
                        nav.Current.EnsureBaseComponent(baseNav.Current, force);

                        if (nav.MoveToFirstChild() && baseNav.MoveToFirstChild())
                        {
                            do
                            {
                                ensureBaseComponents(nav, baseNav, force);
                            } while (nav.MoveToNext());
                        }
                    }

                }
            }
        }

        void ensureBaseComponents(ElementDefinitionNavigator nav, ElementDefinitionNavigator baseNav, bool force = false)
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
                        ensureBaseComponents(nav, baseNav, force);
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
                var baseUrl = baseNav.StructureDefinition.Base;
                if (baseUrl != null)
                {
                    var baseDef = _resolver.FindStructureDefinition(baseUrl);
                    if (ensureSnapshot(baseDef, baseUrl, elem.ToNamedNode()))
                    {
                        baseNav = new ElementDefinitionNavigator(baseDef);
                        if (baseNav.MoveToFirstChild())
                        {
                            ensureBaseComponents(nav, baseNav, force);
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

            // [WMR 20161004] HL7 WGM Baltimore: ElementDefinition.Base should be fully normalized
            // i.e. ElementDefinition.Base references the element definition that originally introduces the element
            // e.g. Patient.meta => Base.Path = Resource.meta

            // var normalizeElementBase = _settings.NormalizeElementBase;
            // if (force || elem.Base == null || (normalizeElementBase && !elem.Base.isCreatedBySnapshotGenerator()))
            if (force || elem.Base == null || !elem.Base.IsCreatedBySnapshotGenerator())
            {
                // [WMR 20160903] Explicitly exclude root types (Resource and Element), they have no base
                if (isRootTypeElementPath(elem.Path))
                {
                    return;
                }

                Debug.Assert(baseElem != null);

                // if (normalizeElementBase && baseElem.Base != null)
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

        static bool isRootTypeElementPath(string path)
        {
            var root = ElementDefinitionNavigator.GetPathRoot(path);
            // Note: the API could provide a utility method to determine if a type name represents a root type (StructureDef.Base == null)
            return root == "Resource" || root == "Element";
        }

        static ElementDefinition.BaseComponent createBaseComponent(FhirString maxElement, Integer minElement, FhirString pathElement)
        {
            var result = new ElementDefinition.BaseComponent()
            {
                MaxElement = (FhirString)maxElement?.DeepCopy(),
                MinElement = (Integer)minElement?.DeepCopy(),
                PathElement = (FhirString)pathElement?.DeepCopy()
            };
            result.SetCreatedBySnapshotGenerator();
            return result;
        }

    }
}
