/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
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

        /// <summary>
        /// Set to true when a complex type property is mandatory so all its children need to be included
        /// </summary>
        private bool _includeAll { get; set; }

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
            ForElements(node, _elements, false);

        public static MaskingNode ForElements(ITypedElement node, string[] _elements, bool includeMandatory) =>
            new MaskingNode(node, new MaskingNodeSettings
            {
                IncludeElements = _elements ?? new string[] { },
                IncludeMandatory = includeMandatory,
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
                throw Error.Argument("MaskingNode can only be used on a navigator chain that contains a ScopedNode", nameof(source));

            Source = source;
            _settings = settings?.Clone() ?? new MaskingNodeSettings();

            if (Source is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private MaskingNode(MaskingNode parent, ITypedElement source, bool includeAll)
        {
            Source = source;
            _settings = parent._settings;
            _includeAll = includeAll;
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

        private (bool included, bool mandatory) included(ITypedElement node)
        {
            var scope = getScope(node);

            // Trivially, we will include the root
            if (!scope.Location.Contains(".")) return (true, false);

            bool atRootBundle() => atBundle() && scope.ParentResource == null;
            bool atBundle() => scope.NearestResourceType == "Bundle";

            switch (_settings.PreserveBundle)
            {
                case MaskingNodeSettings.PreserveBundleMode.All when atBundle():
                case MaskingNodeSettings.PreserveBundleMode.Root when atRootBundle():
                    return (true, false);

                    // fall through...
            }

            var included = _settings.IncludeAll || _includeAll;

            bool mandatory = false;         // included because it's required & includeMandatory is on
            var ed = scope.Definition;
            if (ed != null)
            {
                mandatory = _settings.IncludeMandatory && ed.IsRequired;
                included |= mandatory;
                included |= _settings.IncludeInSummary && (ed.InSummary || ed.IsModifier);
                // Also include Element.id in the summary. Not a nice way to determine this, but for now the only way. 
                included |= _settings.IncludeInSummary && ed.ElementName == "id" && scope.InstanceType == "string";
            }

            var loc = scope.LocalLocation;
            var nearest = scope.NearestResourceType;
            included |= _settings.IncludeElements?.Any(matches) ?? false;

            if (_settings.ExcludeElements?.Any(matches) == true)
                return (false, false);

            bool matches(string filter)
            {
                var f = nearest + "." + filter;
                return loc == f || loc.StartsWith(f + ".") || loc.StartsWith(f + "[");    // include matches + children
            }

            if (_settings.ExcludeMarkdown && scope.InstanceType == "markdown") return (false, false);
            if (_settings.ExcludeNarrative & scope.InstanceType == "Narrative") return (false, false);

            return (included, mandatory);
        }


        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(MaskingNode))
                return new[] { this };
            else
                return Source.Annotations(type);
        }

        public IEnumerable<ITypedElement> Children(string name = null) =>
            from c in Source.Children(name)
            let inc = included(c)
            where inc.included
            select new MaskingNode(this, c, inc.mandatory);
    }
}
