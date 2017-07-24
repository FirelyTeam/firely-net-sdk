/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Rest
{
    public static class FhirClientOperations
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
        public static class Operation
        {
            /// <summary>
            /// "translate" operation
            /// </summary>
            public const string TRANSLATE = "translate"; 
            
            /// <summary>
            /// "everything" operation
            /// </summary>
            public const string FETCH_PATIENT_RECORD = "everything";

            /// <summary>
            /// "expand" operation
            /// </summary>
            public const string EXPAND_VALUESET = "expand";

            /// <summary>
            /// "lookup" operation
            /// </summary>
            public const string CONCEPT_LOOKUP = "lookup";

            /// <summary>
            /// "validate" operation
            /// </summary>
            public const string VALIDATE_RESOURCE = "validate";

            /// <summary>
            /// "meta" operation
            /// </summary>
            public const string META = "meta";

            /// <summary>
            /// "meta-add" operation
            /// </summary>
            public const string META_ADD = "meta-add";

            /// <summary>
            /// "meta-delete" operation
            /// </summary>
            public const string META_DELETE = "meta-delete";

            /// <summary>
            /// "validate-code" operation
            /// </summary>
            public const string VALIDATE_CODE = "validate-code";
        }


        #region Validate (Create/Update/Delete/Resource)

        public static async Task<OperationOutcome> ValidateCreateAsync(this FhirClient client, DomainResource resource, FhirUri profile = null)
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));

            var par = new Parameters().Add("resource", resource).Add("mode", new Code("create"));
            if (profile != null) par.Add("profile", profile);

            return expect<OperationOutcome>(await client.TypeOperationAsync(Operation.VALIDATE_RESOURCE, resource.TypeName, par).ConfigureAwait(false));
        }
        public static OperationOutcome ValidateCreate(this FhirClient client, DomainResource resource,
            FhirUri profile = null)
        {
            return ValidateCreateAsync(client, resource, profile).WaitResult();
        }

        public static async Task<OperationOutcome> ValidateUpdateAsync(this FhirClient client, DomainResource resource, string id, FhirUri profile = null)
        {
            if (id == null) throw Error.ArgumentNull(nameof(id));
            if (resource == null) throw Error.ArgumentNull(nameof(resource));

            var par = new Parameters().Add("resource", resource).Add("mode", new Code("update"));
            if (profile != null) par.Add("profile", profile);

            var loc = ResourceIdentity.Build(resource.TypeName, id);
            return expect<OperationOutcome>(await client.InstanceOperationAsync(loc, Operation.VALIDATE_RESOURCE, par).ConfigureAwait(false));
        }
        public static OperationOutcome ValidateUpdate(this FhirClient client, DomainResource resource, string id,
            FhirUri profile = null)
        {
            return ValidateUpdateAsync(client, resource, id, profile).WaitResult();
        }


        public static async Task<OperationOutcome> ValidateDeleteAsync(this FhirClient client, ResourceIdentity location)
        {
            if (location == null) throw Error.ArgumentNull(nameof(location));

            var par = new Parameters().Add("mode", new Code("delete"));

            return expect<OperationOutcome>(await client.InstanceOperationAsync(location.WithoutVersion().MakeRelative(), Operation.VALIDATE_RESOURCE, par).ConfigureAwait(false));
        }
        public static OperationOutcome ValidateDelete(this FhirClient client, ResourceIdentity location)
        {
            return ValidateDeleteAsync(client,location).WaitResult();
        }

        public static async Task<OperationOutcome> ValidateResourceAsync(this FhirClient client, DomainResource resource, string id = null, FhirUri profile = null)
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));

            var par = new Parameters().Add("resource", resource);
            if (profile != null) par.Add("profile", profile);

            if (id == null)
            {
                return expect<OperationOutcome>(await client.TypeOperationAsync(Operation.VALIDATE_RESOURCE, resource.TypeName, par).ConfigureAwait(false));
            }
            else
            {
                var loc = ResourceIdentity.Build(resource.TypeName, id);
                return expect<OperationOutcome>(await client.InstanceOperationAsync(loc, Operation.VALIDATE_RESOURCE, par).ConfigureAwait(false));
            }
        }

        public static OperationOutcome ValidateResource(this FhirClient client, DomainResource resource,
            string id = null, FhirUri profile = null)
        {
            return ValidateResourceAsync(client, resource, id, profile).WaitResult();
        }

        #endregion

        #region Fetch

        public static async Task<Bundle> FetchPatientRecordAsync(this FhirClient client, Uri patient = null, FhirDateTime start = null, FhirDateTime end = null)
        {
            var par = new Parameters();

            if (start != null) par.Add("start", start);
            if (end != null) par.Add("end", end);
            
            Resource result;
            if (patient == null)
                result = await client.TypeOperationAsync<Patient>(Operation.FETCH_PATIENT_RECORD, par).ConfigureAwait(false);
            else
            {
                var location = new ResourceIdentity(patient);
                result = await client.InstanceOperationAsync(location.WithoutVersion().MakeRelative(), Operation.FETCH_PATIENT_RECORD, par).ConfigureAwait(false);
            }

            return expect<Bundle>(result);
        }
        public static Bundle FetchPatientRecord(this FhirClient client, Uri patient = null, FhirDateTime start = null,
            FhirDateTime end = null)
        {
            return FetchPatientRecordAsync(client, patient, start, end).WaitResult();
        }

        #endregion
        
        #region Expand

        public static async Task<ValueSet> ExpandValueSetAsync(this FhirClient client, Uri valueset, FhirString filter = null, FhirDateTime date = null)
        {
            if (valueset == null) throw Error.ArgumentNull(nameof(valueset));

            var par = new Parameters();

            if (filter != null) par.Add("filter", filter);
            if (date != null) par.Add("date", date);

            ResourceIdentity id = new ResourceIdentity(valueset);

            return expect<ValueSet>(await client.InstanceOperationAsync(id.WithoutVersion().MakeRelative(), Operation.EXPAND_VALUESET, par).ConfigureAwait(false));
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
            
            return expect<ValueSet>(await client.TypeOperationAsync<ValueSet>(Operation.EXPAND_VALUESET, par).ConfigureAwait(false));
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

            return expect<ValueSet>(await client.TypeOperationAsync<ValueSet>(Operation.EXPAND_VALUESET, par).ConfigureAwait(false));
        }

        public static ValueSet ExpandValueSet(this FhirClient client, ValueSet vs, FhirString filter = null,
            FhirDateTime date = null)
        {
            return ExpandValueSetAsync(client, vs, filter, date).WaitResult();
        }

        #endregion

        #region Concept Lookup

        public static async Task<Parameters> ConceptLookupAsync(this FhirClient client, Coding coding, FhirDateTime date=null)
        {
            if (coding == null) throw Error.ArgumentNull(nameof(coding));

            var par = new Parameters();
            par.Add("coding", coding);
            if (date != null) par.Add("date", date);

            return expect<Parameters>(await client.TypeOperationAsync<CodeSystem>(Operation.CONCEPT_LOOKUP, par).ConfigureAwait(false));
        }

        public static Parameters ConceptLookup(this FhirClient client, Coding coding, FhirDateTime date = null)
        {
            return ConceptLookupAsync(client, coding, date).WaitResult();
        }


        public static async Task<Parameters> ConceptLookupAsync(this FhirClient client, Code code, FhirUri system, FhirString version = null, FhirDateTime date = null)
        {
            if (code == null) throw Error.ArgumentNull(nameof(code));
            if (system == null) throw Error.ArgumentNull(nameof(system));

            var par = new Parameters().Add("code",code).Add("system",system);
            if (version != null) par.Add("version", version);
            if (date != null) par.Add("date", date);

            return expect<Parameters>(await client.TypeOperationAsync<CodeSystem>(Operation.CONCEPT_LOOKUP, par).ConfigureAwait(false));
        }

        public static Parameters ConceptLookup(this FhirClient client, Code code, FhirUri system,
            FhirString version = null, FhirDateTime date = null)
        {
            return ConceptLookupAsync(client, code, system, version, date).WaitResult();
        }

        #endregion
        
        #region Meta

        //[base]/$meta
        public static async Task<Meta> MetaAsync(this FhirClient client)
        {
            return extractMeta(expect<Parameters>(await client.WholeSystemOperationAsync(Operation.META, useGet:true).ConfigureAwait(false)));
        }
        public static Meta Meta(this FhirClient client)
        {
            return MetaAsync(client).WaitResult();
        }
        
        //[base]/Resource/$meta
        public static async Task<Meta> MetaAsync(this FhirClient client, ResourceType type)
        {             
            return extractMeta(expect<Parameters>(await client.TypeOperationAsync(Operation.META, type.ToString(), useGet: true).ConfigureAwait(false)));
        }
        public static Meta Meta(this FhirClient client, ResourceType type)
        {
            return MetaAsync(client, type).WaitResult();
        }

        //[base]/Resource/id/$meta/[_history/vid]
        public static async Task<Meta> MetaAsync(this FhirClient client, Uri location)
        {
            Resource result;
            result = await client.InstanceOperationAsync(location, Operation.META, useGet: true).ConfigureAwait(false);

            return extractMeta(expect<Parameters>(result));
        }
        public static Meta Meta(this FhirClient client, Uri location)
        {
            return MetaAsync(client, location).WaitResult();
        }

        public static Task<Meta> MetaAsync(this FhirClient client, string location)
        {
            return MetaAsync(client, new Uri(location, UriKind.RelativeOrAbsolute));
        }
        public static Meta Meta(this FhirClient client, string location)
        {
            return MetaAsync(client, location).WaitResult();
        }

        public static async Task<Meta> AddMetaAsync(this FhirClient client, Uri location, Meta meta)
        {
            var par = new Parameters().Add("meta", meta);
            return extractMeta(expect<Parameters>(await client.InstanceOperationAsync(location, Operation.META_ADD, par).ConfigureAwait(false)));
        }
        public static Meta AddMeta(this FhirClient client, Uri location, Meta meta)
        {
            return AddMetaAsync(client, location, meta).WaitResult();
        }
        
        public static Task<Meta> AddMetaAsync(this FhirClient client, string location, Meta meta)
        {
            return AddMetaAsync(client, new Uri(location, UriKind.RelativeOrAbsolute), meta);
        }
        public static Meta AddMeta(this FhirClient client, string location, Meta meta)
        {
            return AddMetaAsync(client, location, meta).WaitResult();
        }


        public static async Task<Meta> DeleteMetaAsync(this FhirClient client, Uri location, Meta meta)
        {
            var par = new Parameters().Add("meta", meta);
            return extractMeta(expect<Parameters>(await client.InstanceOperationAsync(location, Operation.META_DELETE, par).ConfigureAwait(false)));
        }

        public static Meta DeleteMeta(this FhirClient client, Uri location, Meta meta)
        {
            return DeleteMetaAsync(client, location, meta).WaitResult();
        }

        public static Task<Meta> DeleteMetaAsync(this FhirClient client, string location, Meta meta)
        {
            return DeleteMetaAsync(client, new Uri(location, UriKind.RelativeOrAbsolute), meta);
        }

        public static Meta DeleteMeta(this FhirClient client, string location, Meta meta)
        {
            return DeleteMetaAsync(client, location, meta).WaitResult();
        }

        #endregion

        #region Validate Code

        public static Task<Parameters> ValidateCodeAsync(this IFhirClient client, String valueSetId, FhirUri system, 
            Code code, FhirString display = null, FhirBoolean abstractAllowed = null)
        {
            if (valueSetId == null) throw new ArgumentNullException(nameof(valueSetId));
            if (code == null) throw new ArgumentNullException(nameof(code));
            if (system == null) throw new ArgumentNullException(nameof(system));

            var par = new Parameters().Add("code", code).Add("system", system);

            if (display != null)
                par.Add("display", display);

            if (abstractAllowed != null)
                par.Add("abstract", abstractAllowed);

            return validateCodeForValueSetIdAsync(client, valueSetId, par);
        }
        public static Parameters ValidateCode(this IFhirClient client, String valueSetId, FhirUri system, Code code,
            FhirString display = null, FhirBoolean abstractAllowed = null)
        {
            return ValidateCodeAsync(client, valueSetId, system, code, display, abstractAllowed).WaitResult();
        }

        public static Task<Parameters> ValidateCodeAsync(this IFhirClient client, ValueSet vs, Coding coding, FhirBoolean abstractAllowed = null)
        {
            if (vs == null) throw Error.ArgumentNull(nameof(vs));
            if (coding == null) throw new ArgumentNullException("coding");

            var par = new Parameters()
                .Add("coding", coding)
                .Add("valueSet", vs);

            if (abstractAllowed != null)
                par.Add("abstract", abstractAllowed);

            return validateCodeForValueSetAsync(client, par);
        }

        public static Parameters ValidateCode(this IFhirClient client, ValueSet vs, Coding coding, FhirBoolean abstractAllowed = null)
        {
            return ValidateCodeAsync(client, vs, coding, abstractAllowed).WaitResult();
        }

        public static Task<Parameters> ValidateCodeAsync(this IFhirClient client, FhirUri canonical, Coding coding, FhirBoolean abstractAllowed = null)
        {
            if (canonical == null) throw Error.ArgumentNull(nameof(canonical));
            if (coding == null) throw new ArgumentNullException("coding");

            var par = new Parameters()
                .Add("identifier", canonical)           // TODO: Becomes "url" in STU3!
                .Add("coding", coding);

            if (abstractAllowed != null)
                par.Add("abstract", abstractAllowed);

            return validateCodeForValueSetAsync(client, par);
        }

        public static Parameters ValidateCode(this IFhirClient client, FhirUri canonical, Coding coding, FhirBoolean abstractAllowed = null)
        {
            return ValidateCodeAsync(client, canonical, coding, abstractAllowed).WaitResult();
        }


        public static Task<Parameters> ValidateCodeAsync(this IFhirClient client, String valueSetId, Coding coding, FhirBoolean abstractAllowed = null)
        {
            if (coding == null) throw new ArgumentNullException("coding");

            var par = new Parameters().Add("coding", coding);
            if (abstractAllowed != null)
                par.Add("abstract", abstractAllowed);

            return validateCodeForValueSetIdAsync(client, valueSetId, par);
        }

        public static Parameters ValidateCode(this IFhirClient client, String valueSetId, Coding coding, FhirBoolean abstractAllowed = null)
        {
            return ValidateCodeAsync(client, valueSetId, coding, abstractAllowed).WaitResult();
        }

        public static Task<Parameters> ValidateCodeAsync(this IFhirClient client, String valueSetId, CodeableConcept codeableConcept, FhirBoolean abstractAllowed = null)
        {
            if (codeableConcept == null) throw new ArgumentNullException("codeableConcept");

            var par = new Parameters().Add("codeableConcept", codeableConcept);
            if (abstractAllowed != null)
                par.Add("abstract", abstractAllowed);

            return validateCodeForValueSetIdAsync(client, valueSetId, par);
        }

        public static Parameters ValidateCode(this IFhirClient client, String valueSetId,
            CodeableConcept codeableConcept, FhirBoolean abstractAllowed = null)
        {
            return ValidateCodeAsync(client, valueSetId, codeableConcept, abstractAllowed).WaitResult();
        }

        #endregion

        #region Translate

        public class TranslateConceptDependency
        {
            public FhirUri Element;

            public CodeableConcept Concept;
        }


        public static async Task<Parameters> TranslateConceptAsync(this FhirClient client, string id, Code code, FhirUri system, FhirString version,
            FhirUri valueSet, Coding coding, CodeableConcept codeableConcept, FhirUri target, IEnumerable<TranslateConceptDependency> dependencies)
        {
            Parameters par = createTranslateConceptParams(code, system, version, valueSet, coding, codeableConcept, target, dependencies);
            var loc = ResourceIdentity.Build("ConceptMap", id);
            return expect<Parameters>(await client.InstanceOperationAsync(loc, Operation.TRANSLATE, par).ConfigureAwait(false));
        }
        public static Parameters TranslateConcept(this FhirClient client, string id, Code code, FhirUri system,
            FhirString version,
            FhirUri valueSet, Coding coding, CodeableConcept codeableConcept, FhirUri target,
            IEnumerable<TranslateConceptDependency> dependencies)
        {
            return TranslateConceptAsync(client, id, code, system, version, valueSet, coding, codeableConcept, target,
                dependencies).WaitResult();
        }


        public static async Task<Parameters> TranslateConceptAsync(this FhirClient client, Code code, FhirUri system, FhirString version,
            FhirUri valueSet, Coding coding, CodeableConcept codeableConcept, FhirUri target, IEnumerable<TranslateConceptDependency> dependencies )
        {
            Parameters par = createTranslateConceptParams(code, system, version, valueSet, coding, codeableConcept, target, dependencies);

            return expect<Parameters>(await client.TypeOperationAsync<ConceptMap>(Operation.TRANSLATE, par).ConfigureAwait(false));
        }

        public static Parameters TranslateConcept(this FhirClient client, Code code, FhirUri system, FhirString version,
            FhirUri valueSet, Coding coding, CodeableConcept codeableConcept, FhirUri target,
            IEnumerable<TranslateConceptDependency> dependencies)
        {
            return TranslateConceptAsync(client, code, system, version, valueSet, coding, codeableConcept, target,
                dependencies).WaitResult();
        }

        #endregion

        #region Private
        private static Parameters createTranslateConceptParams(Code code, FhirUri system, FhirString version, FhirUri valueSet, Coding coding, CodeableConcept codeableConcept,
            FhirUri target, IEnumerable<TranslateConceptDependency> dependencies)
        {
            if (target == null) throw new ArgumentNullException("target");

            var par = new Parameters().Add("target", target);

            if (code != null) par.Add("code", code);
            if (system != null) par.Add("system", system);
            if (version != null) par.Add("version", version);
            if (valueSet != null) par.Add("valueSet", valueSet);
            if (coding != null) par.Add("coding", coding);
            if (codeableConcept != null) par.Add("codeableConcept", codeableConcept);

            if (dependencies != null && dependencies.Any())
            {
                foreach (var dependency in dependencies)
                {
                    var dependencyPar = new List<Tuple<string, Base>>();

                    if (dependency.Element != null) dependencyPar.Add(Tuple.Create("element", (Base)dependency.Element));
                    if (dependency.Concept != null) dependencyPar.Add(Tuple.Create("concept", (Base)dependency.Concept));

                    par.Add("dependency", dependencyPar);
                }
            }

            return par;
        }

        private static T expect<T>(Resource result) where T : Resource
        {
            if (result is T)
                return (T)result;
            else
                throw Error.InvalidOperation("Operation did not return a " + typeof(T).Name + " but a " + result.GetType().Name);
        }
        private static Meta extractMeta(Parameters parms)
        {
            if (!parms.Parameter.IsNullOrEmpty())
            {
                var parm = parms.Parameter[0];
                if (parm != null)
                {
                    if (parm.Value is Meta)
                        return (Meta)parm.Value;
                }
            }

            return null;
        }

        private static async Task<Parameters> validateCodeForValueSetAsync(IFhirClient client, Parameters par)
        {
            return expect<Parameters>(await client.TypeOperationAsync<ValueSet>(Operation.VALIDATE_CODE, par).ConfigureAwait(false));
        }

        private static async Task<Parameters> validateCodeForValueSetIdAsync(IFhirClient client, string valueSetId, Parameters par)
        {
            ResourceIdentity location = new ResourceIdentity("ValueSet/" + valueSetId);

            return expect<Parameters>(await client.InstanceOperationAsync(location.WithoutVersion().MakeRelative(), Operation.VALIDATE_CODE, par).ConfigureAwait(false));
        }
        #endregion
    }
}
