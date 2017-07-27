using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    internal struct ElementValue
    {
        public ElementValue(string name, bool isMember, object value)
        {
            ElementName = name;
            IsCollectionMember = isMember;
            Value = value;
        }

        public string ElementName;
        public bool IsCollectionMember;
        public object Value;
    }
}
