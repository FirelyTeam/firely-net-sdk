using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.FluentPath.InstanceTree
{
    internal class UntypedValue : IValueProvider
    {
        public string Representation { get; private set; }  

        public object Value
        {
            get;  private set;
        }

        public IElementNavigator Parent
        {
            get
            {
                return null;
            }
        }

        private static object parseValue(string rep)
        {
            if (rep.ToLower() == "true") return true;

            if (rep.ToLower() == "false") return false;

            if (rep.Contains("-") || rep.Contains(":"))
            {
                PartialDateTime dt;
                if (PartialDateTime.TryParse(rep, out dt)) return dt;
            }

            if (rep.Contains("."))
            {
                try
                {
                    return XmlConvert.ToDecimal(rep);
                }
                catch
                {
                    ;  // Fall through to next case
                }
            }

            try
            {
                return XmlConvert.ToInt64(rep);
            }
            catch
            {
                ; // Fall through to next case
            }

            return rep;     // If all else fails, it's probably just a string
        }

        public UntypedValue(string representation)
        {
            Representation = representation;
            //Value = parseValue(Representation);
            Value = representation;
        }

        public override string ToString()
        {
            return Representation;
        }

        //public IEnumerable<ChildNode> Children()
        //{
        //    return Enumerable.Empty<ChildNode>();
        //}
    }
}
