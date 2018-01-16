using Hl7.Fhir.ElementModel;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public interface IAssertion : IJsonSerializable
    {
    }
   
    public interface IJsonSerializable
    {
        JToken ToJson();
    }
}


/*****

  {   
    define: { identified subschemas which are not evaluated }

    ref: "external reference" 

    {
       // nested schema
    }

	minItems:
	maxItems:

    group { membership condition } : { constraints for this group }

    slice 
    {
        ordered: true/false

		// one or more groups defined by subschemas
		group { membership condition } : { constraints for this group }
		default: { constraints for this group } === group {} : {    }    // no default: {} : { fail }
    }

    children
    {
		"asdfdfs" : { constraints for this name }
		"asdfdfs" : { constraints for this name }
    }

	success { a tag block to execute }
	fail { a tag block to execute }
	undecided { a tag block to execute }
}

****/

