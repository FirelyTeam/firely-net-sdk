using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Hl7.Fhir.Specification.Source
{
    internal interface IConformanceScanner
    {
        Resource Retrieve(ArtifactSummary entry);
        List<ArtifactSummary> List();
    }
}