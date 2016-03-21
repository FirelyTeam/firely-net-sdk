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
        private INode _currentSubj, _currentPred;
        private string _typeName;

        internal TurtleFhirReader(IGraph g, INode subj, INode pred)
        {
            _g = g;
            _currentSubj = subj;
            _currentPred = pred;
            string predString = pred.ToString();
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
            foreach (Triple t in _g.GetTriplesWithSubject(subj))
            {
                if (typePred.Equals(t.Predicate))
                {
                    string uri = t.Object.ToString();
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

            //
            // Find the tripple with predicate rdf:type that has a subject that is never used as object == root!
            //
            IUriNode typePred = _g.CreateUriNode("rdf:type");
            foreach (Triple t in _g.GetTriplesWithPredicate(typePred))
            {
                if (_g.GetTriplesWithObject(t.Subject).Count() == 0)
                {
                    //Console.WriteLine("DEBUG type={0}", t.Object);
                    string uri = t.Object.ToString();
                    if (uri.StartsWith(FHIR_PREFIX))
                    {
                        _currentSubj = t.Subject;
                        _typeName = uri.Substring(uri.LastIndexOf('/') + 1);
                        return;
                    }
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
                    if ("index" != memberName)
                    {
                        // Is rdf:type specified? then append to memberName for now
                        IUriNode typePred = _g.CreateUriNode("rdf:type");
                        Triple typeTriple = _g.GetTriplesWithSubject(t.Object).SingleOrDefault<Triple>(tr => typePred.Equals(tr.Predicate));
                        if (typeTriple != null)
                        {
                            string uri = typeTriple.Object.ToString();
                            if (uri.StartsWith(FHIR_PREFIX))
                            {
                                memberName += uri.Substring(uri.LastIndexOf('/') + 1);
                            }
                        }
                        members.Add(new Tuple<string, IFhirReader>(memberName, new TurtleFhirReader(_g, t.Object, t.Predicate)));
                    }
                }
            }
            return members.AsEnumerable<Tuple<string, IFhirReader>>();
        }

        public object GetPrimitiveValue()
        {
            string pred = _currentPred.ToString();
            string value = _currentSubj.ToString();
            //Console.WriteLine("DEBUG GetPrimitiveValue {0}={1}", pred, value);
            return value;
        }

        public string GetResourceTypeName()
        {
            //Console.WriteLine("DEBUG GetResourceTypeName={0}", _typeName);
            return _typeName;
        }
    }
}
