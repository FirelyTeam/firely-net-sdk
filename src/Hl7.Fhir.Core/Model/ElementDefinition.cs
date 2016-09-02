/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Hl7.Fhir.Model;


namespace Hl7.Fhir.Model
{
    public partial class ElementDefinition 
    {
        public ElementDefinition()
        {

        }

        public ElementDefinition(string path)
        {
            Path = path;
        }
    }
}
