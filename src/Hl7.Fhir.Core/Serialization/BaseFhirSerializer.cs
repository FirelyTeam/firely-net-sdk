/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

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

        public BaseFhirSerializer(ModelInspector modelInspector, SerializerSettings settings = null)
        {
            Settings = settings?.Clone() ?? new SerializerSettings();
            _modelInspector = modelInspector;
        }

        protected ITypedElement MakeElementStack(Base instance, SummaryType summary, string[] elements)
            => MakeElementStack(instance, summary, elements, false);

        protected ITypedElement MakeElementStack(Base instance, SummaryType summary, string[] elements, bool includeMandatoryInElementsSummary)
        {
            if (summary == SummaryType.False && elements == null) return instance.ToTypedElement(_modelInspector);

            if (elements != null && summary != SummaryType.False)
                throw Error.Argument("elements", "Elements parameter is supported only when summary is SummaryType.False or summary is not specified at all.");

            var patchedInstance = (Base)instance.DeepCopy();

            MetaSubsettedAdder.AddSubsetted(patchedInstance, atRoot: true);

            var baseNav = new ScopedNode(patchedInstance.ToTypedElement(_modelInspector));

            switch (summary)
            {
                case SummaryType.True:
                    return MaskingNode.ForSummary(baseNav);
                case SummaryType.Text:
                    return MaskingNode.ForText(baseNav);
                case SummaryType.Data:
                    return MaskingNode.ForData(baseNav);
                case SummaryType.Count:
                    return MaskingNode.ForCount(baseNav);
                case SummaryType.False:
                    return MaskingNode.ForElements(baseNav, elements, includeMandatoryInElementsSummary);
                default:
                    return baseNav;
            }
        }

        // This is a hack to retain the capability to automatically add a SUBSETTED metatag to an 
        // instance, even if the current ITypedElement based serializer won't let you have that.
        // I am not convinced it's the responsibility of the serializer (it's an outside policy), so
        // it's just here to not break existing logic of the POCO serializers.
        private class MetaSubsettedAdder
        {
            public static void AddSubsetted(Base instance, bool atRoot)
            {
                var isBundleAtRoot = instance is Bundle && atRoot;

                if (instance is Resource resource && !isBundleAtRoot)
                {
                    if (resource.Meta == null)
                    {
                        resource.Meta = new Meta();
                    }

                    if (!resource.Meta.Tag.Any(t => t.System == "http://terminology.hl7.org/CodeSystem/v3-ObservationValue" && t.Code == "SUBSETTED"))
                    {
                        var subsettedTag = new Coding("http://terminology.hl7.org/CodeSystem/v3-ObservationValue", "SUBSETTED");
                        resource.Meta.Tag.Add(subsettedTag);
                    }
                }

                foreach (var child in instance.Children)
                    AddSubsetted(child, atRoot: false);
            }
        }
    }
}
