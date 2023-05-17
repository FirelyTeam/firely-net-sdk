#nullable enable
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public partial class RoundTripNewSerializers
    {
        private readonly string _attachmentJson = "{\"size\":\"12\"}";

        private static IEnumerable<object[]> attachmentSource()
        {
            yield return new object[] { "{\"size\":\"12\", \"title\": \"Correct Attachment\"}", 12L, null! };
            yield return new object[] { "{\"size\":12, \"title\": \"An incorrect Attachment\"}", null!, ERR.LONG_INCORRECT_FORMAT_CODE };
            yield return new object[] { "{\"size\":25.345, \"title\": \"An incorrect Attachment\"}", null!, ERR.NUMBER_CANNOT_BE_PARSED_CODE };
            yield return new object[] { "{\"size\":\"12.345\", \"title\": \"An incorrect Attachment\"}", null!, ERR.LONG_CANNOT_BE_PARSED_CODE };
        }
    }
}
#nullable restore