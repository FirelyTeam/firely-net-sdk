using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Specification.Schema
{
    public class MinItems : SimpleAssertion
    {
        public readonly int Min;

        public MinItems(int min)
        {
            Min = min;
        }

        protected override string Key => "minItems";

        protected override object Value => Min;

        public override Assertions Validate(IElementNavigator input, ValidationContext vc)
            => throw new NotImplementedException();
    }
}