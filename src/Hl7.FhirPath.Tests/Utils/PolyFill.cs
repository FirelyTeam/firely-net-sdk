using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hl7.FhirPath.Tests
{
    public static class PolyFill
    {
        public static string TryGetAttribute(this XElement element, string name, out bool exists)
        {
            XAttribute attr = element.Attributes().FirstOrDefault(a => a.Name == name);
            exists = (attr != null);
            return attr?.Value;
        }
    }

    public static class Test 
    {
        public static void Fail(string message)
        {
            throw new Exception("Test failed. "+ message);
        }

        public static bool IsInstanceOfType(object value, Type expectedType)
        {
            return value.GetType().GetTypeInfo().IsAssignableFrom(expectedType);
        }
    }
}
