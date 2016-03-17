using System;

namespace Hl7.Fhir.Navigation
{
    public struct StructuralHints
    {
        public bool IsComment;

        public override string ToString()
        {
            return IsComment ? "Is comment" : "(no hints)";
        }
    }


    public struct XmlRenderHints
    {
        public bool IsXhtmlDiv;

        public bool IsXmlAttribute;

        public string NestedResourceName;

        public bool HasNestedResource { get { return !String.IsNullOrEmpty(NestedResourceName); } }

        public override string ToString()
        {
            return IsXhtmlDiv ? "Is XHTML <div>" : (IsXmlAttribute ? "Is Xml attribute" : (HasNestedResource ? "Contains nested resource " + NestedResourceName : "(no hints)"));
        }
    }


}