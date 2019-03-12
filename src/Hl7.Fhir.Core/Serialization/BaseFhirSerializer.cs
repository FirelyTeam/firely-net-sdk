/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.ElementModel;
using System.Linq;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization
{
    public class BaseFhirSerializer
    {
        public readonly SerializerSettings Settings;

        public BaseFhirSerializer(Model.Version version)
        {
            Settings = new SerializerSettings(version);
        }

        public BaseFhirSerializer(SerializerSettings settings)
        {
            if (settings == null) throw Error.ArgumentNull(nameof(settings));

            Settings = settings.Clone();
        }

        protected static ITypedElement MakeElementStack(Model.Version version, Base instance, SummaryType summary, string[] elements)
        {
            if (summary == SummaryType.False && elements == null) return instance.ToTypedElement(version);

            if (elements != null && summary != SummaryType.False)
                throw Error.Argument("elements", "Elements parameter is supported only when summary is SummaryType.False or summary is not specified at all.");

            var patchedInstance = (Base)instance.DeepCopy();

            MetaSubsettedAdder.AddSubsetted(patchedInstance, atRoot: true);

            var baseNav = new ScopedNode(patchedInstance.ToTypedElement(version));

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
                    return MaskingNode.ForElements(baseNav, elements);
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
                var isBundleAtRoot = instance is IBundle && atRoot;

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
