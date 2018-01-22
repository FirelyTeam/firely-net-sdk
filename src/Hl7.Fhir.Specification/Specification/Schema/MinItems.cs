using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Specification.Schema
{
    public class MaxItems : SimpleAssertion
    {
        public readonly int Max;

        public MaxItems(int max)
        {
            Max = max;
        }

        protected override string Key => "maxItems";

        protected override object Value => Max;

        public override Assertions Validate(IElementNavigator input, ValidationContext vc)
            => throw new NotImplementedException();
    }
}