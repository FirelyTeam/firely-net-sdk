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
        public static string GetIdentifyingElementNameForConformanceResource(string name)
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
                case "NamingSystem": throw Error.NotImplemented("NamingSystem does not have an identifying element");
                default: return null;
            }
        }

        // Profile: url (uri)
        // ExtensionDefinition: url (uri)
        // SearchParameter: url (uri)
        // OperationDefinition: identifier (uri)
        // ValueSet: identifier (uri)
        // ConceptMap: identifier (string)
        // Conformance: identifier (string)
        // NamingSystem: ??
        public static readonly XName ENTRY_ID = XmlNs.XATOM + "id";        

        private XmlReader _input;

        public ConformanceArtifactScanner(XmlReader reader)
        {
            _input = reader;
        }

        public ConformanceArtifactScanner(Stream reader)
        {
            _input = FhirParser.XmlReaderFromStream(reader);
        }

        /// <summary>
        /// Scan a supplied bundle (atom) file with the core artifacts for an entry with an id equal to the given uri 
        /// </summary>
        /// <param name="entryUri"></param>
        /// <returns></returns>
        public string FindArtifactIdentifier()
        {
            var parser = new FhirParser();

            try
            {
                var resource = parser.ParseResource(_input);

                if (resource is DomainResource)
                    return GetIdentifierFromConformanceResource((DomainResource)resource);
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }


        public static string GetIdentifierFromConformanceResource(DomainResource r)
        {
            if (r is Profile)
                return ((Profile)r).Url;
            else if (r is ExtensionDefinition)
                return ((ExtensionDefinition)r).Url;
            else if (r is SearchParameter)
                return ((SearchParameter)r).Url;
            else if (r is OperationDefinition)
                return ((OperationDefinition)r).Identifier;
            else if (r is ValueSet)
                return ((ValueSet)r).Identifier;
            else if (r is ConceptMap)
                return ((ConceptMap)r).Identifier;
            else if (r is Conformance)
                return ((Conformance)r).Identifier;
            else if (r is NamingSystem)
                throw Error.NotImplemented("NamingSystem is not yet identifiable by any element");
            else
                return null;
        }
    }
}
