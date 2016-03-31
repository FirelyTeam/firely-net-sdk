using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace Hl7.Fhir.Serialization
{
    class TurtleFhirReader : IFhirReader
    {
        public const string FHIR_PREFIX = "http://hl7.org/fhir/";
        private IGraph _g;
        private INode _currentSubj, _currentPred, _currentObj;
        private string _typeName;

        internal TurtleFhirReader(IGraph g, Triple t)
        {
            _g = g;
            _currentSubj = t.Subject;
            _currentPred = t.Predicate;
            _currentObj = t.Object;
            string predString = _currentPred.ToString();
            // get type from predicate
            if (predString.StartsWith(FHIR_PREFIX))
            {
                string typePlusMemberName = predString.Substring(predString.LastIndexOf('/') + 1);
                int dotIdx = typePlusMemberName.IndexOf('.');
                if (dotIdx > 0)
                {
                    _typeName = typePlusMemberName.Substring(0, dotIdx);
                }
            }
            else
            {
                _typeName = null;
            }
            // type is overruled by rdf:type
            IUriNode typePred = _g.CreateUriNode("rdf:type");
            foreach (Triple t2 in _g.GetTriplesWithSubject(_currentSubj))
            {
                if (typePred.Equals(t2.Predicate))
                {
                    string uri = t2.Object.ToString();
                    if (uri.StartsWith(FHIR_PREFIX))
                    {
                        _typeName = uri.Substring(uri.LastIndexOf('/') + 1);
                    }
                }
            }
            //Console.WriteLine("DEBUG Constructor subject={0} type={1}", predString, _typeName);
        }

        public TurtleFhirReader(StringReader stringReader)
        {
            try
            {
                TurtleParser parser = new TurtleParser();
                _g = new Graph();
                parser.Load(_g, stringReader);
            }
            catch (Exception e)
            {
                throw Error.Format("Cannot parse turtle: " + e.Message, null);
            }

            // As per discission 2016-mrt-24; find subject with property fhir:nodeRole fhir:treeRoot
            var fhirRootTriples = _g.GetTriplesWithPredicateObject(_g.CreateUriNode("fhir:nodeRole"), _g.CreateUriNode("fhir:treeRoot"));
            if (fhirRootTriples.Count() == 1)
            {
                Triple t = fhirRootTriples.First();

                // now find type triple
                IUriNode typePred = _g.CreateUriNode("rdf:type");
                var typeTriples = _g.GetTriplesWithSubjectPredicate(t.Subject, typePred);
                Triple t2 = typeTriples.First();

                string uri = t2.Object.ToString();
                if (uri.StartsWith(FHIR_PREFIX))
                {
                    _currentSubj = t.Subject;
                    _typeName = uri.Substring(uri.LastIndexOf('/') + 1);
                    return;
                }
            }
            throw Error.Format("Unable to determin resourcetype from turtle", null);
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

        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            //Console.WriteLine("DEBUG GetMembers {0}", _currentSubj);
            var members = new List<Tuple<string, IFhirReader>>();
            foreach (Triple t in _g.GetTriplesWithSubject(_currentSubj))
            {
                string pred = t.Predicate.ToString();
                if (pred.StartsWith(FHIR_PREFIX))
                {
                    string typePlusMemberName = pred.Substring(pred.LastIndexOf('/') + 1);
                    string memberName = typePlusMemberName.Substring(typePlusMemberName.IndexOf('.') + 1);
                    switch (memberName)
                    {
                        // Strip turtle specific members
                        case "index":
                        case "nodeRole":
                        case "reference":
                            break;
                        default:
                            members.Add(new Tuple<string, IFhirReader>(memberName, new TurtleFhirReader(_g, t)));
                            break;
                    }
                }
            }
            return members.AsEnumerable<Tuple<string, IFhirReader>>();
        }

        public object GetPrimitiveValue()
        {
            var valueTriples = _g.GetTriplesWithSubjectPredicate(_currentObj, _g.CreateUriNode("fhir:value"));
            if (valueTriples.Count() == 0)
            {
                return null;
            }
            var valueTriple = valueTriples.First();
            var valueNode = valueTriple.Object;
            string value;
            // Make sure to only get the value, for now ignore the xsd type
            if (valueNode is ILiteralNode)
            {
                value = ((ILiteralNode)valueNode).Value;
            }
            else
            {
                value = valueNode.ToString();
            }
            return value;
        }

        public string GetResourceTypeName()
        {
            //Console.WriteLine("DEBUG GetResourceTypeName={0}", _typeName);
            return _typeName;
        }
    }
}
