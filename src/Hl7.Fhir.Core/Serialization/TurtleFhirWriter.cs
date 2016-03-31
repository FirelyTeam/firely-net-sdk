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
        private string _currentTypeName;
        private Stack<string> _typeStack = new Stack<string>();
        private string _currentMemberName;
        private Stack<string> _memberStack = new Stack<string>();
        private int _arrayIndex = -1;
        private Stack<int> _arrayIndexStack = new Stack<int>();

        private bool _coding = false;
        private bool _coding_system = false;
        private string _coding_system_value;
        private bool _coding_code = false;

        public TurtleFhirWriter()
        {
            _inspector = SerializationConfig.Inspector;

            _g = new Graph();
            //_g.NamespaceMap.AddNamespace("xs", UriFactory.Create("http://www.w3.org/2001/XMLSchema#"));
            _g.BaseUri = UriFactory.Create("http://localhost/fhir/");
            _g.NamespaceMap.AddNamespace("fhir", UriFactory.Create("http://hl7.org/fhir/"));
            _g.NamespaceMap.AddNamespace("sct", UriFactory.Create("http://snomed.info/sct/"));
            _g.NamespaceMap.AddNamespace("loinc", UriFactory.Create("http://loinc.org/owl#"));
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
            _arrayIndex = _arrayIndexStack.Pop();
        }

        public void WriteEndComplexContent()
        {
            if (_typeStack.Count > 0)
            {
                IUriNode pred = _g.CreateUriNode(string.Format("fhir:{0}.{1}", _typeStack.Peek(), _memberStack.Peek()));
                _g.Assert(_subjStack.Peek(), pred, _currentSubj);
                _currentSubj = _subjStack.Pop();
            }
            else
            {
                // top level complex content, ignore
            }
            _arrayIndex = _arrayIndexStack.Pop();
        }

        public void WriteEndProperty()
        {
            _currentTypeName = _typeStack.Pop();
            _currentMemberName = _memberStack.Pop();
            switch (_currentMemberName)
            {
                case "coding": _coding = false; break;
                case "system": _coding_system = false; break;
                case "code": _coding_code = false; break;
            }
        }

        public void WriteEndRootObject(bool contained)
        {
        }

        private static string lowerCamel(string p)
        {
            if (p == null) return p;

            var c = p[0];

            return Char.ToLowerInvariant(c) + p.Remove(0, 1);
        }

        public void WritePrimitiveContents(object value, XmlSerializationHint xmlFormatHint)
        {
            IUriNode pred = _g.CreateUriNode(string.Format("fhir:{0}", _currentMemberName));
            var valueAsString = PrimitiveTypeConverter.ConvertTo<string>(value);
            INode obj;
            switch (_currentTypeName)
            {
                case "FhirUri":
                    Uri valueAsUri;
                    if (Uri.TryCreate(valueAsString, UriKind.Absolute, out valueAsUri))
                    {
                        obj = _g.CreateUriNode(valueAsUri);
                    }
                    else
                    {
                        Console.WriteLine("DEBUG uri is relative fall back to literal: {0}", valueAsString);
                        obj = _g.CreateLiteralNode(valueAsString);
                    }
                    break;
                case "Instant":
                case "FhirDateTime":
                    obj = _g.CreateLiteralNode(valueAsString, UriFactory.Create("xsd:dateTime"));
                    break;
                case "FhirDecimal":
                    obj = _g.CreateLiteralNode(valueAsString, UriFactory.Create("xsd:decimal"));
                    break;
                // handle all others as xsd:string
                default:
                    obj = _g.CreateLiteralNode(valueAsString);
                    break;
            }
            _g.Assert(_currentSubj, pred, obj);

            // Special handling of reference; workaround "my stack issue"
            string typeTmp = _typeStack.Pop();
            if (_typeStack.Peek() == "Reference")
            {
                Uri valueAsUri;
                if (Uri.TryCreate(_g.BaseUri, valueAsString, out valueAsUri))
                {
                    _g.Assert(_subjStack.Peek(), _g.CreateUriNode("fhir:reference"), _g.CreateUriNode(valueAsUri));
                }
            }
            _typeStack.Push(typeTmp);

            if (_coding_system)
            {
                _coding_system_value = valueAsString;
            }
            else if (_coding_code)
            {
                switch (_coding_system_value)
                {
                    case "http://snomed.info/sct":
                        //Console.WriteLine("DEBUG: sct:{0}", valueAsString);
                        IUriNode code = _g.CreateUriNode(string.Format("sct:{0}", valueAsString));
                        _g.Assert(_subjStack.Peek(), _g.CreateUriNode("rdf:type"), code);
                        break;
                    case "http://loinc.org":
                        //Console.WriteLine("DEBUG: loinc:{0}", valueAsString);
                        code = _g.CreateUriNode(string.Format("loinc:{0}", valueAsString));
                        _g.Assert(_subjStack.Peek(), _g.CreateUriNode("rdf:type"), code);
                        break;
                }
            }
        }

        public void WriteStartArray()
        {
            _arrayIndexStack.Push(_arrayIndex);
            _arrayIndex = 0;
        }

        private bool _first = true;
        public void WriteStartComplexContent()
        {
            if (!_first)
            {
                _subjStack.Push(_currentSubj);
                _currentSubj = _g.CreateBlankNode();
                if(_arrayIndex >= 0)
                {
                    _g.Assert(_currentSubj, _g.CreateUriNode("fhir:index"), _g.CreateLiteralNode(_arrayIndex.ToString()));
                    _arrayIndex++;
                }
            }
            else
            {
                _first = false;
            }
            _arrayIndexStack.Push(_arrayIndex);
            _arrayIndex = -1;
        }

        public void WriteStartProperty(PropertyMapping propMap, string memberName)
        {
            _currentTypeName = propMap.DefiningType.Name;
            // memberName = member with type <- thinking now 30-mrt-2016 is to use this
            // propMap.Name = memberName without type
            _currentMemberName = memberName;

            // The fhir type Reference is prefixed with "Resource" in the fhir-net-api
            if (_currentTypeName == "ResourceReference")
            {
                _currentTypeName = "Reference";
            }
            // When the ElementType(also works for array/collection) is "ResourceReference" 
            // postfix the pred with "Reference"
            if (propMap.ElementType.Name == "ResourceReference")
            {
                _currentMemberName += "Reference";
            }
            // Special handling of component
            if (_currentTypeName == "ComponentComponent")
            {
                _currentTypeName = _typeStack.Peek() + ".component";
            }

            _typeStack.Push(_currentTypeName);
            _memberStack.Push(_currentMemberName);

            switch(_currentMemberName)
            {
                case "coding":
                    _coding = true;
                    break;
                case "system":
                    if (_coding)
                    {
                        _coding_system = true;
                        _coding_code = false;
                    }
                    break;
                case "code":
                    if (_coding)
                    {
                        _coding_system = false;
                        _coding_code = true;
                    }
                    break;
            }
        }

        public void WriteStartRootObject(string name, string id, bool contained)
        {
            _currentTypeName = name;
            if (id != null)
                _currentSubj = _g.CreateBlankNode(id);
            else
                _currentSubj = _g.CreateBlankNode();
            _g.Assert(_currentSubj, _g.CreateUriNode("rdf:type"), _g.CreateUriNode("fhir:" + _currentTypeName));
            _g.Assert(_currentSubj, _g.CreateUriNode("fhir:nodeRole"), _g.CreateUriNode("fhir:treeRoot"));
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
