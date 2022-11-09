using Hl7.Fhir.Model;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public static class TerminologyServiceExtensions
    {
        public static async Task<Parameters> ValueSetValidateCode(this ITerminologyService srv, ValidateCodeParameters parameters, string id = null, bool useGet = false)
        {
            return await srv.ValueSetValidateCode(parameters.Build(), id, useGet).ConfigureAwait(false);
        }

        public static async Task<Parameters> CodeSystemValidateCode(this ITerminologyService srv, ValidateCodeParameters parameters, string id = null, bool useGet = false)
        {
            return await srv.CodeSystemValidateCode(parameters.Build(), id, useGet).ConfigureAwait(false);
        }

        public static async Task<Resource> Expand(this ITerminologyService srv, ExpandParameters parameters, string id = null, bool useGet = false)
        {
            return await srv.Expand(parameters.Build(), id, useGet).ConfigureAwait(false);
        }

        public static async Task<Parameters> Lookup(this ITerminologyService srv, LookupParameters parameters, bool useGet = false)
        {
            return await srv.Lookup(parameters.Build(), useGet).ConfigureAwait(false);
        }
        public static async Task<Parameters> Translate(this ITerminologyService srv, TranslateParameters parameters, string id = null, bool useGet = false)
        {
            return await srv.Translate(parameters.Build(), id, useGet).ConfigureAwait(false);
        }

        public static async Task<Parameters> Subsumes(this ITerminologyService srv, SubsumesParameters parameters, string id = null, bool useGet = false)
        {
            return await srv.Subsumes(parameters.Build(), id, useGet).ConfigureAwait(false);
        }

        public static async Task<Resource> Closure(this ITerminologyService srv, ClosureParameters parameters, bool useGet = false)
        {
            return await srv.Closure(parameters.Build(), useGet).ConfigureAwait(false);
        }


    }
}
