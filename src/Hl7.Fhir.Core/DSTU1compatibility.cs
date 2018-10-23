using Hl7.Fhir.Introspection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    #region << DSTU2 Compatability >>
    public partial class StructureDefinition
    {
        /// <summary>
        /// Type defined or constrained by this structure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        [Obsolete("The ConstrainedType property was renamed, please change to use the Type", true)]
        public string ConstrainedType
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Type");
            }
        }

    }

    [Obsolete("The FHIRDefinedType enumeration was renamed, please change to FHIRAllTypes", true)]
    public enum FHIRDefinedType
    {
    }

    public partial class StructureDefinition
    {
#if !PORTABLE45
        [System.ComponentModel.Browsable(false)]
#endif
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        [Obsolete("Base was renamed to BaseDefinition", true)]
        public string Base { get; set; }
    }

    public partial class ElementDefinition
    {
#if !PORTABLE45
        [System.ComponentModel.Browsable(false)]
#endif
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        [Obsolete("NameReference was renamed to ContentReference", true)]
        public string NameReference { get; set; }
    }
    #endregion

    #region << DSTU1 Compatability >>
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
#if !PORTABLE45
        [System.ComponentModel.Browsable(false)]
#endif
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
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
#if !PORTABLE45
        [System.ComponentModel.Browsable(false)]
#endif
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        [Obsolete("This property was renamed to 'Code' in DSTU2, and re-typed from string to CodeableConcept. When using un-coded values, just populate the text property of the codeableconcept", true)]
        public CodeableConcept Note { get; set; }
    }

    public partial class Observation
    {
        /// <summary>
        /// This property was renamed to 'Code' in DSTU2
        /// </summary>
#if !PORTABLE45
        [System.ComponentModel.Browsable(false)]
#endif
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        [Obsolete("This property was renamed to 'Code' in DSTU2", true)]
        public CodeableConcept Name { get; set; }

        /// <summary>
        /// This property was renamed to 'Effective' in DSTU2
        /// </summary>
#if !PORTABLE45
        [System.ComponentModel.Browsable(false)]
#endif
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        [Obsolete("This property was renamed to 'Effective' in DSTU2", true)]
        public FhirDateTime Applies { get; set; }
    }
    #endregion
}