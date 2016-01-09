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
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

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

        public static Bundle FetchPatientRecord(this FhirClient client, Uri patient = null, FhirDateTime start = null, FhirDateTime end = null)
        {
            var par = new Parameters();

            if (start != null) par.Add("start", start);
            if (end != null) par.Add("end", end);
            
            Resource result;
            if (patient == null)
                result = client.TypeOperation<Patient>(Operation.FETCH_PATIENT_RECORD, par);
            else
            {
                var location = new ResourceIdentity(patient);
                result = client.InstanceOperation(location.WithoutVersion().MakeRelative(), Operation.FETCH_PATIENT_RECORD, par);
            }

            return expect<Bundle>(result);
        }


        private static T expect<T>(Resource result) where T : Resource
        {
            if (result is T)
                return (T)result;
            else
                throw Error.InvalidOperation("Operation did not return a " + typeof(T).Name + " but a " + result.GetType().Name);
        }


        public static ValueSet ExpandValueSet(this FhirClient client, Uri valueset, FhirString filter = null, FhirDateTime date = null)
        {
            if (valueset == null) throw Error.ArgumentNull("valuesetLocation");

            var par = new Parameters();

            if (filter != null) par.Add("filter", filter);
            if (date != null) par.Add("date", date);

            ResourceIdentity id = new ResourceIdentity(valueset);

            return expect<ValueSet>(client.InstanceOperation(id.WithoutVersion().MakeRelative(), Operation.EXPAND_VALUESET, par));
        }


        public static ValueSet ExpandValueSet(this FhirClient client, FhirUri identifier, FhirString filter = null, FhirDateTime date = null)
        {
            if (identifier == null) throw Error.ArgumentNull("identifier");

            var par = new Parameters();

            par.Add("identifier", identifier);
            if (filter != null) par.Add("filter", filter);
            if (date != null) par.Add("date", date);
            
            return expect<ValueSet>(client.TypeOperation<ValueSet>(Operation.EXPAND_VALUESET, par));
        }


        public static ValueSet ExpandValueSet(this FhirClient client, ValueSet vs, FhirString filter = null, FhirDateTime date = null)
        {
            if (vs == null) throw Error.ArgumentNull("vs");

            var par = new Parameters().Add("valueSet", vs);
            if (filter != null) par.Add("filter", filter);
            if (date != null) par.Add("date", date);

            return expect<ValueSet>(client.TypeOperation<ValueSet>(Operation.EXPAND_VALUESET, par));
        }

        public static Parameters ConceptLookup(this FhirClient client, Coding coding, FhirDateTime date=null)
        {
            if (coding == null) throw Error.ArgumentNull("coding");

            var par = new Parameters();
            par.Add("coding", coding);
            if (date != null) par.Add("date", date);

            return expect<Parameters>(client.TypeOperation<ValueSet>(Operation.CONCEPT_LOOKUP, par));
        }

        public static Parameters ConceptLookup(this FhirClient client, Code code, FhirUri system, FhirString version = null, FhirDateTime date = null)
        {
            if (code == null) throw Error.ArgumentNull("code");
            if (system == null) throw Error.ArgumentNull("system");

            var par = new Parameters().Add("code",code).Add("system",system);
            if (version != null) par.Add("version", version);
            if (date != null) par.Add("date", date);

            return expect<Parameters>(client.TypeOperation<ValueSet>(Operation.CONCEPT_LOOKUP, par));
        }


        private static Meta extractMeta(Parameters parms)
        {
            if(!parms.Parameter.IsNullOrEmpty())
            {
                var parm = parms.Parameter[0];
                if(parm != null)
                {
                    if (parm.Value is Meta)
                        return (Meta)parm.Value;
                }
            }

            return null;
        }

        //[base]/$meta
        public static Meta Meta(this FhirClient client)
        {
            return extractMeta(expect<Parameters>(client.WholeSystemOperation(Operation.META, useGet:true)));
        }

        //[base]/Resource/$meta
        public static Meta Meta(this FhirClient client, ResourceType type)
        {             
            return extractMeta(expect<Parameters>(client.TypeOperation(Operation.META, type.ToString(), useGet: true)));
        }

        //[base]/Resource/id/$meta/[_history/vid]
        public static Meta Meta(this FhirClient client, Uri location)
        {
            Resource result;
            result = client.InstanceOperation(location, Operation.META, useGet: true);

            return extractMeta(expect<Parameters>(result));
        }

        public static Meta Meta(this FhirClient client, string location)
        {
            return Meta(client, new Uri(location, UriKind.RelativeOrAbsolute));
        }


        public static Meta AddMeta(this FhirClient client, Uri location, Meta meta)
        {
            var par = new Parameters().Add("meta", meta);
            return extractMeta(expect<Parameters>(client.InstanceOperation(location, Operation.META_ADD, par)));
        }

        public static Meta AddMeta(this FhirClient client, string location, Meta meta)
        {
            return AddMeta(client, new Uri(location, UriKind.RelativeOrAbsolute), meta);
        }

        public static Meta DeleteMeta(this FhirClient client, Uri location, Meta meta)
        {
            var par = new Parameters().Add("meta", meta);
            return extractMeta(expect<Parameters>(client.InstanceOperation(location, Operation.META_DELETE, par)));
        }

        public static Meta DeleteMeta(this FhirClient client, string location, Meta meta)
        {
            return DeleteMeta(client, new Uri(location, UriKind.RelativeOrAbsolute), meta);
        }

        public static OperationOutcome ValidateCreate(this FhirClient client, DomainResource resource, FhirUri profile = null)
        {
            if (resource == null) throw Error.ArgumentNull("resource");

            var par = new Parameters().Add("resource", resource).Add("mode", new FhirString("create"));
            if (profile != null) par.Add("profile", profile);

            return expect<OperationOutcome>(client.TypeOperation(Operation.VALIDATE_RESOURCE, "Resource", par));
        }

        public static OperationOutcome ValidateUpdate(this FhirClient client, DomainResource resource, string id, FhirUri profile = null)
        {
            if (id == null) throw Error.ArgumentNull("id");
            if (resource == null) throw Error.ArgumentNull("resource");

            var par = new Parameters().Add("resource", resource).Add("mode", new FhirString("update"));
            if (profile != null) par.Add("profile", profile);

            var loc = ResourceIdentity.Build("Resource",id);
            return expect<OperationOutcome>(client.InstanceOperation(loc, Operation.VALIDATE_RESOURCE, par));
        }

        public static OperationOutcome ValidateDelete(this FhirClient client, ResourceIdentity location)
        {
            if (location == null) throw Error.ArgumentNull("location");

            var par = new Parameters().Add("mode", new FhirString("delete"));

            return expect<OperationOutcome>(client.InstanceOperation(location.WithoutVersion().MakeRelative(), Operation.VALIDATE_RESOURCE, par));
        }

        public static OperationOutcome ValidateResource(this FhirClient client, DomainResource resource, string id=null, FhirUri profile=null)
        {
            if (resource == null) throw Error.ArgumentNull("resource");

            var par = new Parameters().Add("resource", resource);
            if (profile != null) par.Add("profile", profile);

            if (id == null)
            {
                return expect<OperationOutcome>(client.TypeOperation(Operation.VALIDATE_RESOURCE, resource.TypeName, par));
            }
            else
            {
                var loc = ResourceIdentity.Build("Resource", id);
                return expect<OperationOutcome>(client.InstanceOperation(loc, Operation.VALIDATE_RESOURCE, par));
            }
        }

        public static Parameters Validate(this FhirClient client, String valueSetId, FhirUri system, Code code, FhirString display = null)
        {
            if (code == null) throw new ArgumentNullException("code");
            if (system == null) throw new ArgumentNullException("system");

            var par = new Parameters().Add("code", code).Add("system", system);
            if (display != null)
            {
                par.Add("display", display);
            }

            return validateCodeForValueSetId(client, valueSetId, par);
        }

        public static Parameters ValidateCode(this FhirClient client, String valueSetId, Coding coding)
        {
            if (coding == null) throw new ArgumentNullException("coding");

            var par = new Parameters().Add("coding", coding);

            return validateCodeForValueSetId(client, valueSetId, par);
        }

        public static Parameters ValidateCode(this FhirClient client, String valueSetId, CodeableConcept codeableConcept)
        {
            if (codeableConcept == null) throw new ArgumentNullException("codeableConcept");

            var par = new Parameters().Add("codeableConcept", codeableConcept);

            return validateCodeForValueSetId(client, valueSetId, par);
        }


        public class TranslateConceptDependency
        {
            public FhirUri Element;

            public CodeableConcept Concept;
        }


        public static Parameters TranslateConcept(this FhirClient client, string id, Code code, FhirUri system, FhirString version,
        FhirUri valueSet, Coding coding, CodeableConcept codeableConcept, FhirUri target, IEnumerable<TranslateConceptDependency> dependencies)
        {
            Parameters par = createTranslateConceptParams(code, system, version, valueSet, coding, codeableConcept, target, dependencies);
            var loc = ResourceIdentity.Build("ConceptMap", id);
            return expect<Parameters>(client.InstanceOperation(loc, Operation.TRANSLATE, par));
        }


        public static Parameters TranslateConcept(this FhirClient client, Code code, FhirUri system, FhirString version,
           FhirUri valueSet, Coding coding, CodeableConcept codeableConcept, FhirUri target, IEnumerable<TranslateConceptDependency> dependencies )
        {
            Parameters par = createTranslateConceptParams(code, system, version, valueSet, coding, codeableConcept, target, dependencies);

            return expect<Parameters>(client.TypeOperation<ConceptMap>(Operation.TRANSLATE, par));
        }

        private static Parameters createTranslateConceptParams(Code code, FhirUri system, FhirString version, FhirUri valueSet, Coding coding, CodeableConcept codeableConcept, FhirUri target, IEnumerable<TranslateConceptDependency> dependencies)
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

        private static Parameters validateCodeForValueSetId(FhirClient client, string valueSetId, Parameters par)
        {
            ResourceIdentity location = new ResourceIdentity("ValueSet/" + valueSetId);

            return expect<Parameters>(client.InstanceOperation(location.WithoutVersion().MakeRelative(), Operation.VALIDATE_CODE, par));
        }        
    }
}
