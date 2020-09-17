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

        public Parameters ToParameters()
        {
            var result = new Parameters();

            if (!string.IsNullOrWhiteSpace(Name))
            {
                result.AddParameterComponent("name", new FhirString(Name));
            }

            if (Concept != null)
            {
                result.AddParameterComponent("concept", Concept);
            }

            if (!string.IsNullOrWhiteSpace(Version))
            {
                result.AddParameterComponent("version", new FhirString(Version));
            }

            return result;
        }
    }
}
