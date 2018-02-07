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
using System.Xml;

namespace Hl7.Fhir.XPath
{
    internal class XPath2Context : XsltContext
    {
        private readonly List<INamedXsltContextFunction> _functions = new List<INamedXsltContextFunction>();

        public XPath2Context() : this(new NameTable())
        {
        }

        public XPath2Context(NameTable table)
            : base(table)
        {
            _functions.Add(new UpperCaseFunction());
            _functions.Add(new LowerCaseFunction());
            _functions.Add(new ExistsFunction());
            _functions.Add(new DistinctValuesFunction());
        }


        public override int CompareDocument(string baseUri, string nextbaseUri)
        {
            throw new NotImplementedException();
        }

        public override bool PreserveWhitespace(System.Xml.XPath.XPathNavigator node)
        {
            throw new NotImplementedException();
        }

        public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes)
        {
            var func = _functions.SingleOrDefault(f => f.Name == name && f.Prefix == prefix);

            if(func != null)
                return func;
            else
                throw new InvalidOperationException("Unknown function in XPath: " + name);              
        }

        public override IXsltContextVariable ResolveVariable(string prefix, string name)
        {
            throw new NotImplementedException();
        }

        public override bool Whitespace
        {
            get {
                return true;
            }
        }
    }


    internal interface INamedXsltContextFunction : IXsltContextFunction
    {
        string Prefix { get; }
        string Name { get; }
    }
   
    internal class UpperCaseFunction : CustomXsltFunction
    {
        public UpperCaseFunction() : base("upper-case", new XPathResultType[1] { XPathResultType.String }, XPathResultType.String) {}

        public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
        {
            if (args.Count() != 1) throw new ArgumentException("upper-case accepts only 1 argument");

            return ((string)args[0]).ToUpperInvariant();
        }
    }

    internal class LowerCaseFunction : CustomXsltFunction
    {
        public LowerCaseFunction() : base("lower-case", new XPathResultType[1] { XPathResultType.String }, XPathResultType.String) { }

        public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
        {
            if (args.Count() != 1) throw new ArgumentException("lower-case accepts only 1 argument");

            return ((string)args[0]).ToLowerInvariant();
        }
    }


    internal class NodeListIterator : XPathNodeIterator
    {
        private List<XPathNavigator> _nodes;
        private int _position = -1;

        
        public NodeListIterator(List<XPathNavigator> nodes)
        {
            _nodes = nodes;
        }

        public NodeListIterator(NodeListIterator other)
        {
            _nodes = other._nodes;
            _position = other._position;
        }

        public override int Count
        {
            get
            {
                return _nodes.Count;
            }
        }

        public override XPathNodeIterator Clone()
        {
            return new NodeListIterator(this);
        }

        public override XPathNavigator Current
        {
            get { return _nodes[_position]; }
        }

        public override int CurrentPosition
        {
            get { return _position; }
        }

        public override bool MoveNext()
        {
            if(_position+1 < _nodes.Count)
            {
                _position++;
                return true;
            }

            return false;
        }
    }




    internal class DistinctValuesFunction : CustomXsltFunction
    {
        public DistinctValuesFunction() : base("distinct-values", new XPathResultType[1] { XPathResultType.NodeSet }, XPathResultType.NodeSet) { }

        public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
        {
            if (args.Count() != 1) throw new ArgumentException("distinct-values accepts only 1 argument");

            var seen = new List<string>();
            var result = new List<XPathNavigator>();
            var nodeSet = (XPathNodeIterator)args[0];

            foreach (var n in nodeSet)
            {
                var node = (XPathNavigator)n;

                if (!seen.Contains(node.Value))
                {
                    seen.Add(node.Value);
                    result.Add(node);
                }
            }

            return new NodeListIterator(result);
        }
    }

    internal class ExistsFunction : CustomXsltFunction
    {
        public ExistsFunction() : base("exists", new XPathResultType[1] { XPathResultType.NodeSet }, XPathResultType.Boolean) { }

        public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
        {
            if (args.Count() != 1) throw new ArgumentException("exists accepts only 1 argument");

            var nodeSet = (XPathNodeIterator)args[0];
            return nodeSet.Count != 0;
        }
    }
}
#endif