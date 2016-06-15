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