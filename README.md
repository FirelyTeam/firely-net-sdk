fhir-net-api
============

This is the support API's for working with the HL7 FHIR standard on the .NET platform. The project provides 
additional functionality beyond those in the HL7.Fhir.Model library provided at the FHIR website 
(http://www.hl7.org/fhir)

This library provides:
* Xml & Json Parsers and serializers for the FHIR object model
* Profile validation functionality
* ValueSet support
* ... more to come

Note: the parsers and serializers cover the functionality found originally in the HL7.Fhir.Model libraries 
provided on the FHIR website, but are totally rewritten to support profiled FHIR resources. Parsers and 
serializers are no longer generated, but use a run-time discovery mechanism so new resources and extensions
can be added.

