using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The Query class from DSTU1 has been replaced by the 
    /// </summary>
    [Obsolete("The Query class has been made obsolete, please change to the class Parameters instead", true)]
    public class Query : Parameters
    {
    }

    [Obsolete("The ResourceEntry class has been made obsolete, please change to the class Bundle.BundleEntryComponent instead", true)]
    public class ResourceEntry<T> : Bundle.BundleEntryComponent where T : Resource
    {
    }
}