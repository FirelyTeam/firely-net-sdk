#nullable enable

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Serialization;

[Obsolete("This class has been replaced by the equivalent FhirJsonSerializer class.")]
public class FhirJsonPocoSerializer() : BaseFhirJsonPocoSerializer(ModelInfo.ModelInspector);