/*
* Copyright (c) 2014, Firely (info@fire.ly) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Hl7.Fhir.Utility;

#pragma warning disable 1591 // suppress XML summary warnings

using System.Xml.XPath;
using Hl7.Fhir.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.XPath
{
    public class JsonXPathNavigator : XPathNavigator, IXPathNavigable
    {
        public const string FHIR_PREFIX = "f";
        public const string XHTML_PREFIX = "html";
        public const string XML_PREFIX = "xml";

        public const string SPEC_CHILD_ID = "id";
        public const string SPEC_CHILD_URL = "url";
        public const string SPEC_CHILD_CONTENTTYPE = "contentType";
        public const string SPEC_CHILD_CONTENT = "content";
        private const string SPEC_CHILD_VALUE = "value";

        private const string ROOT_PROP_NAME = "(root)";


        private readonly NameTable _nameTable = new NameTable();

        private readonly Stack<NavigatorState> _state = new Stack<NavigatorState>();

        private NavigatorState position { get { return _state.Peek(); } }

        public JsonXPathNavigator(JsonReader reader)
        {
            reader.DateParseHandling = DateParseHandling.None;
            reader.FloatParseHandling = FloatParseHandling.Decimal;

            try
            {
                var docChild = (JObject)JObject.Load(reader);

                // Add symbolic 'root' child, so initially, we are at a virtual "root" element, just like in the DOM model
                var root = new JProperty(ROOT_PROP_NAME, docChild);
                _state.Push(new NavigatorState(root));
            }
            catch (Exception e)
            {
                throw new FormatException("Cannot parse json: " + e.Message);
            }

            _nameTable.Add(XmlNs.FHIR);
            _nameTable.Add(XmlNs.XHTML);
            _nameTable.Add(XmlNs.NAMESPACE);
            _nameTable.Add(String.Empty);
            _nameTable.Add(FHIR_PREFIX);
            _nameTable.Add(XML_PREFIX);
            _nameTable.Add(XHTML_PREFIX);
            _nameTable.Add(SPEC_CHILD_VALUE);
        }


        public JsonXPathNavigator(JsonXPathNavigator source)
        {
            copyState(source._state);

            _nameTable = source._nameTable;
        }

        private IEnumerable<JProperty> elementChildren()
        {
            return position.Children.Where(c => !isAttribute(c));
        }

        private IEnumerable<JProperty> attributeChildren()
        {
            return position.Children.Where(c => isAttribute(c));
        }

        private Lazy<List<Tuple<string,string>>> _namespaces = new Lazy<List<Tuple<string,string>>>( () =>
            new List<Tuple<string,string>>() { 
                Tuple.Create(FHIR_PREFIX,XmlNs.FHIR),
                Tuple.Create(XHTML_PREFIX,XmlNs.XHTML),
        //        Tuple.Create(XML_PREFIX,XML_NS),
            });
       

        private IEnumerable<Tuple<string,string>> namespaceChildren()
        {
            //switch (scope)
            //{
            //    case XPathNamespaceScope.All:
            //        return _namespaces.Value;
            //    case XPathNamespaceScope.ExcludeXml:
            //        return _namespaces.Value.Where(ns => ns.Item1 != "xml");
            //    case XPathNamespaceScope.Local:
            //        if (position.Element.Name == "Patient")
            //            return _namespaces.Value;
            //        else
            //            return new List<Tuple<string, string>>();
            //    default:
            //        break;
            //}


            return _namespaces.Value;
        }

        private bool isAttribute(JProperty property)
        {
            return property.IsValueProperty() ||
                property.Name == SPEC_CHILD_ID ||               // id attr that all types can have
                (property.Name == SPEC_CHILD_URL && position.Element.Name == "extension") ||     // url attr of extension
                (property.Name == SPEC_CHILD_URL && position.Element.Name == "modifierExtension") ||     // url attr of modifierExtension parent
                (property.Name == SPEC_CHILD_CONTENTTYPE && position.Element.Name == "Binary");     // contentType attr of Binary resource
        }

        private bool isTextNode(JProperty prop)
        {
            return prop.Name == SPEC_CHILD_CONTENT; // && position.Element.Name == "Binary";
        }

        private void copyState(IEnumerable<NavigatorState> other)
        {
            _state.Clear();

            foreach (var state in other.Reverse())
            {
                _state.Push(state.Copy());
            }
        }

        public override string BaseURI
        {            
            get { return _nameTable.Get(String.Empty); }
        }

        public override XPathNavigator Clone()
        {
            return new JsonXPathNavigator(this);
        }
     
        public override bool IsSamePosition(XPathNavigator nav)
        {
            var other = nav as JsonXPathNavigator;

            if (other == null) return false;

            return _state.SequenceEqual(other._state);
        }

        public override bool MoveTo(XPathNavigator other)
        {
            var xpn = other as JsonXPathNavigator;

            if (xpn != null)
            {
                copyState(xpn._state);
                return true;
            }
            else
                return false;
        }

        public override bool MoveToFirstChild()
        {          
            if (NodeType == XPathNodeType.Element || NodeType == XPathNodeType.Root)
            {
                return moveToElementChild(0);
            }
            else
                return false;
        }

        public override bool MoveToNext()
        {
            if (NodeType == XPathNodeType.Element || NodeType == XPathNodeType.Text)
                return tryMoveToSibling(1);
            else
                return false;
        }

        public override bool MoveToPrevious()
        {
            if (NodeType == XPathNodeType.Element || NodeType == XPathNodeType.Text)
                return tryMoveToSibling(-1);
            else
                return false;
        }

        public override bool MoveToParent()
        {
            switch(NodeType)
            {
                case XPathNodeType.Root:
                    return false;
                case XPathNodeType.Element:
                case XPathNodeType.Text:
                    _state.Pop();
                    return true;
                case XPathNodeType.Attribute:
                    position.AttributePos = null;
                    return true;
                case XPathNodeType.Namespace:
                    position.NamespacePos = null;
                    return true;
                default:
                    return false;
            }
        }

        private bool tryMoveToSibling(int delta)
        {
            // Move to the next/prev sibling. This means moving up to the parent, and continue with the next/prev node
            if (!_state.Any()) return false;

            // Keep the current state, when we cannot move after we've moved up to the parent, we'll need to stay
            // where we are
            var currentState = _state.Pop();
            
            // Can we move?
            if (NodeType == XPathNodeType.Element || NodeType == XPathNodeType.Text)
            {
                var newPos = position.ChildPos.Value + delta;
                if (newPos >= 0 && newPos < elementChildren().Count())
                {
                    moveToElementChild(newPos);
                    return true;
                }
                else
                {
                    // we cannot, roll back to old state
                    _state.Push(currentState);
                    return false;
                }
            }
            else if (NodeType == XPathNodeType.Root)
            {
                // we cannot, roll back to old state
                _state.Push(currentState);
                return false;
            }
            else
                throw new InvalidOperationException(
                    "Internal logic error: popping state on tryMove does not end up on element");
        }

        private bool moveToElementChild(int index)
        {
            var child = elementChildren().Skip(index).FirstOrDefault();
            if (child == null) return false;

            position.ChildPos = index;

            var newState = new NavigatorState(child);
            _state.Push(newState);

            return true;
        }

        private bool moveToAttributeChild(int index)
        {
            var child = attributeChildren().Skip(index).FirstOrDefault();
            if (child == null) return false;

            position.AttributePos = index;

            return true;
        }

        private bool moveToNamespaceChild(int index, XPathNamespaceScope scope)
        {
            if ((scope == XPathNamespaceScope.Local && _state.Count == 2) ||
                scope == XPathNamespaceScope.All)
            {
                var child = namespaceChildren().Skip(index).FirstOrDefault();
                if (child == null) return false;

                position.NamespacePos = index;

                return true;
            }
            else
                return false;
        }

        private JProperty currentAttribute
        {
            get
            {
                if (position.AttributePos != null)
                {
                    return attributeChildren().Skip(position.AttributePos.Value).First();
                }

                return null;
            }
        }

        private Tuple<string,string> currentNamespace
        {
            get
            {
                if (position.NamespacePos != null)
                {
                    return namespaceChildren().Skip(position.NamespacePos.Value).First();
                }

                return null;
            }
        }


        public override bool MoveToId(string id)
        {
            throw new NotImplementedException("Sorry, not too important for me");
        }

        public override bool MoveToFirstAttribute()
        {
            if (NodeType == XPathNodeType.Element)
                return moveToAttributeChild(0);
            else
                return false;
        }

        public override bool MoveToNextAttribute()
        {
            if (NodeType == XPathNodeType.Attribute)
                return moveToAttributeChild(position.AttributePos.Value + 1);
            else
                return false;
        }


        public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
        {
            if (NodeType == XPathNodeType.Element)
            {
                return moveToNamespaceChild(0, namespaceScope);
            }
            else
                return false;                
        }

        public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
        {
            if (NodeType == XPathNodeType.Namespace)
                return moveToNamespaceChild(position.NamespacePos.Value + 1, namespaceScope);
            else
                return false;
        }

        private string nt(string val)
        {
            return _nameTable.Get(val);
        }

        public override bool IsEmptyElement
        {
            get
            {
                if (NodeType == XPathNodeType.Element || NodeType == XPathNodeType.Root)
                    return !position.Children.Any();
                else
                    return false;
            }
        }

        public override string Name
        {
            get
            {
                var pref = Prefix != String.Empty ? Prefix + ":" : String.Empty;
                return _nameTable.Add(pref + LocalName);
            }
        }

        public override string LocalName
        {
            get
            {
                switch (NodeType)
                {
                    case XPathNodeType.Root:
                    case XPathNodeType.Text:
                        return nt(String.Empty);
                    case XPathNodeType.Element:
                        return _nameTable.Add(position.Element.Name);
                    case XPathNodeType.Attribute:
                        if (currentAttribute.IsValueProperty())
                            return nt(SPEC_CHILD_VALUE);
                        else
                            return _nameTable.Add(currentAttribute.Name);
                    case XPathNodeType.Namespace:
                        return nt(currentNamespace.Item1);
                    default:
                        throw new NotImplementedException("Don't know how to get LocalName when at node of type " + NodeType.ToString());
                }
            }
        }

        public override System.Xml.XmlNameTable NameTable
        {
            get { return _nameTable; }
        }

        public override string NamespaceURI
        {
            get 
            {
                if (NodeType == XPathNodeType.Element)
                {
                    // Check for the special <div> element, which comes from the xhtml namespace. Otherwise,
                    // return the FHIR namespace
                    if (LocalName == "div")
                        return nt(XmlNs.XHTML);
                    else
                        return nt(XmlNs.FHIR);
                }
                else
                    return nt(String.Empty);
            }
        }

        public override string Prefix
        {
            get
            {
                if (NodeType == XPathNodeType.Element)
                {
                    // Check for the special <div> element, which comes from the xhtml namespace. Otherwise,
                    // return the FHIR namespace
                    if (LocalName == "div")
                        return nt(XHTML_PREFIX);
                    else
                        return nt(FHIR_PREFIX);
                }
                else
                    return nt(String.Empty);
            }
        }

        public override XPathNodeType NodeType
        {
            get
            {
                if (position.Element.Name == ROOT_PROP_NAME)
                    return XPathNodeType.Root;
                else if (isTextNode(position.Element))
                    return XPathNodeType.Text;
                else if (position.AttributePos != null)
                    return XPathNodeType.Attribute;
                else if (position.NamespacePos != null)
                    return XPathNodeType.Namespace;
                else
                    return XPathNodeType.Element;
            }
        }

     
        public override string Value
        {
            get
            {
                if (NodeType == XPathNodeType.Attribute)
                {
                    if (currentAttribute.IsValueProperty())
                        return currentAttribute.ElementText();
                    else
                    {
                        // This is a named attribute (like id and contentType) for which only the
                        // primitive value is relevant, they cannot be extended
                        // Note that ElementText() will join all subnodes (in this case only one) into
                        // one string, so this *currently* works.
                        return currentAttribute.ElementText();
                    }
                }
                else if (NodeType == XPathNodeType.Namespace)
                {
                    return nt(currentNamespace.Item2);
                }
                else
                    return position.Element.ElementText();
            }   
        }

        public override string ToString()
        {
            return String.Join("/", _state);
        }
    }
}