using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Schema
{
    public interface ISchemaResolver
    {
        ElementSchema GetSchema(Uri schemaUri);
    }
}
