/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using Hl7.Fhir.Profiling;
using Hl7.Fhir.Specification.Model;

namespace Hl7.Fhir.Specification.IO
{
    internal static class FhirFile
    {
        public static List<Structure> LoadStructuresFromXmlFile(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            SpecificationReader reader = new SpecificationReader();
            return reader.ReadProfiles(document);
        }

        public static void LoadXmlFile(this SpecificationBuilder builder, string filename)
        {
            List<Structure> structures = LoadStructuresFromXmlFile(filename);
            builder.Add(structures);
        }

        public static Feed LoadXMLFeed(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            return new Feed(document);
        }

        public static void LoadXMLValueSets(this SpecificationBuilder builder, string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            SpecificationReader reader = new SpecificationReader();
            List<ValueSet> valuesets = reader.ReadValueSets(document);
            builder.Add(valuesets);
        }

    }
}
