using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;

namespace Hl7.Fhir.Serialization
{
    class TurtleReusableNodes
    {
        public IUriNode nodeRole;
        public IUriNode treeRoot;
        public IUriNode type;
        public IUriNode value;

        public TurtleReusableNodes(IGraph g)
        {
            nodeRole = g.CreateUriNode("fhir:nodeRole");
            treeRoot = g.CreateUriNode("fhir:treeRoot");
            type = g.CreateUriNode("rdf:type");
            value = g.CreateUriNode("fhir:value");
        }

    }
}
