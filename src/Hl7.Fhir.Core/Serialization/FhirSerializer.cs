/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using Hl7.Fhir.Model;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Hl7.Fhir.ElementModel;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    public class BaseFhirSerializer
    {
        public ParserSettings Settings { get; private set; }

        public BaseFhirSerializer(ParserSettings settings)
        {
            Settings = settings ?? throw Error.ArgumentNull(nameof(settings));
        }

        public BaseFhirSerializer()
        {
            Settings = new ParserSettings();
        }

        protected static IElementNavigator makeNav(Base instance, SummaryType summary)
        {
            if (summary == SummaryType.False) return instance.ToElementNavigator();

            var baseNav = new ScopedNavigator(instance.ToElementNavigator());

            switch (summary)
            {
                case SummaryType.True:
                    return MaskingNavigator.ForSummary(baseNav);
                case SummaryType.Text:
                    return MaskingNavigator.ForText(baseNav);
                case SummaryType.Data:
                    return MaskingNavigator.ForData(baseNav);
                case SummaryType.Count:
                    return MaskingNavigator.ForCount(baseNav);
                default:
                    return baseNav;
            }
        }
    }

    public class FhirXmlSerializer : BaseFhirSerializer
    {
        public FhirXmlSerializer() : base()
        {
        }

        public FhirXmlSerializer(ParserSettings settings) : base(settings)
        {
        }

        public string SerializeToString(Base instance, SummaryType summary = SummaryType.False, string root = null)
        {
            using (new MetaSubsettedAdder(instance, summary != SummaryType.False))
                return makeNav(instance, summary).ToXml(rootName: root);
        }


        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False, string root = null)
        {
            using (new MetaSubsettedAdder(instance, summary != SummaryType.False))
                return makeNav(instance, summary).ToXmlBytes(rootName: root);
        }

#if NET45
        // [WMR 20180409] NEW
        // https://github.com/ewoutkramer/fhir-net-api/issues/545
        public XDocument SerializeToDocument(Base instance, SummaryType summary = SummaryType.False, string root = null)
        {
            using (new MetaSubsettedAdder(instance, summary != SummaryType.False))
            {
                var nav = makeNav(instance, summary);


                return SerializationUtil.WriteXmlToDocument(w =>
                {
                    var fhirWriter = new FhirXmlWriter();
                    fhirWriter.Write(nav, w, root);
                });
            }
        }
#endif

        // [WMR 20160421] Caller is responsible for disposing writer
        //public void Serialize(Base instance, XmlWriter writer, SummaryType summary = SummaryType.False) => Serialize(instance, new XmlFhirWriter(writer), summary);
    }

    public class FhirJsonSerializer : BaseFhirSerializer
    {
        public FhirJsonSerializer() : base()
        {
        }

        public FhirJsonSerializer(ParserSettings settings) : base(settings)
        {
        }

        public string SerializeToString(Base instance, SummaryType summary = SummaryType.False)
        {
            using (new MetaSubsettedAdder(instance, summary != SummaryType.False))
                return makeNav(instance, summary).ToJson();
        }

        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False)
        {
            using (new MetaSubsettedAdder(instance, summary != SummaryType.False))
                return makeNav(instance, summary).ToJsonBytes();
        }
    }

    //[Obsolete("Obsolete. Instead, create a new FhirXmlSerializer or FhirJsonSerializer instance and call one of the serialization methods.")]
    //public static class FhirSerializer
    //{
    //    private static FhirXmlSerializer _xmlSerializer = new FhirXmlSerializer();
    //    private static FhirJsonSerializer _jsonSerializer = new FhirJsonSerializer();

    //    [Obsolete("Create a new FhirXmlSerializer and call SerializeToString()")]
    //    public static string SerializeResourceToXml(Resource resource, SummaryType summary = SummaryType.False) => _xmlSerializer.SerializeToString(resource, summary);

    //    [Obsolete("Create a new FhirXmlSerializer and call SerializeToString()")]
    //    public static string SerializeToXml(Base data, SummaryType summary = SummaryType.False, string root = null) => _xmlSerializer.SerializeToString(data, summary, root);

    //    [Obsolete("Create a new FhirXmlSerializer and call SerializeToBytes()")]
    //    public static byte[] SerializeResourceToXmlBytes(Resource resource, SummaryType summary = SummaryType.False) => _xmlSerializer.SerializeToBytes(resource, summary);

    //    [Obsolete("Create a new FhirXmlSerializer and call SerializeToBytes()")]
    //    public static byte[] SerializeToXmlBytes(Base instance, SummaryType summary = SummaryType.False, string root = null) => _xmlSerializer.SerializeToBytes(instance, summary, root);

    //    [Obsolete("Create a new FhirXmlSerializer and call Serialize()")]
    //    public static void SerializeResource(Resource resource, XmlWriter writer, SummaryType summary = SummaryType.False) => _xmlSerializer.Serialize(resource, writer, summary);

    //    [Obsolete("Create a new FhirJsonSerializer and call SerializeToString()")]
    //    public static string SerializeResourceToJson(Resource resource, SummaryType summary = SummaryType.False) => _jsonSerializer.SerializeToString(resource, summary);

    //    [Obsolete("Create a new FhirJsonSerializer and call SerializeToString()")]
    //    public static string SerializeToJson(Base instance, SummaryType summary = SummaryType.False) => _jsonSerializer.SerializeToString(instance, summary);

    //    [Obsolete("Create a new FhirJsonSerializer and call SerializeToBytes()")]
    //    public static byte[] SerializeResourceToJsonBytes(Resource resource, SummaryType summary = SummaryType.False) => _jsonSerializer.SerializeToBytes(resource, summary);

    //    [Obsolete("Create a new FhirJsonSerializer and call SerializeToBytes()")]
    //    public static byte[] SerializeToJsonBytes(Base instance, SummaryType summary = SummaryType.False) => _jsonSerializer.SerializeToBytes(instance, summary);

    //    [Obsolete("Create a new FhirJsonSerializer and call Serialize()")]
    //    public static void SerializeResource(Resource resource, JsonWriter writer, SummaryType summary = SummaryType.False) => 
    //        _jsonSerializer.Serialize(resource, writer, summary);
    //}

    // This is a hack to retain the capability to automatically add a SUBSETTED metatag to an 
    // instance, even if the current IElementNavigator based serializer won't let you have that.
    // I am not convinced it's the responsibility of the serializer (it's an outside policy), so
    // it's just here to not break existing logic of the POCO serializers.
    internal class MetaSubsettedAdder : IDisposable
    {
        private readonly Coding subsettedTag = null;
        private readonly bool createdMetaElement = false;
        private readonly Base instance = null;
        private readonly bool addSubsetted = false;

        public MetaSubsettedAdder(Base i, bool addSubsetted)
        {
            instance = i;
            this.addSubsetted = addSubsetted;

            if (instance is Resource resource && !(instance is Bundle) && addSubsetted)
            {
                if (resource.Meta == null)
                {
                    resource.Meta = new Meta();
                    createdMetaElement = true;
                }

                if (!resource.Meta.Tag.Any(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED"))
                {
                    subsettedTag = new Coding("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED");
                    resource.Meta.Tag.Add(subsettedTag);
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && instance is Resource resource)
                {
                    if (subsettedTag != null)
                        resource.Meta.Tag.Remove(subsettedTag);

                    if (createdMetaElement)
                        resource.Meta = null; // remove the meta element again.
                }

                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}
