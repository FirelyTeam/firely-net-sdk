/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

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

        ///// <summary>
        ///// Include all elements marked "is modifier" in the definition of the element
        ///// </summary>
        //public bool IncludeIsModifier;

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
                PreserveBundle = this.PreserveBundle,
                IncludeMandatory = this.IncludeMandatory,
                IncludeInSummary = this.IncludeInSummary,
                //   IncludeIsModifier = this.IncludeIsModifier,
                ExcludeMarkdown = this.ExcludeMarkdown,
                ExcludeNarrative = this.ExcludeNarrative,
                IncludeAll = this.IncludeAll,
                IncludeElements = this.IncludeElements?.ToArray(),
                ExcludeElements = this.ExcludeElements?.ToArray()
            };

    }

    public class MaskingNode : ITypedElement, IAnnotated, IExceptionSource
    {
        public static MaskingNode ForSummary(ITypedElement node) =>
            new MaskingNode(node, new MaskingNavigatorSettings
            {
                IncludeInSummary = true,
                PreserveBundle = MaskingNavigatorSettings.PreserveBundleMode.Root
            });

        public static MaskingNode ForText(ITypedElement node) =>
            new MaskingNode(node, new MaskingNavigatorSettings
            {
                IncludeElements = new[] { "text", "id", "meta" },
                IncludeMandatory = true, //IncludeIsModifier = true,
                PreserveBundle = MaskingNavigatorSettings.PreserveBundleMode.All
            });

        public static MaskingNode ForData(ITypedElement node) =>
            new MaskingNode(node, new MaskingNavigatorSettings
            {
                IncludeAll = true,
                ExcludeNarrative = true
            });

        public static MaskingNode ForCount(ITypedElement node) =>
          new MaskingNode(node, new MaskingNavigatorSettings
          {
              IncludeMandatory = true,
              IncludeElements = new[] { "id", "total" },
          });

        public MaskingNode(ITypedElement source, MaskingNavigatorSettings settings = null)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (source.Annotation<ScopedNode>() == null)
                throw Error.Argument("MaskingNavigator can only be used on a navigator chain that contains a ScopedNavigator", nameof(source));

            Source = source;
            _settings = settings?.Clone() ?? new MaskingNavigatorSettings();

            if (Source is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private MaskingNode(MaskingNode parent, ITypedElement source)
        {
            Source = source;
            _settings = parent._settings;
            ExceptionHandler = parent.ExceptionHandler;
        }

        private ScopedNode getScope(ITypedElement node) =>
            node.Annotation<ScopedNode>();

        private readonly MaskingNavigatorSettings _settings;

        public ITypedElement Source { get; private set; }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public string Name => Source.Name;

        public string InstanceType => Source.InstanceType;

        public object Value => Source.Value;

        public string Location => Source.Location;

        public IElementDefinitionSummary Definition => Source.Definition;

        private bool included(ITypedElement node)
        {
            // Trivially, we will include the root
            if (!node.Location.Contains(".")) return true;

            var scope = getScope(node);

            //bool atTopLevel() =>
            //    // check whether there is a single path separator -> path looks like 'Resource.xxxxx'
            //    node.LocalLocation.IndexOf('.') == node.LocalLocation.LastIndexOf('.');

            bool atRootBundle() => atBundle() && scope.ParentResource == null;
            bool atBundle() => scope.NearestResourceType == "Bundle";

            switch (_settings.PreserveBundle)
            {
                case MaskingNavigatorSettings.PreserveBundleMode.All when atBundle():
                case MaskingNavigatorSettings.PreserveBundleMode.Root when atRootBundle():
                    return true;
                    // else fall through
            }

            var included = _settings.IncludeAll;

            var ed = scope.Definition;
            if (ed != null)
            {
                included |= _settings.IncludeMandatory && ed.IsRequired;
                included |= _settings.IncludeInSummary && ed.InSummary;
            }

            var loc = scope.LocalLocation;
            var nearest = scope.NearestResourceType;
            included |= _settings.IncludeElements?.Any(matches) ?? false;

            if (_settings.ExcludeElements?.Any(matches) == true)
                return false;

            bool matches(string filter)
            {
                var f = nearest + "." + filter;
                return loc == f || loc.StartsWith(f + ".") || loc.StartsWith(f + "[");    // include matches + children
            }


            if (_settings.ExcludeMarkdown && scope.InstanceType == "markdown") return false;
            if (_settings.ExcludeNarrative & scope.InstanceType == "Narrative") return false;

            return included;
        }


        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(MaskingNode))
                return new[] { this };
            else
                return Source.Annotations(type);
        }

        public IEnumerable<ITypedElement> Children(string name = null) =>
            Source.Children(name).Where(c => included(c)).Select(c => new MaskingNode(this, c));
    }
}
