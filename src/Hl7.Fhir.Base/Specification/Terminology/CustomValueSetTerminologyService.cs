#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    /// <summary>
    /// Base class for checking Code terminology
    /// </summary>
    public abstract class CustomValueSetTerminologyService : ITerminologyService
    {
        private readonly string _terminologyType;
        private readonly string _codeSystem;
        private readonly string[] _codeValueSets;

        /// <summary>
        /// Base class for checking terminology of codes that are part of a value set.
        /// </summary>
        /// <param name="terminologyType">String representation of the code type which is being checked. Exclusively used for error messages</param>
        /// <param name="codeSystem">Name of the specification defining the members of the value set</param>
        /// <param name="codeValueSets">uri's of the definitions of the code system. This can be multiple, if a FHIR version has changed this at some point.</param>
        protected CustomValueSetTerminologyService(string terminologyType, string codeSystem, string[] codeValueSets)
        {
            _terminologyType = terminologyType;
            _codeSystem = codeSystem;
            _codeValueSets = codeValueSets;
        }
        
        ///<inheritdoc />
        public Tasks.Task<Resource> Closure(Parameters parameters, bool useGet = false) =>
            throw new NotImplementedException();

        ///<inheritdoc />
        public Tasks.Task<Parameters>
            CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false) =>
            throw new NotImplementedException();

        ///<inheritdoc />
        public Tasks.Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false) =>
            throw new NotImplementedException();

        ///<inheritdoc />
        public Tasks.Task<Parameters> Lookup(Parameters parameters, bool useGet = false) =>
            throw new NotImplementedException();

        ///<inheritdoc />
        public Tasks.Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false) =>
            throw new NotImplementedException();

        ///<inheritdoc />
        public Tasks.Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false) =>
            throw new NotImplementedException();

        ///<inheritdoc />
        public async Tasks.Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null,
            bool useGet = false)
        {
            parameters.CheckForValidityOfValidateCodeParams();

            var validCodeParams = new ValidateCodeParameters(parameters);
            var valueSetUri = validCodeParams?.Url?.Value != null
                ? new Canonical(validCodeParams?.Url?.Value).Uri
                : null;

            if (_codeValueSets.All(valueSet => valueSet != valueSetUri))
            {
                // 404 not found
                throw new FhirOperationException($"Cannot find valueset '{validCodeParams!.Url?.Value}'",
                    HttpStatusCode.NotFound);
            }

            try
            {
                if (validCodeParams!.CodeableConcept is { })
                    return await ValidateCodeVS(validCodeParams.CodeableConcept).ConfigureAwait(false);
                else if (validCodeParams.Coding is { })
                    return await ValidateCodeVS(validCodeParams.Coding).ConfigureAwait(false);
                else
                    return await ValidateCodeVS(validCodeParams.Code?.Value, validCodeParams.System?.Value)
                        .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //500 internal server error
                throw new FhirOperationException(e.Message, (HttpStatusCode)500);
            }

        }
        
        private async Task<Parameters> ValidateCodeVS(Coding coding)
        {
            return await ValidateCodeVS(coding.Code, coding.System).ConfigureAwait(false);
        }

        private async Task<Parameters> ValidateCodeVS(CodeableConcept cc)
        {
            var result = new Parameters();

            // Maybe just a text, but if there are no codings, that's a positive result
            if (!cc.Coding.Any())
            {
                result.Add("result", new FhirBoolean(true));
                return result;
            }

            // If we have just 1 coding, we better handle this using the simpler version of ValidateBinding
            if (cc.Coding.Count == 1)
                return await ValidateCodeVS(cc.Coding.Single()).ConfigureAwait(false);


            // Else, look for one succesful match in any of the codes in the CodeableConcept
            var callResults = await Task.WhenAll(cc.Coding.Select(coding => ValidateCodeVS(coding))).ConfigureAwait(false);
            var anySuccesful = callResults.Any(p => p.GetSingleValue<FhirBoolean>("result")?.Value == true);

            if (anySuccesful == false)
            {
                var messages = new StringBuilder();
                messages.AppendLine("None of the Codings in the CodeableConcept were valid for the binding. Details follow.");

                // gathering the messages of all calls
                foreach (var msg in callResults.Select(cr => cr.GetSingleValue<FhirString>("message")?.Value).Where(m => m is { }))
                {
                    messages.AppendLine(msg);
                }

                result.Add("message", new FhirString(messages.ToString()));
                result.Add("result", new FhirBoolean(false));
            }
            else
            {
                result.Add("result", new FhirBoolean(true));
            }

            return result;
        }

        private Task<Parameters> ValidateCodeVS(string? code, string? system)
        {
            var result = new Parameters();
            var systemUri = system != null ? new Canonical(system).Uri : null;


            if (systemUri == _codeSystem || systemUri == null)
            {
                if (code is null)
                {
                    result.Add("message", new FhirString("No code supplied."))
                          .Add("result", new FhirBoolean(false));
                }
                else
                {
                    var success = ValidateCodeType(code);

                    if (success)
                    {
                        result.Add("result", new FhirBoolean(true));
                    }
                    else
                    {
                        result.Add("result", new FhirBoolean(false))
                              .Add("message", new FhirString($"'{code}' is not a valid {_terminologyType}."));
                    }
                }
            }
            else
            {
                throw new FhirOperationException($"Unknown system '{systemUri}'", HttpStatusCode.NotFound);
            }
            return Task.FromResult(result);
        }
        abstract protected bool ValidateCodeType(string code);
    }
}