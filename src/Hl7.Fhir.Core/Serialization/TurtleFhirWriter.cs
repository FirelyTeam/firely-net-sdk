using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Introspection;
using VDS.RDF;
using VDS.RDF.Writing;

namespace Hl7.Fhir.Serialization
{
    class TurtleFhirWriter : IFhirWriter
    {
        private IGraph g;
        private INode _currentSubj;
        private Stack<INode> _subjStack = new Stack<INode>();
        private INode _currentPred;
        private Stack<INode> _predStack = new Stack<INode>();
        private string _currentType;
        private Stack<string> _typeStack = new Stack<string>();

        private bool _coding = false;
        private bool _coding_system = false;
        private string _coding_system_value;
        private bool _coding_code = false;

        public TurtleFhirWriter()
        {
            g = new Graph();
            g.NamespaceMap.AddNamespace("fhir", UriFactory.Create("http://hl7.org/fhir/"));
            g.NamespaceMap.AddNamespace("sct", UriFactory.Create("http://snomed.info/sct/"));
            g.NamespaceMap.AddNamespace("loinc", UriFactory.Create("http://loinc.org/"));
        }

        public string turtleAsString()
        {
            StringBuilder resultBuilder = new StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(resultBuilder);
            // Use TurtleSyntax.W3C so that prefixes are used.
            // TurtleSyntax.Original will expand the prefixes.
            CompressingTurtleWriter tw = new CompressingTurtleWriter(VDS.RDF.Parsing.TurtleSyntax.W3C);
            tw.PrettyPrintMode = true;
            tw.Save(g, sw);
            return resultBuilder.ToString();
        }

        public bool HasValueElementSupport
        {
            get { return false; }
        }

        public void WriteEndArray()
        {
            //Console.WriteLine("DEBUG: WriteEndArray");
        }

        public void WriteEndComplexContent()
        {
            //Console.WriteLine("DEBUG: WriteEndComplexContent");
            INode pred;
            if (_typeStack.Count > 0)
            {
                pred = g.CreateUriNode(string.Format("fhir:{0}.{1}", _typeStack.Peek(), _currentType));
            }
            else
            {
                pred = g.CreateUriNode(string.Format("fhir:{0}", _currentType));
            }
            g.Assert(_subjStack.Peek(), pred, _currentSubj);
            _currentSubj = _subjStack.Pop();
        }

        public void WriteEndProperty()
        {
            if ("coding".Equals(_currentType))
            {
                _coding = false;
            }
            else if ("system".Equals(_currentType))
            {
                _coding_system = false;
            }
            else if ("code".Equals(_currentType))
            {
                _coding_code = false;
            }

            //Console.WriteLine("DEBUG: WriteEndProperty");
            _currentPred = _predStack.Pop();
            _currentType = _typeStack.Pop();
        }

        public void WriteEndRootObject(bool contained)
        {
            Console.WriteLine("DEBUG: WriteEndRootObject contained={0}", contained);
        }

        public void WritePrimitiveContents(object value, XmlSerializationHint xmlFormatHint)
        {
            IUriNode simplePred = g.CreateUriNode(string.Format("fhir:{0}", _currentType));
            var valueAsString = PrimitiveTypeConverter.ConvertTo<string>(value);
            ILiteralNode obj = g.CreateLiteralNode(valueAsString);
            g.Assert(_currentSubj, simplePred, obj);

            if (_coding_system)
            {
                //Console.WriteLine("DEBUG: coding_system_value");
                _coding_system_value = valueAsString;
            }
            else if (_coding_code)
            {
                if ("http://snomed.info/sct".Equals(_coding_system_value))
                {
                    //Console.WriteLine("DEBUG: sct:{0}", valueAsString);
                    IUriNode pred = g.CreateUriNode("rdf:type");
                    IUriNode code = g.CreateUriNode(string.Format("sct:{0}", valueAsString));
                    g.Assert(_subjStack.Peek(), pred, code);
                }
                else if ("http://loinc.org".Equals(_coding_system_value))
                {
                    //Console.WriteLine("DEBUG: loinc:{0}", valueAsString);
                    IUriNode pred = g.CreateUriNode("rdf:type");
                    IUriNode code = g.CreateUriNode(string.Format("loinc:{0}", valueAsString));
                    g.Assert(_subjStack.Peek(), pred, code);
                }
            }
        }

        public void WriteStartArray()
        {
            //Console.WriteLine("DEBUG: WriteStartArray");
        }

        public void WriteStartComplexContent()
        {
            //Console.WriteLine("DEBUG: WriteStartComplexContent");
            INode subj = g.CreateBlankNode();
            _subjStack.Push(_currentSubj);
            _currentSubj = subj;
        }

        public void WriteStartProperty(string name)
        {
            Console.WriteLine("DEBUG: WriteStartProperty {0}", name);
            IUriNode pred = g.CreateUriNode(string.Format("fhir:{0}.{1}", _currentType, name));
            _predStack.Push(pred);
            _currentPred = pred;
            _typeStack.Push(_currentType);
            _currentType = name;

            if ("coding".Equals(name))
            {
                //Console.WriteLine("DEBUG: coding");
                _coding = true;
            }
            if (_coding && "system".Equals(name))
            {
                //Console.WriteLine("DEBUG: coding_system");
                _coding_system = true;
                _coding_code = false;
            }
            if (_coding && "code".Equals(name))
            {
                //Console.WriteLine("DEBUG: coding_code");
                _coding_system = false;
                _coding_code = true;
            }
        }

        public void WriteStartRootObject(string name, bool contained)
        {
            Console.WriteLine("DEBUG: WriteStartRootObject {0} contained={1}", name, contained);
            _currentType = name;
            _currentSubj = g.CreateBlankNode();
            IUriNode pred = g.CreateUriNode("rdf:type");
            IUriNode obj = g.CreateUriNode("fhir:" + _currentType);
            g.Assert(_currentSubj, pred, obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && g != null) ((IDisposable)g).Dispose();
        }
    }
}
