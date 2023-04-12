using System;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    public class FhirGenericSerializer : BaseFhirSerializer
    {
        public FhirGenericSerializer(Model.Version version) : base(version)
        {
        }

        public FhirGenericSerializer(SerializerSettings settings) : base(settings)
        {
        }

        public void Serialize(Base instance, ISerializerTarget target, Rest.SummaryType summary = Rest.SummaryType.False, string[] elements = null)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (target == null) throw new ArgumentNullException(nameof(target));

            var serializerSink = new GenericSerializerSink(target, Settings.Version, summary, elements);
            instance.Serialize(serializerSink);
        }
    }
}
