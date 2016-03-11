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
        private readonly ModelInspector _inspector;

        private IGraph _g;
        private INode _currentSubj;
        private Stack<INode> _subjStack = new Stack<INode>();
        private IUriNode _currentPred;
        private Stack<IUriNode> _predStack = new Stack<IUriNode>();
        private string _currentType;
        private Stack<string> _typeStack = new Stack<string>();
        private string _currentName;
        private Stack<string> _nameStack = new Stack<string>();

        private bool _coding = false;
        private bool _coding_system = false;
        private string _coding_system_value;
        private bool _coding_code = false;

        public TurtleFhirWriter()
        {
            _inspector = SerializationConfig.Inspector;

            _g = new Graph();
            _g.NamespaceMap.AddNamespace("fhir", UriFactory.Create("http://hl7.org/fhir/"));
            _g.NamespaceMap.AddNamespace("sct", UriFactory.Create("http://snomed.info/sct/"));
            _g.NamespaceMap.AddNamespace("loinc", UriFactory.Create("http://loinc.org/"));
        }

        public string turtleAsString()
        {
            StringBuilder resultBuilder = new StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(resultBuilder);
            // Use TurtleSyntax.W3C so that prefixes are used.
            // TurtleSyntax.Original will expand the prefixes.
            CompressingTurtleWriter tw = new CompressingTurtleWriter(VDS.RDF.Parsing.TurtleSyntax.W3C);
            tw.Save(_g, sw);
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
            if (_typeStack.Count > 0)
            {
                IUriNode pred = _g.CreateUriNode(string.Format("fhir:{0}.{1}", _typeStack.Peek(), _currentName));
                _g.Assert(_subjStack.Peek(), pred, _currentSubj);
                _currentSubj = _subjStack.Pop();
            }
            else
            {
                // top level complex content, ignore
            }
        }

        public void WriteEndProperty()
        {
            if ("coding".Equals(_currentName))
            {
                _coding = false;
            }
            else if ("system".Equals(_currentName))
            {
                _coding_system = false;
            }
            else if ("code".Equals(_currentName))
            {
                _coding_code = false;
            }

            //Console.WriteLine("DEBUG: WriteEndProperty");
            _currentPred = _predStack.Pop();
            _currentType = _typeStack.Pop();
            _currentName = _nameStack.Pop();
        }

        public void WriteEndRootObject(bool contained)
        {
            Console.WriteLine("DEBUG: WriteEndRootObject contained={0}", contained);
        }

        public void WritePrimitiveContents(object value, XmlSerializationHint xmlFormatHint)
        {
            IUriNode simplePred = _g.CreateUriNode(string.Format("fhir:{0}", _currentName));
            var valueAsString = PrimitiveTypeConverter.ConvertTo<string>(value);
            ILiteralNode obj = _g.CreateLiteralNode(valueAsString);
            _g.Assert(_currentSubj, simplePred, obj);

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
                    IUriNode pred = _g.CreateUriNode("rdf:type");
                    IUriNode code = _g.CreateUriNode(string.Format("sct:{0}", valueAsString));
                    _g.Assert(_subjStack.Peek(), pred, code);
                }
                else if ("http://loinc.org".Equals(_coding_system_value))
                {
                    //Console.WriteLine("DEBUG: loinc:{0}", valueAsString);
                    IUriNode pred = _g.CreateUriNode("rdf:type");
                    IUriNode code = _g.CreateUriNode(string.Format("loinc:{0}", valueAsString));
                    _g.Assert(_subjStack.Peek(), pred, code);
                }
            }
        }

        public void WriteStartArray()
        {
            //Console.WriteLine("DEBUG: WriteStartArray");
        }

        private bool _first = true;
        public void WriteStartComplexContent()
        {
            //Console.WriteLine("DEBUG: WriteStartComplexContent");
            if (!_first)
            {
                _subjStack.Push(_currentSubj);
                _currentSubj = _g.CreateBlankNode();
            }
            else
            {
                _first = false;
            }
        }

        public void WriteStartProperty(string name)
        {
            Console.WriteLine("DEBUG: WriteStartProperty {0}", name);
            IUriNode pred = _g.CreateUriNode(string.Format("fhir:{0}.{1}", _currentType, name));
            _typeStack.Push(_currentType);
            _nameStack.Push(_currentName);
            _predStack.Push(pred);
            _currentPred = pred;

            // BEGIN Try and figure out typeName
            ClassMapping clsMap = _inspector.FindClassMappingForResource(_currentType);
            if (clsMap == null)
            {
                // try datatype
                clsMap = _inspector.FindClassMappingForFhirDataType(_currentType);
            }
            if (clsMap != null)
            {
                PropertyMapping prtMap = clsMap.FindMappedElementByName(name);
                if (prtMap != null)
                {
                    _currentType = prtMap.ElementType.Name;
                    _currentName = name;
                }
                else
                {
                    _currentType = name;
                    _currentName = name;
                }
                Console.WriteLine("DEBUG: WriteStartProperty type={0}", _currentType);
            }
            else
            {
                Console.WriteLine("DEBUG: WriteStartProperty _currentType=name: {0}", name);
                _currentType = name;
                _currentName = name;
            }
            // END Try and figure out typeName

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
            _currentSubj = _g.CreateBlankNode();
            IUriNode pred = _g.CreateUriNode("rdf:type");
            IUriNode obj = _g.CreateUriNode("fhir:" + _currentType);
            _g.Assert(_currentSubj, pred, obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && _g != null) ((IDisposable)_g).Dispose();
        }
    }
}
