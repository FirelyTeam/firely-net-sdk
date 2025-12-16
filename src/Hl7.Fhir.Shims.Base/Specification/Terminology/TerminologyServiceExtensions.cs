#nullable enable

/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public static class TerminologyServiceExtensions
    {
        public static Task<Parameters> ValueSetValidateCode(this ITerminologyService srv, ValidateCodeParameters parameters, string? id = null, bool useGet = false)
        {
            return srv.ValueSetValidateCode(parameters.Build(), id, useGet);
        }

        public static Task<Parameters> CodeSystemValidateCode(this ITerminologyService srv, ValidateCodeParameters parameters, string? id = null, bool useGet = false)
        {
            return srv.CodeSystemValidateCode(parameters.Build(), id, useGet);
        }

        public static Task<Resource> Expand(this ITerminologyService srv, ExpandParameters parameters, string? id = null, bool useGet = false)
        {
            return srv.Expand(parameters.Build(), id, useGet);
        }

        public static Task<Parameters> Lookup(this ITerminologyService srv, LookupParameters parameters, bool useGet = false)
        {
            return srv.Lookup(parameters.Build(), useGet);
        }

        public static Task<Parameters> Translate(this ITerminologyService srv, TranslateParameters parameters, string? id = null, bool useGet = false)
        {
            return srv.Translate(parameters.Build(), id, useGet);
        }

        public static Task<Parameters> Subsumes(this ITerminologyService srv, SubsumesParameters parameters, string? id = null, bool useGet = false)
        {
            return srv.Subsumes(parameters.Build(), id, useGet);
        }

        public static Task<Resource> Closure(this ITerminologyService srv, ClosureParameters parameters, bool useGet = false)
        {
            return srv.Closure(parameters.Build(), useGet);
        }
    }
}

#nullable restore