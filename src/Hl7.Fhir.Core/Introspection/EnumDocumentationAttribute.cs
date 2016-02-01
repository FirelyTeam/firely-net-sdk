using System;

namespace Hl7.Fhir.Introspection
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumDocumentationAttribute : Attribute
    {
        readonly string documentation;

        // This is a positional argument
        public EnumDocumentationAttribute(string documentation)
        {
            this.documentation = documentation;
        }

        public string Documentation
        {
            get { return documentation; }
        }
    }
}
