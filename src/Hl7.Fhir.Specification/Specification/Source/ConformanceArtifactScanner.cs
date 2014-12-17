/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Hl7.Fhir.Support;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Internal class which is able to scan a (possibly) large Xml conformance resource for its identifying url
    /// </summary>
    internal class ConformanceArtifactScanner
    {
        public static bool IsConformanceResource(string name)
        {
            return name == "Profile" || name == "ExtensionDefinition" || name == "SearchParameter" ||
                    name == "OperationDefinition" || name == "ValueSet" || name == "ConceptMap" ||
                    name == "Conformance" || name == "NamingSystem" || name == "DataElement";
        }

        public static string GetIdentifyingElementName(string name)
        {
            switch(name)
            {
                case "Profile": return "url";
                case "ExtensionDefinition": return "url";
                case "SearchParameter": return "url";
                case "OperationDefinition": return "identifier";
                case "ValueSet": return "identifier";
                case "ConceptMap": return "identifier";
                case "Conformance": return "identifier";
                case "DataElement" : throw Error.NotImplemented("DataElement used Identifier datatype to represent identity. This is unsupported");
                case "NamingSystem": throw Error.NotImplemented("NamingSystem does not have an identifying element");
                default: return null;
            }
        }

        public static string GetNameElementName(string name)
        {
            if(!IsConformanceResource(name)) return null;

            if (name == "OperationDefinition") return "title";

            return "name";
        }

        private Stream _input;
        private string _origin;

        public ConformanceArtifactScanner(Stream input, string origin)
        {
            _input = input;
            _origin = origin;
        }

        /// <summary>
        /// Scan a supplied (bundle or single resource) file with the core artifacts for an entry with an id equal to the given uri 
        /// </summary>
        /// <param name="entryUri"></param>
        /// <returns></returns>
        public XElement FindConformanceResourceById(string identifier)
        {
            if (identifier == null) throw Error.ArgumentNull("identifier");

            var resources = StreamResources();

            return resources.Where(res => IsConformanceResource(res.Name.LocalName) &&
                res.Element(XmlNs.XFHIR + GetIdentifyingElementName(res.Name.LocalName))
                            .Attribute("value").Value == identifier)
                            .SingleOrDefault();
        }

        private string getPrimitiveValueElement(XElement element, string name)
        {
            var valueElem = element.Element(XmlNs.XFHIR + name);
            if(valueElem == null) return null;

            var valueAttr = valueElem.Attribute("value");

            return valueAttr != null ? valueAttr.Value : null;
        }


        public IEnumerable<ConformanceInformation> ListConformanceResourceInformation()
        {
            var resources = StreamResources();

            return resources.Where(res => IsConformanceResource(res.Name.LocalName))
                .Select(res =>
                        new ConformanceInformation()
                        {
                                Identifier = getPrimitiveValueElement(res, GetIdentifyingElementName(res.Name.LocalName)),
                                Name = getPrimitiveValueElement(res, GetNameElementName(res.Name.LocalName)),
                                Origin = _origin,
                                Type = res.Name.LocalName
                        });                       
        }

        private static string getRootName(XmlReader reader)
        {
           while (reader.Read())
           {
               if (reader.NodeType == XmlNodeType.Element && reader.NamespaceURI == XmlNs.FHIR)
                   return reader.LocalName;
           }

           return null;
        }

        // Use a forward-only XmlReader to scan through a possibly huge bundled file,
        // and yield the feed entries, so only one entry is in memory at a time
        internal IEnumerable<XElement> StreamResources()
        {
            var reader = FhirParser.XmlReaderFromStream(_input);

            var root = getRootName(reader);

            if (root == "Bundle")
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.NamespaceURI == XmlNs.FHIR && reader.LocalName == "entry")
                    {
                        var entryNode = (XElement)XElement.ReadFrom(reader);
                        yield return entryNode.Elements().First().Elements().First();
                    }
                }
            }
            else if (root != null)
            {
                var resourceNode = (XElement)XElement.ReadFrom(reader);
                yield return resourceNode;
            }
            else
                yield break;
        }


        //public static string GetIdentifierFromConformanceResource(DomainResource r)
        //{
        //    if (r is Profile)
        //        return ((Profile)r).Url;
        //    else if (r is ExtensionDefinition)
        //        return ((ExtensionDefinition)r).Url;
        //    else if (r is SearchParameter)
        //        return ((SearchParameter)r).Url;
        //    else if (r is OperationDefinition)
        //        return ((OperationDefinition)r).Identifier;
        //    else if (r is ValueSet)
        //        return ((ValueSet)r).Identifier;
        //    else if (r is ConceptMap)
        //        return ((ConceptMap)r).Identifier;
        //    else if (r is Conformance)
        //        return ((Conformance)r).Identifier;
        //    else if (r is NamingSystem)
        //        throw Error.NotImplemented("NamingSystem is not yet identifiable by any element");
        //    else
        //        return null;
        //}
    }
}
