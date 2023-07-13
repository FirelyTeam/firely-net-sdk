using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification.Terminology
{
    public class TerminologyServiceSettings
    {
        public TerminologyServiceSettings() { }

        /// <summary>
        /// (Canonicals of) ValueSets for which this host is preferred to use. When there are no
        /// valueset preferred (the list is empty), then the service applies to all incoming valuesets.
        /// </summary>
        public string[] PreferredValueSets { get; set; }
        /// <summary>
        /// Order in which the host is to be used. Lower is preferred of higher.
        /// </summary>
        public int Order { get; set; }

        /// <summary>Clone constructor. Generates a new <see cref="TerminologyServiceSettings() "/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public TerminologyServiceSettings(TerminologyServiceSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="TerminologyServiceSettings() "/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(TerminologyServiceSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.Order = Order;
            other.PreferredValueSets = PreferredValueSets;

        }

        /// <summary>Creates a new <see cref="TerminologyServiceSettings"/> object that is a copy of the current instance.</summary>
        public TerminologyServiceSettings Clone() => new(this);

        /// <summary>Creates a new <see cref="TerminologyServiceSettings"/> instance with default property values.</summary>
        public static TerminologyServiceSettings CreateDefault() => new();
    }
}