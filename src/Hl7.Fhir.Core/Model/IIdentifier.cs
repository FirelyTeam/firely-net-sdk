using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Common interface for different version-specific variants of Identifier
    /// </summary>
    public interface IIdentifier
    {
        /// <summary>
        /// usual | official | temp | secondary (If known)
        /// </summary>
        IdentifierUse? Use { get; }

        /// <summary>
        /// Description of identifier
        /// </summary>
        CodeableConcept Type { get; }

        /// <summary>
        /// The namespace for the identifier value
        /// </summary>
        string System { get; }

        /// <summary>
        /// The value that is unique
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Time period when id is/was valid for use
        /// </summary>
        Period Period { get; }

        /// <summary>
        /// Organization that issued id (may be just text)
        /// </summary>
        IResourceReference Assigner { get; }
    }

    /// <summary>
    /// Identifier class common accross different versions
    /// </summary>
    public class Identifier : IIdentifier
    {
        public Identifier()
        {
        }

        public Identifier(string system, string value)
        {
            System = system;
            Value = value;
        }

        /// <summary>
        /// usual | official | temp | secondary (If known)
        /// </summary>
        public IdentifierUse? Use { get; set; }

        /// <summary>
        /// Description of identifier
        /// </summary>
        public CodeableConcept Type { get; set; }

        /// <summary>
        /// The namespace for the identifier value
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// The value that is unique
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Time period when id is/was valid for use
        /// </summary>
        public Period Period { get; set; }

        /// <summary>
        /// Organization that issued id (may be just text)
        /// </summary>
        public IResourceReference Assigner { get; set; }
    }
}
