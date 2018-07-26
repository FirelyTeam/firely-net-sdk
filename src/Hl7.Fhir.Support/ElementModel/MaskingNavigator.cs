/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{


    public class MaskingNavigatorSettings
    {
        public enum PreserveBundleMode
        {
            /// <summary>
            /// All Bundles (including nested) are masked like any other resource 
            /// </summary>
            None,

            /// <summary>
            /// The Bundle at the root is preserved, nested bundles are masked
            /// </summary>
            Root,

            /// <summary>
            /// All Bundles (including nested) are exempt from masking
            /// </summary>
            All
        }

        public PreserveBundleMode PreserveBundle;

        /// <summary>
        /// Include top-level mandatory elements, including all their children
        /// </summary>
        public bool IncludeMandatory;

        /// <summary>
        /// Include all elements marked "in summary" in the definition of the element
        /// </summary>
        public bool IncludeInSummary;

        /// <summary>
        /// Include all elements marked "is modifier" in the definition of the element
        /// </summary>
        public bool IncludeIsModifier;

        /// <summary>
        /// Exclude all elements of type "Narrative"
        /// </summary>
        public bool ExcludeNarrative;

        /// <summary>
        /// Exclude all elements of type "Markdown"
        /// </summary>
        public bool ExcludeMarkdown;

        /// <summary>
        /// Start by including all elements
        /// </summary>
        public bool IncludeAll;

        /// <summary>
        /// List of names op top-level elements to include, including their children
        /// </summary>
        public string[] IncludeElements;

        /// <summary>
        /// List of top-level elements to exclude
        /// </summary>
        public string[] ExcludeElements;
       
        internal MaskingNavigatorSettings Clone() =>
            new MaskingNavigatorSettings
            {
                IncludeMandatory = this.IncludeMandatory,
                IncludeInSummary = this.IncludeInSummary,
                IncludeIsModifier = this.IncludeIsModifier,
                ExcludeMarkdown = this.ExcludeMarkdown,
                ExcludeNarrative = this.ExcludeNarrative,
                IncludeAll = this.IncludeAll,
                IncludeElements = this.IncludeElements.ToArray(),
                ExcludeElements = this.ExcludeElements.ToArray()
            };

    }

    public class MaskingNavigator : IElementNavigator, IAnnotated, IExceptionSource
    {
        public static MaskingNavigator ForSummary(IElementNavigator nav) =>
            new MaskingNavigator(nav, new MaskingNavigatorSettings
            {
                IncludeInSummary = true,
                PreserveBundle = MaskingNavigatorSettings.PreserveBundleMode.Root
            });

        public static MaskingNavigator ForText(IElementNavigator nav) =>
            new MaskingNavigator(nav, new MaskingNavigatorSettings
            {
                IncludeElements = new[] { "text", "id", "meta" },
                IncludeMandatory = true, IncludeIsModifier = true,
                PreserveBundle = MaskingNavigatorSettings.PreserveBundleMode.All
            });

        public static MaskingNavigator ForData(IElementNavigator nav) =>
            new MaskingNavigator(nav, new MaskingNavigatorSettings
            {
                IncludeAll = true,
                ExcludeNarrative = true
            });

        public MaskingNavigator(IElementNavigator source, MaskingNavigatorSettings settings = null)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (!source.InPipeline(typeof(ScopedNavigator)))
                throw Error.Argument("ScopedNavigator can only be used on a navigator chain that contains a ScopedNavigator", nameof(source));

            Source = source;
            _settings = settings?.Clone() ?? new MaskingNavigatorSettings();
        }

        private ScopedNavigator getScope() => this.Annotation<ScopedNavigator>();

        private bool atRootBundle(IElementNavigator node) =>
            getScope().Parent == null && node.Location.StartsWith("Bundle.");

        private bool atTopLevel(IElementNavigator node) =>
            // check whether there is a single path separator -> path looks like 'Resource.xxxxx'
            node.Location.IndexOf('.') == node.Location.LastIndexOf('.');

        private MaskingNavigatorSettings _settings;

        public IElementNavigator Source { get; private set; }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Source.Name;

        public string Type => Source.Type;

        public object Value => Source.Value;

        public string Location => Source.Location;

        private MaskingNavigator() { }   // for Clone()

        public IElementNavigator Clone()
        {
            return new MaskingNavigator()
            {
                Source = this.Source.Clone(),
                _settings = this._settings,
            };
        }

        public bool Included(IElementNavigator node)
        {
            var included = _settings.IncludeAll;

            var ed = node.GetElementDefinitionSummary();
            if (ed != null)
            {
                included |= _settings.IncludeMandatory && ed.IsRequired;
                included |= _settings.IncludeInSummary && ed.InSummary;
            }

            if (atTopLevel(node))
            {
                var location = node.Location.Split('.')[1].TrimEnd('[');
                included |= _settings.IncludeElements?.Any(incl => location == incl) ?? false;

                if (_settings.ExcludeElements?.Any(excl => location == excl) == true)
                    included = false;
            }

            return included;
        }


        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(MaskingNavigator))
                return new[] { this };
            else
                return Source.Annotations(type);
        }

        private bool findNext(IElementNavigator scan, string nameFilter)
        {
            if (nameFilter != null)
                return !Included(scan);
            else
            {
                do
                {
                    if (Included(scan)) return true;
                }
                while (scan.MoveToNext());
                return false;
            }
        }

        public bool MoveToNext(string nameFilter = null)
        {
            var scan = Source.Clone();
            var success = scan.MoveToNext(nameFilter);

            // Return immediately if there's no match at all
            if (!success) return false;

            success = findNext(scan, nameFilter);

            if (success)
                Source = scan;

            return success;
        }

        public bool MoveToFirstChild(string nameFilter = null)
        {
            var scan = Source.Clone();
            var success = scan.MoveToFirstChild(nameFilter);

            // Return immediately if there's no match at all
            if (!success) return false;

            success = findNext(scan, nameFilter);

            // When unsuccessful, restore to where we were before
            if (success)
            {
                Source = scan;
                var def = Source.GetElementDefinitionSummary();
            }

            return success;
        }
    }
}
