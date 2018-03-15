using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public class PocoModelMetadataProvider : IModelMetadataProvider
    {
        //TODO: cache this
        public static PocoModelMetadataProvider Default => new PocoModelMetadataProvider();

        public IComplexTypeSerializationInfo GetSerializationInfoForType(string typeName)
        {
            throw new NotImplementedException();
        }

        public bool IsResource(string typeName)
        {
            throw new NotImplementedException();
        }
    }
}
