/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class XmlNavFhirReader : IFhirReader
    {
        public const string BINARY_CONTENT_MEMBER_NAME = "content";

        public bool DisallowXsiAttributesOnRoot { get; set; }

        IElementNavigator _current;

        // [WMR 20160421] Caller can safely dispose reader after calling this ctor
        public XmlNavFhirReader(XmlReader reader, bool disallowXsiAttributesOnRoot = false)
        {
            DisallowXsiAttributesOnRoot = disallowXsiAttributesOnRoot;

            var internalReader = SerializationUtil.WrapXmlReader(reader);
            XDocument doc;

            try
            {
                doc = XDocument.Load(internalReader, LoadOptions.SetLineInfo);
            }
            catch (XmlException xec)
            {
                throw Error.Format("Cannot parse xml: " + xec.Message);
            }

            setRoot(doc);
        }

        public XmlNavFhirReader(XObject root)
        {
            setRoot(root);
        }

        private void setRoot(XObject root)
        {
            if (root is XDocument doc)
                _current = XmlDomFhirNavigator.Create(doc);
            else
                _current = XmlDomFhirNavigator.Create((XElement)root);
        }

        public XmlNavFhirReader(IElementNavigator root)
        {
            _current = root;
        }

        public string GetResourceTypeName()
        {
            if (_current.Type != null)
                return _current.Type;
            else
                throw Error.Format("Cannot get resource type name: reader not at an element", this);
        }


        private static readonly XName XHTMLDIV = XmlNs.XHTMLNS + "div";

        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            if (_current.Value != null)
                yield return Tuple.Create("value", (IFhirReader)new XmlNavFhirReader(_current));

            var children = _current.Children();
            foreach (var child in _current.Children())
                yield return Tuple.Create(child.Name, (IFhirReader)new XmlNavFhirReader(child));
        }

        public object GetPrimitiveValue()
        {
            return _current.Value;
        }


        public int LineNumber
        {
            get 
            {
                return -1;
            }
        }

        public int LinePosition
        {
            get
            {
                return -1;
            }
        }
    }
}
