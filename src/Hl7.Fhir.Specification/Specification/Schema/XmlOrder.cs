using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl7.Fhir.Specification.Schema
{
    public class XmlOrder : IAssertion
    {
        public readonly int Order;

        public XmlOrder(int order)
        {
            Order = order;
        }

        public JToken ToJson() => new JProperty("xml-order", Order);
    }
}