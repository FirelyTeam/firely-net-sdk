using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace Hl7.Fhir.Serialization
{
    class TurtleReusableNodes
    {
        public IUriNode nodeRole;
        public IUriNode treeRoot;
        public IUriNode type;
        public IUriNode value;
        public IUriNode index;

        public Uri dtDateTime = new Uri(XmlSpecsHelper.XmlSchemaDataTypeDateTime);
        public Uri dtDecimal = new Uri(XmlSpecsHelper.XmlSchemaDataTypeDecimal);
        public Uri dtInteger = new Uri(XmlSpecsHelper.XmlSchemaDataTypeInteger);

        public TurtleReusableNodes(IGraph g)
        {
            nodeRole = g.CreateUriNode("fhir:nodeRole");
            treeRoot = g.CreateUriNode("fhir:treeRoot");
            type = g.CreateUriNode("rdf:type");
            value = g.CreateUriNode("fhir:value");
            index = g.CreateUriNode("fhir:index");
        }

    }
}
