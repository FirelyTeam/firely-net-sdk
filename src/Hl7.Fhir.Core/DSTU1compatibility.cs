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

    /// <summary>
    /// The ResourceEntry class has been made obsolete, please change to the class Bundle.EntryComponent instead
    /// </summary>
    [Obsolete("The ResourceEntry class has been made obsolete, please change to the class Bundle.EntryComponent instead", true)]
    public class ResourceEntry : Bundle.EntryComponent
    {
    }

    /// <summary>
    /// The ResourceEntry class has been made obsolete, please change to the class Bundle.EntryComponent instead
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("The ResourceEntry class has been made obsolete, please change to the class Bundle.EntryComponent instead", true)]
    public class ResourceEntry<T> : Bundle.EntryComponent where T : Resource
    {
        /// <summary>
        /// The ResourceEntry class has been made obsolete, please change to the class Bundle.EntryComponent instead
        /// </summary>
        public new T Resource { get; set; }
    }

    /// <summary>
    /// The 'Alert' resource was renamed to 'Flag' in DSTU2
    /// </summary>
    [Obsolete("The 'Alert' resource was renamed to 'Flag' in DSTU2", true)]
    public partial class Alert : Flag
    {
    }

    public partial class Flag
    {
        /// <summary>
        /// This property was renamed to 'Code' in DSTU2, and re-typed from string to CodeableConcept. When using un-coded values, just populate the text property of the codeableconcept
        /// </summary>
        [Obsolete("This property was renamed to 'Code' in DSTU2, and re-typed from string to CodeableConcept. When using un-coded values, just populate the text property of the codeableconcept", true)]
        public CodeableConcept Note { get; set; }
    }

    public partial class Observation
    {
        /// <summary>
        /// This property was renamed to 'Code' in DSTU2
        /// </summary>
        [Obsolete("This property was renamed to 'Code' in DSTU2", true)]
        public CodeableConcept Name { get; set; }

        /// <summary>
        /// This property was renamed to 'Effective' in DSTU2
        /// </summary>
        [Obsolete("This property was renamed to 'Effective' in DSTU2", true)]
        public FhirDateTime Applies { get; set; }
    }
}