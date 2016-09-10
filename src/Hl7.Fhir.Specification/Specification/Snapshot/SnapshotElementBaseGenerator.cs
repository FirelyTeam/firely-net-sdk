/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
 
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    // Methods to (re-)generate the ElementDefinition.Base components from the associated base profile
    // Controlled by _settings.NormalizeElementBase

    partial class SnapshotGenerator
    {
        /// <summary>
        /// Initialize the <see cref="ElementDefinition.Base"/> components of the <see cref="StructureDefinition.Snapshot"/> component.
        /// <br />
        /// If <see cref="SnapshotGeneratorSettings.NormalizeElementBase"/> equals <c>true</c>, then Base is derived from the structure definition that originally introduces the element.
        /// If <see cref="SnapshotGeneratorSettings.NormalizeElementBase"/> equals <c>false</c>, then Base is derived from the immediate base structure definition.
        /// </summary>
        /// <param name="structureDef">A <see cref="StructureDefinition"/> instance with a valid snapshot component.</param>
        /// <param name="force">If <c>true</c>, then always (re-)generate the Base component, even if it exists.</param>
        public void GenerateSnapshotElementsBase(StructureDefinition structureDef, bool force = false)
        {
            if (structureDef == null) { throw Error.ArgumentNull("structureDef"); }
            if (!structureDef.HasSnapshot) { throw Error.Argument("structureDef", "StructureDefinition.Snapshot component is null or empty."); }
            clearIssues();
            generateSnapshotElementsBase(structureDef, force);
        }

        private void generateSnapshotElementsBase(StructureDefinition structureDef, bool force = false)
        {
            generateElementsBase(structureDef.Snapshot.Element, structureDef.BaseDefinition, force);
        }

        /// <summary>(Re-)generate the <see cref="ElementDefinition.Base"/> components.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances.</param>
        /// <param name="baseProfileUrl">The canonical url of the base profile, as defined by the <see cref="StructureDefinition.Base"/> property.</param>
        /// <param name="force">If <c>true</c>, then always (re-)generate the Base component, even if it exists.</param>
        private void generateElementsBase(IList<ElementDefinition> elements, string baseProfileUrl, bool force = false)
        {
            var nav = new ElementDefinitionNavigator(elements);
            if (nav.MoveToFirstChild() && !string.IsNullOrEmpty(baseProfileUrl))
            {
                var sd = _resolver.FindStructureDefinition(baseProfileUrl);
                if (verifyStructureDef(sd, baseProfileUrl))
                {
                    var baseNav = new ElementDefinitionNavigator(sd);
                    if (baseNav.MoveToFirstChild())
                    {
                        ensureElementBase(nav.Current, baseNav.Current, force);

                        if (nav.MoveToFirstChild() && baseNav.MoveToFirstChild())
                        {
                            do
                            {
                                generateElementsBase(nav, baseNav, force);
                            } while (nav.MoveToNext());
                        }
                    }

                }
            }
        }

        private void generateElementsBase(ElementDefinitionNavigator nav, ElementDefinitionNavigator baseNav, bool force = false)
        {
            // Debug.Print("[generateElementBase] Path = {0}  Base = {1}".FormatWith(nav.Path, baseNav.Path));
            var elem = nav.Current;
            Debug.Assert(elem != null);

            // Determine if the current element matches the current base element
            if (baseNav.PathName == nav.PathName || ElementDefinitionNavigator.IsRenamedChoiceElement(baseNav.PathName, nav.PathName))
            {
                // Match!

                // Initialize Base component
                ensureElementBase(elem, baseNav.Current, force);

                // Recurse child elements
                var navBm = nav.Bookmark();
                var baseNavBm = baseNav.Bookmark();
                if (nav.MoveToFirstChild() && baseNav.MoveToFirstChild())
                {
                    do
                    {
                        generateElementsBase(nav, baseNav, force);
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
                    var baseDef = _resolver.FindStructureDefinition(baseUrl);
                    if (verifyStructureDef(baseDef, baseUrl, ToNamedNode(elem)))
                    {
                        baseNav = new ElementDefinitionNavigator(baseDef);
                        if (baseNav.MoveToFirstChild())
                        {
                            generateElementsBase(nav, baseNav, force);
                            return;
                        }
                    }
                }
            }

            // No match... try base profile
            // Debug.Print("[generateElementBase] Path = {0}  (no base)".FormatWith(nav.Path));
        }

        /// <summary>Assign the <see cref="ElementDefinition.Base"/> component if necessary.</summary>
        /// <param name="elem">An <see cref="ElementDefinition"/> instance.</param>
        /// <param name="baseElem">The associated base <see cref="ElementDefinition"/> instance.</param>
        /// <param name="force">If <c>true</c>, then always (re-)generate the Base component, even if it exists.</param>
        private void ensureElementBase(ElementDefinition elem, ElementDefinition baseElem, bool force = false)
        {
            Debug.Assert(elem != null);
            var normalizeElementBase = _settings.NormalizeElementBase;
            if (force || elem.Base == null || (normalizeElementBase && !isCreatedBySnapshotGenerator(elem.Base)))
            {
                // [WMR 20160903] Explicitly exclude root types (Resource and Element), they have no base
                if (isRootTypeElementPath(elem.Path))
                {
                    return;
                }

                Debug.Assert(baseElem != null);

                if (normalizeElementBase && baseElem.Base != null) //  && !isRootElement
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

                // Debug.Print("[ensureElementBase] #{0} Path = {1}  Base = {2}".FormatWith(elem.GetHashCode(), elem.Path, elem.Base.Path));
                Debug.Assert(elem.Base == null || isCreatedBySnapshotGenerator(elem.Base));
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
                MaxElement = (FhirString)maxElement.DeepCopy(),
                MinElement = (Integer)minElement.DeepCopy(),
                PathElement = (FhirString)pathElement.DeepCopy(),
            };
            result.AddAnnotation(new CreatedBySnapshotGeneratorAnnotation());
            return result;
        }

        // Custom annotation to mark generated elements, so we can prevent duplicate re-generation
        class CreatedBySnapshotGeneratorAnnotation
        {
            private readonly DateTime _created;
            public DateTime Created { get { return _created; } }
            public CreatedBySnapshotGeneratorAnnotation() { _created = DateTime.Now; }
        }

        /// <summary>Determines if the specified element was created by the <see cref="SnapshotGenerator"/> class.</summary>
        /// <param name="elem">A FHIR <see cref="Element"/>.</param>
        /// <returns><c>true</c> if the element was created by the <see cref="SnapshotGenerator"/> class, or <c>false</c> otherwise.</returns>
        static bool isCreatedBySnapshotGenerator(Element elem)
        {
            return elem != null && elem.Annotation<CreatedBySnapshotGeneratorAnnotation>() != null;
        }

    }
}
