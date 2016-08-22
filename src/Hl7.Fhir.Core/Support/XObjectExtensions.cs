/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using System.Xml.Linq; 
 
namespace Hl7.Fhir.Support 
{ 
    public static class XObjectExtensions
    { 

        public static string TryGetAttribute(this XElement docNode, XName name, out bool hasValue)
        {  
            var valueAttribute = docNode.Attribute(name);

            if (valueAttribute != null) 
            { 
                hasValue = true; 
                return valueAttribute.Value; 
            } 
            else 
            { 
                hasValue = false; 
                return null; 
            } 
        } 
    } 
} 