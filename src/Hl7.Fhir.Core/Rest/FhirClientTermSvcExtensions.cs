using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// Collection of the valid Operation values supported as const strings
    /// </summary>
    /// <remarks>
    /// This static class is required to keep the operation const values
    /// separate from the methods in the Fhir Client.
    /// Specifically the Meta operation clashes with the META const value.
    /// This would make it un-usable from VB.Net
    /// </remarks>
    public static partial class RestOperation
    {
        /// <summary>
        /// "expand" operation
        /// </summary>
        public const string EXPAND_VALUESET = "expand";

        /// <summary>
        /// "lookup" operation
        /// </summary>
        public const string CONCEPT_LOOKUP = "lookup";

        /// <summary>
        /// "validate-code" operation
        /// </summary>
        public const string VALIDATE_CODE = "validate-code";
    }

    public class ValidateCodeResult
    {
        public FhirBoolean Result;
        public FhirString Message;
        public FhirString Display;

        public static ValidateCodeResult FromParameters(Parameters p)
        {
            return new ValidateCodeResult
            {
                Result = p["result"].Value as FhirBoolean,
                Message = p["message"].Value as FhirString,
                Display = p["display"].Value as FhirString
            };
        }
    }



    public static class FhirClientTermSvcExtensions
    {
        #region Expand
        public static async Task<ValueSet> ExpandValueSetAsync(this FhirClient client, Uri valueset, FhirString filter = null, FhirDateTime date = null)
        {
            if (valueset == null) throw Error.ArgumentNull(nameof(valueset));

            var par = new Parameters();

            if (filter != null) par.Add("filter", filter);
            if (date != null) par.Add("date", date);

            ResourceIdentity id = new ResourceIdentity(valueset);

            return (await client.InstanceOperationAsync(id.WithoutVersion().MakeRelative(), RestOperation.EXPAND_VALUESET, par).ConfigureAwait(false))
                        .OperationResult<ValueSet>();
        }

        public static ValueSet ExpandValueSet(this FhirClient client, Uri valueset, FhirString filter = null,
            FhirDateTime date = null)
        {
            return ExpandValueSetAsync(client, valueset, filter, date).WaitResult();
        }

        public static async Task<ValueSet> ExpandValueSetAsync(this FhirClient client, FhirUri identifier, FhirString filter = null, FhirDateTime date = null)
        {
            if (identifier == null) throw Error.ArgumentNull(nameof(identifier));

            var par = new Parameters();

            par.Add("identifier", identifier);
            if (filter != null) par.Add("filter", filter);
            if (date != null) par.Add("date", date);

            return (await client.TypeOperationAsync<ValueSet>(RestOperation.EXPAND_VALUESET, par).ConfigureAwait(false))
                        .OperationResult<ValueSet>();
        }

        public static ValueSet ExpandValueSet(this FhirClient client, FhirUri identifier, FhirString filter = null,
            FhirDateTime date = null)
        {
            return ExpandValueSetAsync(client, identifier, filter, date).WaitResult();
        }

        public static async Task<ValueSet> ExpandValueSetAsync(this FhirClient client, ValueSet vs, FhirString filter = null, FhirDateTime date = null)
        {
            if (vs == null) throw Error.ArgumentNull(nameof(vs));

            var par = new Parameters().Add("valueSet", vs);
            if (filter != null) par.Add("filter", filter);
            if (date != null) par.Add("date", date);

            return (await client.TypeOperationAsync<ValueSet>(RestOperation.EXPAND_VALUESET, par).ConfigureAwait(false))
                    .OperationResult<ValueSet>();
        }

        public static ValueSet ExpandValueSet(this FhirClient client, ValueSet vs, FhirString filter = null,
            FhirDateTime date = null)
        {
            return ExpandValueSetAsync(client, vs, filter, date).WaitResult();
        }

        #endregion

        #region Concept Lookup

        public static async Task<Parameters> ConceptLookupAsync(this FhirClient client, Coding coding, FhirDateTime date = null)
        {
            if (coding == null) throw Error.ArgumentNull(nameof(coding));

            var par = new Parameters();
            par.Add("coding", coding);
            if (date != null) par.Add("date", date);

            return (await client.TypeOperationAsync<ValueSet>(RestOperation.CONCEPT_LOOKUP, par).ConfigureAwait(false))
                .OperationResult<Parameters>();
        }

        public static Parameters ConceptLookup(this FhirClient client, Coding coding, FhirDateTime date = null)
        {
            return ConceptLookupAsync(client, coding, date).WaitResult();
        }


        public static async Task<Parameters> ConceptLookupAsync(this FhirClient client, Code code, FhirUri system, FhirString version = null, FhirDateTime date = null)
        {
            if (code == null) throw Error.ArgumentNull(nameof(code));
            if (system == null) throw Error.ArgumentNull(nameof(system));

            var par = new Parameters().Add("code", code).Add("system", system);
            if (version != null) par.Add("version", version);
            if (date != null) par.Add("date", date);

            return (await client.TypeOperationAsync<ValueSet>(RestOperation.CONCEPT_LOOKUP, par).ConfigureAwait(false))
                .OperationResult<Parameters>();
        }

        public static Parameters ConceptLookup(this FhirClient client, Code code, FhirUri system,
            FhirString version = null, FhirDateTime date = null)
        {
            return ConceptLookupAsync(client, code, system, version, date).WaitResult();
        }

        #endregion

        #region Validate Code

        public static async Task<ValidateCodeResult> ValidateCodeAsync(this IFhirClient client, String valueSetId, 
                FhirUri identifier = null, FhirUri context = null, ValueSet valueSet = null, Code code = null,
                FhirUri system = null, FhirString version = null, FhirString display = null, 
                Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null,
                FhirBoolean @abstract = null)   
        {
            if (valueSetId == null) throw new ArgumentNullException(nameof(valueSetId));

            var par = new Parameters()
                .Add(nameof(identifier), identifier)
                .Add(nameof(context), context)
                .Add(nameof(valueSet), valueSet)
                .Add(nameof(code), code)
                .Add(nameof(system), system)
                .Add(nameof(version), version)
                .Add(nameof(display), display)
                .Add(nameof(coding), coding)
                .Add(nameof(codeableConcept), codeableConcept)
                .Add(nameof(date), date)
                .Add(nameof(@abstract), @abstract);

            ResourceIdentity location = new ResourceIdentity("ValueSet/" + valueSetId);
            var result = await client.InstanceOperationAsync(location.WithoutVersion().MakeRelative(), RestOperation.VALIDATE_CODE, par).ConfigureAwait(false);
                
            return ValidateCodeResult.FromParameters(result.OperationResult<Parameters>());
        }

        public static ValidateCodeResult ValidateCode(this IFhirClient client, String valueSetId,
                FhirUri identifier = null, FhirUri context = null, ValueSet valueSet = null, Code code = null,
                FhirUri system = null, FhirString version = null, FhirString display = null,
                Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null,
                FhirBoolean @abstract = null)
        {
            return ValidateCodeAsync(client, valueSetId, identifier, context, valueSet, code, system, version, display,
                coding, codeableConcept, date, @abstract).WaitResult();
        }

        public async static Task<ValidateCodeResult> ValidateCodeAsync(this IFhirClient client,
                FhirUri identifier = null, FhirUri context = null, ValueSet valueSet = null, Code code = null,
                FhirUri system = null, FhirString version = null, FhirString display = null,
                Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null,
                FhirBoolean @abstract = null)
        {
            var par = new Parameters()
                .Add(nameof(identifier), identifier)
                .Add(nameof(context), context)
                .Add(nameof(valueSet), valueSet)
                .Add(nameof(code), code)
                .Add(nameof(system), system)
                .Add(nameof(version), version)
                .Add(nameof(display), display)
                .Add(nameof(coding), coding)
                .Add(nameof(codeableConcept), codeableConcept)
                .Add(nameof(date), date)
                .Add(nameof(@abstract), @abstract);

            var result = await client.TypeOperationAsync<ValueSet>(RestOperation.VALIDATE_CODE, par).ConfigureAwait(false);
            return ValidateCodeResult.FromParameters(result.OperationResult<Parameters>());
        }

        public static ValidateCodeResult ValidateCode(this IFhirClient client,
                FhirUri identifier = null, FhirUri context = null, ValueSet valueSet = null, Code code = null,
                FhirUri system = null, FhirString version = null, FhirString display = null,
                Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null,
                FhirBoolean @abstract = null)
        {
            return ValidateCodeAsync(client, identifier, context, valueSet, code, system, version, display,
                        coding, codeableConcept, date, @abstract).WaitResult();
        }


        #endregion
    }
}
