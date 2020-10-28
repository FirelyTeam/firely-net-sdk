using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ClosureParameters
    {
        public ClosureParameters(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            Name = new FhirString(name);
        }

        /// <summary>
        /// The name that defines the particular context for the subsumption based closure table.
        /// </summary>
        public FhirString Name { get; private set; }
        /// <summary>
        /// Concepts to add to the closure table.
        /// </summary>
        public IEnumerable<Coding> Concept { get; private set; }
        /// <summary>
        /// A request to resynchronise - request to send all new entries since the nominated version was sent by the server.
        /// </summary>
        public FhirString Version { get; private set; }

        #region Builder methods
        public ClosureParameters WithConcepts(IEnumerable<Coding> codings)
        {
            Concept = codings;
            return this;
        }

        public ClosureParameters WithVersion(string version)
        {
            if (!string.IsNullOrWhiteSpace(version)) Version = new FhirString(version);
            return this;
        }
        #endregion

        public Parameters Build()
        {
            var result = new Parameters();

            if (Name is { }) result.Add("name", Name);

            foreach (var concept in Concept ?? Enumerable.Empty<Coding>())
            {
                result.Add("concept", concept);
            }

            if (Version is { }) result.Add("version", Version);

            return result;
        }
    }
}
