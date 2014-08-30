using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhir.Profiling
{
    public static class UriHelper
    {
        public static Uri TypeUri(string type)
        {
            string name = type.ToLower(); // todo: this is a temporary fix!!!
            return new Uri("http://hl7.org/fhir/profile/" + name);
        }

        public static Uri ResolvingUri(TypeRef typeref)
        {
            if (typeref.ProfileUri == null)
            {
                return UriHelper.TypeUri(typeref.Code);
            }
            else
            {
                try
                {
                    return new Uri(typeref.ProfileUri);
                }
                catch
                {
                    return null;
                }
            }
        }
        public static Uri ResolvingUri(this Structure structure)
        {
            return TypeUri(structure.Type);
        }
    }
}
