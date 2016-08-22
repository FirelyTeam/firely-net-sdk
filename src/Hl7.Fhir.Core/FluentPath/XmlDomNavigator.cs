/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Xml;
using System.Xml.Linq;
using Hl7.Fhir.Support;
using Hl7.ElementModel;

namespace Hl7.Fhir.Serialization
{
    public struct XmlDomFhirNavigator : IElementNavigator, IPositionInfo
    {
        internal XmlDomFhirNavigator(XElement current, XAttribute attribute, XState state, bool disallowXsiAttributesOnRoot = false)
        {
            this.current = current;
            this.attribute = attribute;
            this.state = state;
            this.DisallowXsiAttributesOnRoot = disallowXsiAttributesOnRoot;
        }

        public string Name
        {
            get
            {
                if (state == XState.Element && current != null)
                {
                    return current.Name.LocalName;
                }
                else if (state == XState.Attr && attribute != null)
                {
                    return attribute.Name.LocalName;
                }
                else
                {
                    return null;
                }
            }
        }

        public string TypeName
        {
            get
            {
                if (state == XState.Element && current != null)
                {
                    return "element";
                }
                else if (state == XState.Attr && attribute != null)
                {
                    return "attribute";
                }
                else
                {
                    return "unknown";
                }
            }
        }

        public object Value
        {
            get
            {
                if (state == XState.Element && current != null)
                {
                    return current.Value;
                }
                else if (state == XState.Attr && attribute != null)
                {
                    return attribute.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        public string Path
        {
            get
            {
                // This does not look like a property that a navigator can expose from the underlying navigable, without 
                // a serious performance dip.
                return null;
            }
        }

        public IElementNavigator Clone()
        {
            return new XmlDomFhirNavigator(current, attribute, state, DisallowXsiAttributesOnRoot);
        }

        public bool MoveToFirstChild()
        {
            if (state == XState.Element)
            {
                bool ok = MoveToFirstXAttribute();
                if (!ok) ok = MoveToFirstXElement();
                return ok;
            }
            else
            {
                return false;
            }

        }

        public bool MoveToNext()
        {
            bool ok = true;

            if (state == XState.Attr)
            {
                ok = MoveToNextXAttribute();
                if (!ok)
                {
                    ok = MoveToFirstXElement();
                }
            }
            else
            {
                ok = MoveToNextXElement();
            }
            return ok;
        }

        private void MovetoRoot(XObject root)
        {
            if (root is XDocument)
                current = ((XDocument)root).Root;
            else
                current = (XElement)root;
        }

        public const string BINARY_CONTENT_MEMBER_NAME = "content";
        public bool DisallowXsiAttributesOnRoot
        {
            get;
            set;
        }

        public int LineNumber
        {
            get
            {
                var li = (IXmlLineInfo)current;

                if (!li.HasLineInfo())
                    throw Error.InvalidOperation("No lineinfo available. Please read the Xml document using LoadOptions.SetLineInfo.");

                return li.LineNumber;
            }
        }

        public int LinePosition
        {
            get
            {
                var li = (IXmlLineInfo)current;

                if (!li.HasLineInfo())
                    throw Error.InvalidOperation("No lineinfo available. Please read the Xml document using LoadOptions.SetLineInfo.");

                return li.LinePosition;
            }
        }

       

        private bool ValidAttribute(XAttribute attr)
        {
            return true;
        }

        private bool MoveToFirstXAttribute()
        {
            if (current.HasAttributes)
            {
                state = XState.Attr;
                this.attribute = current.FirstAttribute;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool MoveToNextXAttribute()
        {
            attribute = attribute.NextAttribute;
            return attribute != null;
        }

        private bool MoveToFirstXElement()
        {
            // we just came from the last XAttribute.
            state = XState.Element;

            if (current.HasElements)
            {
                current = (XElement)current.FirstNode;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool MoveToNextXElement()
        {
            if (current is XElement)
            {
                var node = (XNode)current;
                current = (XElement)node.NextNode;
                return (current != null);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return this.Name+"("+this.TypeName+") = "+this.Value;
        }

        XElement current;
        XAttribute attribute;
        XState state;
    }

    public enum XState { Element, Attr, Unknown };
}