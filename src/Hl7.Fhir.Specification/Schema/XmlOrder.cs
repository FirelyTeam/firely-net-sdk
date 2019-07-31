/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Validation.Schema;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    internal class XmlOrder : IAssertion
    {
        private int _order;

        public XmlOrder(int xmlOrder)
        {
            _order = xmlOrder;
        }

        public JToken ToJson() => new JProperty("xml-order", _order);
    }
}