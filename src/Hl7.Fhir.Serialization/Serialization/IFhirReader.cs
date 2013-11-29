using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public interface IFhirReader
    {
        string GetResourceTypeName(bool nested);

        IEnumerable<Tuple<string, IFhirReader>> GetMembers();

        IEnumerable<IFhirReader> GetArrayElements();

        object GetPrimitiveValue();

        TokenType CurrentToken { get; }

        int LineNumber { get; }
        int LinePosition { get; }
    }

    public enum TokenType
    {
        Object,
        Array,
        String,
        Boolean,
        Number,
        Null,
    }

    public static class FhirReaderExtensions
    {
        public static bool IsPrimitive(this IFhirReader reader)
        {
            return reader.CurrentToken == TokenType.Boolean ||
                    reader.CurrentToken == TokenType.Number ||
                    reader.CurrentToken == TokenType.String;
        }
    }
}
