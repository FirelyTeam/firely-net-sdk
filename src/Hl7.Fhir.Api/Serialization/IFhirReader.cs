using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public interface IPostitionInfo
    {
        int LineNumber { get; }
        int LinePosition { get; }
    }

    public interface IFhirReader : IPostitionInfo
    {
        string GetResourceTypeName(bool nested);

        IEnumerable<Tuple<string, IFhirReader>> GetMembers();

        IEnumerable<IFhirReader> GetArrayElements();

        object GetPrimitiveValue();

        TokenType CurrentToken { get; }
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
