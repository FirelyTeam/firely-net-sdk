/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Hl7.Fhir.Utility;
using Hl7.Fhir.ElementModel;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// This class wraps an IElementNavigator to implement IFhirReader. This is a temporary solution to use IElementNavigator
    /// with the POCO-parsers.
    /// </summary>
#pragma warning disable 612, 618
    internal struct ElementNavFhirReader : IFhirReader, IElementNavigator
#pragma warning restore 612,618
    {
        private IElementNavigator _current;

        public bool DisallowXsiAttributesOnRoot { get; set; }

        public ElementNavFhirReader(IElementNavigator root, bool disallowXsiAttributesOnRoot = false)
        {
            DisallowXsiAttributesOnRoot = disallowXsiAttributesOnRoot;
            _current = root;
        }

        public object GetPrimitiveValue() => Value;

        public string GetResourceTypeName()
        {
            if (Type != null) return Type;

            // No type name on this element....give the users some details about why?
            var xmlDetails = getXmlDetails(_current);
            if (xmlDetails != null)
            {
                throw Error.Format($"Cannot derive type name from element with name '{xmlDetails.Name.LocalName}' and namespace '{xmlDetails.Name.NamespaceName}'", this);
            }
            else
            {
                throw Error.Format("Cannot determine type of resource to create from json input data. " +
                                    $"Is there a '{JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME}' member present? ", this);
            }

        }


        private static XmlSerializationDetails getXmlDetails(IElementNavigator nav)
        {
            if (nav is IAnnotated ia)
                return ia.Annotation<XmlSerializationDetails>();
            else
                return null;
        }

        private static JsonSerializationDetails getJsonDetails(IElementNavigator nav)
        {
            if (nav is IAnnotated ia)
                return ia.Annotation<JsonSerializationDetails>();
            else
                return null;
        }

#pragma warning disable 612, 618
        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            if (Value != null)
                yield return Tuple.Create("value", (IFhirReader)new ElementNavFhirReader(_current));

            foreach (var child in _current.Children())
            {
                bool mustSkip = verifyXmlSpecificDetails(child);

                if(!mustSkip)
                    yield return Tuple.Create(child.Name, (IFhirReader)new ElementNavFhirReader(child));
            }
        }
#pragma warning restore 612, 618

        /// <summary>
        /// Verify xml specific details.
        /// </summary>
        /// <param name="child"></param>
        /// <returns>'true' if the child is ok, but needs to be skipped, 'false' if it is ok and needs to be processed, throws otherwise</returns>
        private bool verifyXmlSpecificDetails(IElementNavigator child)
        {
            var xmlDetails = getXmlDetails(child);
            if (xmlDetails == null) return false;

            if (xmlDetails.NodeType == System.Xml.XmlNodeType.Attribute)
            {
                if (xmlDetails.IsNamespaceDeclaration) return true;      // skip xmlns declarations
                if (xmlDetails.Name == XName.Get("{http://www.w3.org/2000/xmlns/}xsi") && !DisallowXsiAttributesOnRoot) return true; // skip xmlns:xsi declaration
                if (xmlDetails.Name == XName.Get("{http://www.w3.org/2001/XMLSchema-instance}schemaLocation") && !DisallowXsiAttributesOnRoot) return true;     // skip schemaLocation

                if (xmlDetails.Name.NamespaceName == "") return false;

                throw Error.Format($"Encountered unsupported attribute {xmlDetails.Name}", this);
            }

            else if (xmlDetails.NodeType == System.Xml.XmlNodeType.Element)
            {
                if (xmlDetails.Name.NamespaceName == XmlNs.FHIR) return false;
                if (xmlDetails.Name == XmlNs.XHTMLDIV) return false;

                throw Error.Format($"Encountered element '{xmlDetails.Name.LocalName}' from unsupported namespace '{xmlDetails.Name.NamespaceName}'", this);
            }

            else
                throw Error.Format($"Xml node of type '{xmlDetails.NodeType}' is unexpected at this point", this);
        }

        #region IElementNavigator members
        public bool MoveToNext(string nameFilter = null) => _current.MoveToNext(nameFilter);

        public bool MoveToFirstChild(string nameFilter = null) => _current.MoveToFirstChild(nameFilter);

        public IElementNavigator Clone() => new ElementNavFhirReader(_current.Clone(), DisallowXsiAttributesOnRoot);

        public int LineNumber => (_current as IPositionInfo)?.LineNumber ?? -1;

        public int LinePosition => (_current as IPositionInfo)?.LinePosition ?? -1;

        public string Name => _current.Name;

        public string Type => _current.Type;

        public object Value => _current.Value;

        public string Location => _current.Location;
        #endregion
    }
}
