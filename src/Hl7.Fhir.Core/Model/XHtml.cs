/*
  Copyright (c) 2011-2012, HL7, Inc
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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Hl7.Fhir.Serialization;


namespace Hl7.Fhir.Model
{
    public static class XHtml
    {
        public static bool IsValidValue(string value)
        {
            try
            {
                // There is currently no validation in the portable .net
                // for the XDocument validation, would need to scan for
                // another implementation to cover this
#if !PORTABLE45
                var doc = FhirParser.XDocumentFromXml(value as string);
                doc.Validate(_xhtmlSchemaSet.Value, validationEventHandler: null);
#endif

                return true;
            }
            catch
            {
                return false;
            }
        }

#if !PORTABLE45
        private static Lazy<XmlSchemaSet> _xhtmlSchemaSet = new Lazy<XmlSchemaSet>(compileXhtmlSchema, true);

        private static XmlSchemaSet compileXhtmlSchema()
        {
            var assembly = typeof(XHtml).Assembly;
            XmlSchemaSet schemas = new XmlSchemaSet();

            var schema = new StringReader(Properties.Resources.fhir_xhtml);
            schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file
            schema = new StringReader(Properties.Resources.xml);
            schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file

            schemas.Compile();

            return schemas;
        }

        /*
         * // This code prevents some exceptions that can occur during debugging that things just proceed naturally afterwards.
         * // it just interferes with debug flow when you are catching exceptions.
        private class LocalXmlResolver : XmlUrlResolver
        {
            public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
            {
                if (absoluteUri.OriginalString.EndsWith("xml.xsd"))
                    return Properties.Resources.xml;
                return base.GetEntity(absoluteUri, role, ofObjectToReturn);
            }
        }

        private static XmlSchemaSet compileXhtmlSchema()
        {
            var assembly = typeof(XHtml).Assembly;
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.XmlResolver = new LocalXmlResolver();

            var schema = new StringReader(Properties.Resources.xml);
            schemas.Add("http://www.w3.org/XML/1998/namespace", XmlReader.Create(schema));   // null = use schema namespace as specified in schema file
            schema = new StringReader(Properties.Resources.fhir_xhtml);
            schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file

            schemas.Compile();

            return schemas;
        }
         */
#endif

    }
  
}
