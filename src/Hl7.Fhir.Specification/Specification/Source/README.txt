The generated classes are taken directly from the \implementations\csharp\Model directory after the generator has ran and produces the generated classes.

NOTE: Remember to also copy:
	* The relevant fhir-single.xsd files from the generated schema directory
	* The relevant examples.zip for the tests
	* The relevant validation.zip, post-processed by hand to correct DSTU1 errors:
			- extract the conformance statements from profiles-resources.xml and put them into their
			  own core-conformances-base.xml feed file. 
			- Remove the duplicates base/base2 conformance statements from profiles-resources.xml
			- rename profiles-resources.xml to core-profiles-resources.xml and profiles-types.xml to
			  core-profiles-types.xml.
			- rename valueset files to core-valuesets-v2.xml, core-valuesets-v3.xml and core-valuesets-fhir.xml
			- extract the valueset with id http://hl7.org/fhir/valueset/http://nema.org/dicom/vs/dcid to
			  core-valuesets-dicom.xml, and give it a new entry id "http://nema.org/dicom/vs/dcid"
			- in core-valuesets-v2.xml, search & replace "<id>http://hl7.org/fhir/vs/http" with "<id>http" (or make entry.id the same as ValueSet.identifier)
			- in core-valuesets-v3.xml, search & replace "<id>http://hl7.org/fhir/v3/" with "<id>http://hl7.org/fhir/v3/vs/" and then
			  search & replace "<id>http://hl7.org/fhir/v3/vs/vs/" with "<id>http://hl7.org/fhir/v3/vs/" (or make entry.id the same as ValueSet.identifier)
			- in core-valuesets-fhir.xml, correct entry.id "http://hl7.org/fhir/valueset/http://hl7.org/fhir/valueset-systems" to become "http://hl7.org/fhir/vs/valueset-systems", update it's identifier as well.
			- in core-valuesets-fhir.xml, rename the prefix http://hl7.org/fhir/valueset/ to http://hl7.org/fhir/vs/
			- add all core-* files to validation.zip, remove the original (=renamed) files. leave the rest of the files intact
			- rename all references http://hl7.org/fhir/profiles and http://hl7.org/fhir/profile to http://hl7.org/fhir/Profile and change the Feed <id>'s to use camelcasing
