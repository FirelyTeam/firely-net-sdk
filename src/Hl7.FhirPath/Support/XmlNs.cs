using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Furore.Support
{
    public static class XmlNs
    {
        public const string FHIR = "http://hl7.org/fhir";
        public const string FHIR_URL_SEARCHPARAM = FHIR + "/query";
        public const string FHIRTAG = FHIR + "/tag";
        public const string TAG_PROFILE = XmlNs.FHIRTAG + "/profile";
        public const string TAG_SECURITY = XmlNs.FHIRTAG + "/security";
        public const string ATOM_CATEGORY_RESOURCETYPE = FHIR + "/resource-types";

        public const string XHTML = "http://www.w3.org/1999/xhtml";
        public const string ATOMPUB_TOMBSTONES = "http://purl.org/atompub/tombstones/1.0";
        public const string ATOM = "http://www.w3.org/2005/Atom";
        public const string OPENSEARCH = "http://a9.com/-/spec/opensearch/1.1/";
        public const string NAMESPACE = "http://www.w3.org/XML/1998/namespace";


        public static readonly XNamespace XATOM = ATOM;
        public static readonly XNamespace XATOMPUB_TOMBSTONES = ATOMPUB_TOMBSTONES;
        public static readonly XNamespace XFHIR = FHIR;
        public static readonly XNamespace XOPENSEARCH = OPENSEARCH;
        public static readonly XNamespace XHTMLNS = XHTML;

        public static readonly XName XHTMLDIV = XmlNs.XHTMLNS + "div";
    }
}
