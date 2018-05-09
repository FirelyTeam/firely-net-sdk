using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    internal struct ElementValue
    {
        public ElementValue(string name, bool isMember,bool isChoice, bool isContained, object value)
        {
            ElementName = name;
            IsCollectionMember = isMember;
            IsChoice = isChoice;
            IsContained = isContained;
            Value = value;
        }

        public string ElementName;
        public bool IsCollectionMember;
        public object Value;
        public bool IsChoice;
        public bool IsContained;
    }
}
