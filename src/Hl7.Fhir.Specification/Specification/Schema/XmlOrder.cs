using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl7.Fhir.Specification.Schema
{
    public class XmlOrder : SimpleAssertion
    {
        public readonly int Order;

        public XmlOrder(int order)
        {
            Order = order;
        }

        protected override string Key => "xml-order";

        protected override object Value => Order;

        public override Assertions Validate(IElementNavigator input, ValidationContext vc) => Assertions.Empty;
    }
}