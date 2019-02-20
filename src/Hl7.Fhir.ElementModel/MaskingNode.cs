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
    public class MaskingNode : ITypedElement, IAnnotated, IExceptionSource
    {
        public static MaskingNode ForSummary(ITypedElement node) =>
            new MaskingNode(node, new MaskingNodeSettings
            {
                IncludeInSummary = true,
                PreserveBundle = MaskingNodeSettings.PreserveBundleMode.Root
            });

        public static MaskingNode ForText(ITypedElement node) =>
            new MaskingNode(node, new MaskingNodeSettings
            {
                IncludeElements = new[] { "text", "id", "meta" },
                IncludeMandatory = true, //IncludeIsModifier = true,
                PreserveBundle = MaskingNodeSettings.PreserveBundleMode.All
            });

        public static MaskingNode ForElements(ITypedElement node, string[] _elements) =>
            new MaskingNode(node, new MaskingNodeSettings
            {
                IncludeElements = _elements ?? new string[] { },
                PreserveBundle = MaskingNodeSettings.PreserveBundleMode.All
            });

        public static MaskingNode ForData(ITypedElement node) =>
            new MaskingNode(node, new MaskingNodeSettings
            {
                IncludeAll = true,
                ExcludeNarrative = true
            });

        public static MaskingNode ForCount(ITypedElement node) =>
          new MaskingNode(node, new MaskingNodeSettings
          {
              IncludeMandatory = true,
              IncludeElements = new[] { "id", "total" },
          });

        public MaskingNode(ITypedElement source, MaskingNodeSettings settings = null)
        {
            if (source == null) throw Error.ArgumentNull(nameof(source));
            if (source.Annotation<ScopedNode>() == null)
                throw Error.Argument("MaskingNavigator can only be used on a navigator chain that contains a ScopedNavigator", nameof(source));

            Source = source;
            _settings = settings?.Clone() ?? new MaskingNodeSettings();

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

        private readonly MaskingNodeSettings _settings;

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
                case MaskingNodeSettings.PreserveBundleMode.All when atBundle():
                case MaskingNodeSettings.PreserveBundleMode.Root when atRootBundle():
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
