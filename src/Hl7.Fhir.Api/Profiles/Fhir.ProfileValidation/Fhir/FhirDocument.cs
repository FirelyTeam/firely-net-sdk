/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using Fhir.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fhir.IO
{
    public static class FhirFile
    {
        public static Validation.Profile LoadXmlFile(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            ProfileReader reader = new ProfileReader();
            return reader.Read(document);
        }

        public static void LoadXmlFile(this Validation.Profile profile, string filename)
        {
            Validation.Profile p = LoadXmlFile(filename);
            profile.Add(p);
        }

        public static Feed LoadXMLFeed(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            return new Feed(document);
        }

        public static Feed LoadXMLResource(string filename)
        {
            /*
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            return new Feed(document);
            */
            throw new NotImplementedException("Conversion in xml-reader of single resource as 1-element feed not yet implemented");
        }

        public static Validation.Profile LoadXMLValueSets(string filename)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            ProfileReader reader = new ProfileReader();
            List<Validation.ValueSet> valuesets = reader.ReadValueSets(document);
            Validation.Profile profile = new Validation.Profile();
            profile.Add(valuesets);
            return profile;
        }

        public static void LoadXMLValueSets(this Validation.Profile profile, string filename)
        {
            Validation.Profile p = LoadXMLValueSets(filename);
            profile.Add(p);
        }

    }
}
