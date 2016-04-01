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
        private INode _currentPred, _currentSubj;
        private string _typeName;
        private static TurtleReusableNodes nodes;

        internal TurtleFhirReader(IGraph g, INode pred, INode subj)
        {
            _g = g;
            _currentPred = pred;
            _currentSubj = subj;
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
            // Type is overruled by rdf:type
            // Currently this is only used in contained resources
            foreach (Triple t in _g.GetTriplesWithSubjectPredicate(subj, nodes.type))
            {
                string uri = t.Object.ToString();
                if (uri.StartsWith(FHIR_PREFIX))
                {
                    _typeName = uri.Substring(uri.LastIndexOf('/') + 1);
                }
            }
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
            if (nodes == null)
            {
                nodes = new TurtleReusableNodes(_g);
            }

            // As per discission 2016-mrt-24; find subject with property fhir:nodeRole fhir:treeRoot
            var fhirRootTriples = _g.GetTriplesWithPredicateObject(nodes.nodeRole, nodes.treeRoot);
            if (fhirRootTriples.Count() == 1)
            {
                Triple t = fhirRootTriples.First();

                // now find type triple
                var typeTriples = _g.GetTriplesWithSubjectPredicate(t.Subject, nodes.type);
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
            var members = new List<Tuple<string, IFhirReader>>();
            foreach (Triple t in _g.GetTriplesWithSubject(_currentSubj))
            {
                string pred = t.Predicate.ToString();
                if (pred.StartsWith(FHIR_PREFIX))
                {
                    string typePlusMemberName = pred.Substring(pred.LastIndexOf('/') + 1);
                    switch (typePlusMemberName)
                    {
                        // Ignore turtle specific members
                        case "index":
                        case "nodeRole":
                        case "reference":
                            continue;
                    }
                    string memberName = typePlusMemberName.Substring(typePlusMemberName.LastIndexOf('.') + 1);
                    /* 
                        Special handling of References; in Turtle Reference is added to the memberName.
                        Remove it here and handle in DispatchingReader.determineElementPropertyType.
                        There "Reference" is used as type for polymorph members with a Reference.
                    */
                    if (memberName.EndsWith("Reference"))
                    {
                        memberName = memberName.Substring(0, memberName.Length - 9);
                    }
                    members.Add(new Tuple<string, IFhirReader>(memberName, new TurtleFhirReader(_g, t.Predicate, t.Object)));
                }
            }
            return members.AsEnumerable<Tuple<string, IFhirReader>>();
        }

        public object GetPrimitiveValue()
        {
            var valueNode = _currentSubj;
            if (_currentSubj is IUriNode)
            {
                var valueTriples = _g.GetTriplesWithSubjectPredicate(_currentSubj, nodes.value);
                if (valueTriples.Count() == 0)
                {
                    // the subject is the value; e.g. for Coding.system
                    return _currentSubj.ToString();
                }
                var valueTriple = valueTriples.First();
                valueNode = valueTriple.Object;
            }
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
            return _typeName;
        }
    }
}
