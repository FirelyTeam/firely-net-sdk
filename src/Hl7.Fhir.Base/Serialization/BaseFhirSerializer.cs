/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    public class BaseFhirSerializer
    {
        public readonly SerializerSettings Settings;
        private readonly ModelInspector _modelInspector;
        private readonly Coding[] _subsettedTags =
            new[]
            {
                new Coding("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED"), // STU3 Tag
                new Coding("http://terminology.hl7.org/CodeSystem/v3-ObservationValue", "SUBSETTED"), // Tag from R4 and higher
            };

        public BaseFhirSerializer(ModelInspector modelInspector, SerializerSettings? settings = null)
        {
            Settings = settings?.Clone() ?? new SerializerSettings();
            _modelInspector = modelInspector;
        }

        protected ITypedElement MakeElementStack(Base instance, SummaryType summary, string[]? elements)
            => MakeElementStack(instance, summary, elements, false);

        protected ITypedElement MakeElementStack(Base instance, SummaryType summary, string[]? elements, bool includeMandatoryInElementsSummary)
        {
            if (summary == SummaryType.False && elements == null) return instance.ToTypedElement(_modelInspector);

            if (elements is not null && summary != SummaryType.False)
                throw Error.Argument("elements", "Elements parameter is supported only when summary is SummaryType.False or summary is not specified at all.");

            var patchedInstance = (Base)instance.DeepCopy();

            addSubsetted(patchedInstance, atRoot: true);

            var baseNav = new ScopedNode(patchedInstance.ToTypedElement(_modelInspector));

            return summary switch
            {
                SummaryType.True => MaskingNode.ForSummary(baseNav),
                SummaryType.Text => MaskingNode.ForText(baseNav),
                SummaryType.Data => MaskingNode.ForData(baseNav),
                SummaryType.Count => MaskingNode.ForCount(baseNav),
                SummaryType.False => MaskingNode.ForElements(baseNav, elements, includeMandatoryInElementsSummary),
                _ => baseNav,
            };
        }

        // This is a hack to retain the capability to automatically add a SUBSETTED metatag to an 
        // instance, even if the current ITypedElement based serializer won't let you have that.
        // I am not convinced it's the responsibility of the serializer (it's an outside policy), so
        // it's just here to not break existing logic of the POCO serializers.
        private void addSubsetted(Base instance, bool atRoot)
        {
            var isBundleAtRoot = instance is Bundle && atRoot;

            if (instance is Resource resource && !isBundleAtRoot)
            {
                resource.Meta ??= new Meta();

                foreach (var item in _subsettedTags)
                {
                    if (!resource.Meta.Tag.Any(t => t.System == item.System && t.Code == item.Code))
                    {
                        resource.Meta.Tag.Add((Coding)item.DeepCopy());
                    }
                }
            }

            foreach (var child in instance.Children)
                addSubsetted(child, atRoot: false);
        }
    }
}
#nullable restore