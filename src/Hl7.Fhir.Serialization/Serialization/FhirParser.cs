/*
  Copyright (c) 2011-2013, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;


namespace Hl7.Fhir.Serialization
{
    public partial class FhirParser
    {
        public static Resource ParseResourceFromXml(string xml)
        {
            var reader = XmlReaderFromString(xml);
            return ParseResource(reader);
        }

        public static Resource ParseResourceFromJson(string json)
        {
            var reader = JsonReaderFromString(json);
            return ParseResource(reader);
        }

        public static IList<Tag> ParseTagListFromXml(string xml)
        {
            var reader = XmlReaderFromString(xml);
            return ParseTagList(reader);
        }

        public static IList<Tag> ParseTagListFromJson(string json)
        {
            var reader = JsonReaderFromString(json);
            return ParseTagList(reader);
        }

        public static Resource ParseResource(XmlReader reader)
        {
            return ParseResource(new XmlDomFhirReader(reader));
        }

        public static Resource ParseResource(JsonReader reader)
        {
            return ParseResource(new JsonDomFhirReader(reader));
        }

        public static IList<Tag> ParseTagList(XmlReader reader)
        {
            ErrorList errors = new ErrorList();
            var result = TagListParser.ParseTags(reader,errors);

            if (errors.Count > 0)
                throw  Error.Format(errors.ToString());

            return result;
        }

        public static IList<Tag> ParseTagList(JsonReader reader)
        {
            ErrorList errors = new ErrorList();
            var result = TagListParser.ParseTags(reader, errors);

            if (errors.Count > 0)
                throw Error.Format(errors.ToString());

            return result;
        }

        public static BundleEntry ParseBundleEntry(JsonReader reader)
        {
            ErrorList errors = new ErrorList();
            var result = BundleJsonParser.LoadEntry(reader,errors);

            if (errors.Count > 0)
                throw Error.Format(errors.ToString());

            return result;
        }

        public static BundleEntry ParseBundleEntry(XmlReader reader)
        {
            ErrorList errors = new ErrorList();
            var result = BundleXmlParser.LoadEntry(reader, errors);

            if (errors.Count > 0)
                throw Error.Format(errors.ToString());

            return result;
        }

        public static BundleEntry ParseBundleEntryFromJson(string json)
        {
            return ParseBundleEntry(JsonReaderFromString(json));
        }

        public static BundleEntry ParseBundleEntryFromXml(string xml)
        {
            return ParseBundleEntry(XmlReaderFromString(xml));
        }

        public static Bundle ParseBundle(JsonReader reader)
        {
            ErrorList errors = new ErrorList();
            var result = BundleJsonParser.Load(reader, errors);

            if (errors.Count > 0)
                throw Error.Format(errors.ToString());

            return result;
        }

        public static Bundle ParseBundleFromJson(string json)
        {
            return ParseBundle(JsonReaderFromString(json));
        }

        public static Bundle ParseBundle(XmlReader reader)
        {
            ErrorList errors = new ErrorList();
            var result = BundleXmlParser.Load(reader, errors);

            if (errors.Count > 0)
                throw Error.Format(errors.ToString());

            return result;
        }

        public static Bundle ParseBundleFromXml(string xml)
        {
            return ParseBundle(XmlReaderFromString(xml));
        }

        internal static Resource ParseResource(IFhirReader reader)
        {
            return new ResourceReader(reader).Deserialize();
        }

        internal static XmlReader XmlReaderFromString(string xml)
        {
            return XmlReader.Create(new StringReader(xml));
        }

        internal static JsonTextReader JsonReaderFromString(string json)
        {
            return new JsonTextReader(new StringReader(json));
        }
    }
}
