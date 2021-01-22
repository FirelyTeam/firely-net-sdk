The validator test cases

These are a set of test cases that test out a validator and ensure it is functioning correctly. 
The tests depend on using tx.fhir.org as a terminology server for the validator. 

#Introduction

Notes about the tests:
* each test provides an exampe that will be validated, and specifies the acceptable outcome from the validator
* different validators work slightly differently. There's no need that the different validators produce the same error messages, or even that they produce the same number of errors. Because of this, each different test case has different success or failure rules for different validators. But the validators should agree about what is and isn't valid
* All tests test the provided example against the base specification
* in addition the test may specify a profile (with supporting collateral such as value sets) that is also tested


The following validator codes are known:
* "java" - the validator maintained as part of HAPI Core


# Manifest.json

The tests are specified in a manifest file that lists the tests. The manifest file is a json Object that contains 
a "test-cases" object, which contains a series of named tests that each has the same content. The name of the 
test is the name of the file to test (relative to this directory).

Tests have the following properties:
* ```use-test```: a boolean that specifies whether the test is valid (default true - sometimes tests are disabled)
* ```allowed-extension-domain``` - a string that specifies an extension URL the vlaidator won't insist in validating
* ```allowed-extension-domains``` - an array of strings that specifies an extension URL the vlaidator won't insist in validating
* ```language``` - the langauge context in which to run the evaluation (makes rules both about the output messages and text in the instance e.g. Coding.display)
* ```questionnaire``` - specifies a questionnaire to use when validating - only applies to QuestionnaireResponses
* ```codesystems``` - an array of strings that lists filenames of code systems to load 
* ```profiles``` - an array of strings that lists filenames of profiles to load 
* ```version``` - the version of FHIR to use when validating (default = R5 current, also supported, R2, R3 and R4, and also the 2016May ballot)
* ```java``` - rules for successful evaluation of the java validator (see below)
* ```profile``` - a sub-object that specifies to validate against a profile as well. This can have the properties
  * ```source``` - a string that is the name of the profile to test against (relative filename)
  * ```supporting``` - a string array (relative file names) of other code systems, value sets etc to load
  * ```java``` - rules for successful evaluation of the java validator (see below)
* ```logical``` - a sub-object that specifies to validate against a logical model as well. This can have the properties:
  * ```supporting``` - a string array (relative file names) of other code systems, value sets etc to load. One of these should be the base logical model for the instance
  * ```expressions``` - a series of FHIRPath expressions that will be checked to be true (testing FHIRpath against logical modesl)
  * ```java``` - rules for successful evaluation of the java validator (see below)

The java properrty is mandatory - all others are optional

# Java Validator

The java validator object specifies the rules for success for the validator, and has the following properties:

* ```errorCount``` (mandatory) - the number of errors that the java validator should produce
* ```warningCount``` (optional) - the number of warnings that the java validator should produce (if not specified, count of warnings is not checked)
* ```infoCount``` (optional) - the number of hints that the java validator should produce (if not specified, count of warnings is not checked)
* ```output``` (optional) - the correct output for the java validator. This is present to assist in testing/debugging, and should always be present once the test is approved.

Note: in order to make managing the tests easier, the java validator produces it's own copy of the manifest with output populated in $temp/validator-produced-manifest.json





