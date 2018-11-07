/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.ElementModel;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    public class BaseFhirSerializer
    {
        public readonly SerializerSettings Settings;

        public BaseFhirSerializer(SerializerSettings settings)
        {
            Settings = settings?.Clone() ?? new SerializerSettings();
        }

        protected static ITypedElement MakeElementStack(Base instance, SummaryType summary)
        {
            if (summary == SummaryType.False) return instance.ToTypedElement();

            var patchedInstance = (Base)instance.DeepCopy();

            MetaSubsettedAdder.AddSubsetted(patchedInstance, atRoot: true);

            var baseNav = new ScopedNode(patchedInstance.ToTypedElement());

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
                default:
                    return baseNav;
            }
        }

        // This is a hack to retain the capability to automatically add a SUBSETTED metatag to an 
        // instance, even if the current IElementNavigator based serializer won't let you have that.
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

                    if (!resource.Meta.Tag.Any(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED"))
                    {
                        var subsettedTag = new Coding("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED");
                        resource.Meta.Tag.Add(subsettedTag);
                    }
                }

                foreach (var child in instance.Children)
                    AddSubsetted(child, atRoot: false);
            }
        }
    }
}
