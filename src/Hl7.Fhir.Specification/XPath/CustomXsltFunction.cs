/*
* Copyright (c) 2014, Firely (info@fire.ly) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET_XSD_SCHEMA
using System.Xml.Xsl;
using System.Xml.XPath;

namespace Hl7.Fhir.XPath
{
    internal delegate object InvokedFunction(XsltContext ctx, object[] args, XPathNavigator nav);

    internal class CustomXsltFunction : INamedXsltContextFunction
    {
        public CustomXsltFunction(string name, XPathResultType[] argTypes, XPathResultType returnType, InvokedFunction function = null)
        {
            Prefix = String.Empty;
            Name = name;
            ArgTypes = argTypes;
            ReturnType = returnType;
            Function = function;
        }

        public string Prefix { get; private set; }
        public string Name{ get; private set; }
        public System.Xml.XPath.XPathResultType[] ArgTypes { get; private set; }
        public XPathResultType ReturnType { get; private set; }
        public InvokedFunction Function { get; private set; }

        public int Maxargs
        {
            get { return ArgTypes.Count(); }
        }

        public int Minargs
        {
            get { return ArgTypes.Count(); }
        }

        public virtual object Invoke(XsltContext xsltContext, object[] args, System.Xml.XPath.XPathNavigator docContext)
        {
            if (Function != null)
                return Function(xsltContext, args, docContext);
            else
                throw new NotImplementedException("Custom function not implemented. Supply a lamba, or override Invoke()");
        }
    }   
}
#endif