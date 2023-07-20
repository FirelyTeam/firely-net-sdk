#nullable enable

using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification.Terminology
{
    /// <summary>
    /// Settings to manage routing mechanism of an <see cref="ITerminologyService"/>.
    /// </summary>
    public class TerminologyServiceRoutingSettings
    {
        /// <summary>
        /// Settings to manage routing mechanism of an <see cref="ITerminologyService"/>.
        /// <param name="service"><see cref="ITerminologyService"/> to be managed</param>
        /// </summary>
        public TerminologyServiceRoutingSettings(ITerminologyService service)
        {
            PreferredValueSets = Array.Empty<string>();
            Service = service;
        }
        /// <summary>
        /// The <see cref="ITerminologyService"/> these routing settings are about
        /// </summary>
        public ITerminologyService Service { get; }

        /// <summary>
        /// (Canonicals of) ValueSets for which this service is preferred to use. When there are no
        /// valueset preferred (the list is empty), then the service applies to all incoming valuesets.
        /// </summary>
        public string[] PreferredValueSets { get; set; }

        /// <summary>Clone constructor. Generates a new <see cref="TerminologyServiceRoutingSettings "/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public TerminologyServiceRoutingSettings(TerminologyServiceRoutingSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
            Service = other.Service;
            PreferredValueSets = (string[])other.PreferredValueSets.Clone();
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="TerminologyServiceRoutingSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(TerminologyServiceRoutingSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.PreferredValueSets = (string[])PreferredValueSets.Clone();
        }

        /// <summary>Creates a new <see cref="TerminologyServiceRoutingSettings"/> object that is a copy of the current instance.</summary>
        public TerminologyServiceRoutingSettings Clone() => new(this);

        /// <summary>Creates a new <see cref="TerminologyServiceRoutingSettings"/> instance with default property values.</summary>
        public static TerminologyServiceRoutingSettings CreateDefault(ITerminologyService service) => new(service);
    }
}

#nullable restore