#nullable enable

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Serialization;

[Obsolete("This class has been replaced by the equivalent FhirXmlSerializer class.")]
public class FhirXmlPocoSerializer() : BaseFhirXmlPocoSerializer(ModelInfo.ModelInspector);