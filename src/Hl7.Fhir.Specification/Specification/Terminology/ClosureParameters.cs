using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Specification.Terminology
{
    public class ClosureParameters
    {
        /// <summary>
        /// The name that defines the particular context for the subsumption based closure table.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Concepts to add to the closure table.
        /// </summary>
        public Coding Concept { get; set; }
        /// <summary>
        /// A request to resynchronise - request to send all new entries since the nominated version was sent by the server.
        /// </summary>
        public string Version { get; set; }
    }
}
