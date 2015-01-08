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
        public static string FETCH_PATIENT_RECORD = "everything";
        public static string EXPAND_VALUESET = "expand";
        public static string CONCEPT_LOOKUP = "lookup";
        public static string VALIDATE_RESOURCE = "validate";

        public static Bundle FetchPatientRecord(this FhirClient client, Uri patient = null, FhirDateTime start = null, FhirDateTime end = null)
        {
            var par = new Parameters();

            if (start != null) par.Add("start", start);
            if (end != null) par.Add("end", end);
            
            Resource result;
            if (patient == null)
                result = client.TypeOperation<Patient>(FETCH_PATIENT_RECORD, par);
            else
            {
                var location = new ResourceIdentity(patient);
                result = client.Operation(location.WithoutVersion().MakeRelative(), FETCH_PATIENT_RECORD, par);
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

            return expect<ValueSet>(client.Operation(id.WithoutVersion().MakeRelative(), EXPAND_VALUESET, par));
        }


        public static ValueSet ExpandValueSet(this FhirClient client, FhirUri identifier, FhirString filter = null, FhirDateTime date = null)
        {
            if (identifier == null) throw Error.ArgumentNull("identifier");

            var par = new Parameters();

            par.Add("identifier", identifier);
            if (filter != null) par.Add("filter", filter);
            if (date != null) par.Add("date", date);
            
            return expect<ValueSet>(client.TypeOperation<ValueSet>(EXPAND_VALUESET, par));
        }


        public static ValueSet ExpandValueSet(this FhirClient client, ValueSet vs, FhirString filter = null, FhirDateTime date = null)
        {
            if (vs == null) throw Error.ArgumentNull("vs");

            var par = new Parameters().Add("valueSet", vs);
            if (filter != null) par.Add("filter", filter);
            if (date != null) par.Add("date", date);

            return expect<ValueSet>(client.TypeOperation<ValueSet>(EXPAND_VALUESET, par));
        }

        public static Parameters ConceptLookup(this FhirClient client, Coding coding, FhirDateTime date=null)
        {
            if (coding == null) throw Error.ArgumentNull("coding");

            var par = new Parameters();
            par.Add("coding", coding);
            if (date != null) par.Add("date", date);

            return expect<Parameters>(client.TypeOperation<ValueSet>(CONCEPT_LOOKUP, par));
        }

        public static Parameters ConceptLookup(this FhirClient client, Code code, FhirUri system, FhirString version = null, FhirDateTime date = null)
        {
            if (code == null) throw Error.ArgumentNull("code");
            if (system == null) throw Error.ArgumentNull("system");

            var par = new Parameters().Add("code",code).Add("system",system);
            if (version != null) par.Add("version", version);
            if (date != null) par.Add("date", date);

            return expect<Parameters>(client.TypeOperation<ValueSet>(CONCEPT_LOOKUP, par));
        }

        public static OperationOutcome ValidateCreate(this FhirClient client, DomainResource resource, FhirUri profile = null)
        {
            if (resource == null) throw Error.ArgumentNull("resource");

            var par = new Parameters().Add("resource", resource).Add("mode", new FhirString("create"));
            if (profile != null) par.Add("profile", profile);

            return expect<OperationOutcome>(client.TypeOperation(VALIDATE_RESOURCE, "Resource", par));
        }

        public static OperationOutcome ValidateUpdate(this FhirClient client, DomainResource resource, string id, FhirUri profile = null)
        {
            if (id == null) throw Error.ArgumentNull("id");
            if (resource == null) throw Error.ArgumentNull("resource");

            var par = new Parameters().Add("resource", resource).Add("mode", new FhirString("update"));
            if (profile != null) par.Add("profile", profile);

            var loc = ResourceIdentity.Build("Resource",id);
            return expect<OperationOutcome>(client.Operation(loc, VALIDATE_RESOURCE, par));
        }

        public static OperationOutcome ValidateDelete(this FhirClient client, ResourceIdentity location)
        {
            if (location == null) throw Error.ArgumentNull("location");

            var par = new Parameters().Add("mode", new FhirString("delete"));

            return expect<OperationOutcome>(client.Operation(location.WithoutVersion().MakeRelative(), VALIDATE_RESOURCE, par));
        }

        public static OperationOutcome ValidateResource(this FhirClient client, DomainResource resource, string id=null, FhirUri profile=null)
        {
            if (resource == null) throw Error.ArgumentNull("resource");

            var par = new Parameters().Add("resource", resource);
            if (profile != null) par.Add("profile", profile);

            if (id == null)
            {
                return expect<OperationOutcome>(client.TypeOperation(VALIDATE_RESOURCE, resource.TypeName, par));
            }
            else
            {
                var loc = ResourceIdentity.Build("Resource", id);
                return expect<OperationOutcome>(client.Operation(loc, VALIDATE_RESOURCE, par));
            }
        }
    }
}
