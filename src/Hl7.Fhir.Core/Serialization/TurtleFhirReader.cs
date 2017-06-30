using Hl7.Fhir.Utility;
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
                throw Error.Format("Cannot parse turtle: " + e.Message, this);
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
            throw Error.Format("Unable to determin resourcetype from turtle", this);
        }

        #region IPositionInfo
        public int LineNumber { get { return -1; } }
        public int LinePosition { get { return -1; } }
        public string Path {  get { return string.Format("{0} {1}",_currentSubj.ToString(), _currentPred.ToString()); } }
        #endregion

        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            var members = new List<Tuple<string, IFhirReader>>();
            foreach (Triple t in _g.GetTriplesWithSubject(_currentSubj))
            {
                string pred = t.Predicate.ToString();
                if (pred.StartsWith(FHIR_PREFIX))
                {
                    string typePlusMemberName = pred.Substring(pred.LastIndexOf('/') + 1);
                    // Ignore turtle specific members
                    switch (typePlusMemberName)
                    {
                        case "index":
                        case "nodeRole":
                        case "link":
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

            // Sort members based on fhir:index predicate of the members
            members.Sort(delegate (Tuple<string, IFhirReader> x, Tuple<string, IFhirReader> y)
            {
                int xindex = ((TurtleFhirReader)x.Item2).FhirIndex();
                int yindex = ((TurtleFhirReader)y.Item2).FhirIndex();
                if (xindex == yindex) return 0;
                else if (xindex < yindex) return -1;
                else if (xindex > yindex) return 1;
                else return 0;
            });

            return members.AsEnumerable<Tuple<string, IFhirReader>>();
        }

        // if there is a index predicate in this subject return its value
        // needed to restore the order in an array
        private int FhirIndex()
        {
            Triple tIndex = _g.GetTriplesWithSubjectPredicate(_currentSubj, nodes.index).FirstOrDefault();
            if (tIndex?.Object is ILiteralNode)
            {
                return int.Parse(((ILiteralNode)tIndex.Object).Value);
            }
            else
            {
                return -1;
            }
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
