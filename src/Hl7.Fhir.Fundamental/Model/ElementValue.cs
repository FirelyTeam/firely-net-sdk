/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.Model
{
    public struct ElementValue
    {
        public ElementValue(string name, Base value)
        {
            ElementName = name;
            Value = value;
        }

        public string ElementName;
        public Base Value;
    }
}
