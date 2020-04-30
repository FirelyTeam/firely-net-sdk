---
layout: default
title: What's new?
---
## In 0.92.5 (DSTU2) / 0.93.5 (STU3) (released 201708DD)
DSTU2+STU3:
* Changed the IElementNavigator interface to enable skipping directly to a child with a given name, thus increasing navigation performance 
* Improved performance of validation and fhirpath for POCOs
* Split off IFhirClient interface from the FhirClient implementation (primarily for testing/mocking)
* Many smaller bugfixes
* Re-instated support for .NET framework 4. Thanks lstratman!
* Improved error messages produced by the validator based on input from the NHS UK
* The validator will now let you put a constraint on children of Resource.contained, Bundle.entry.resource and similar nested resources.
* SerializationUtil.XmlReaderFromString() will no longer try to seek the stream passed in and rewind it.
* TransactionBuilder now has a (default) argument to specify the type of Bundle to build. Thanks mbaltus!
* DirectorySource now has Include/Exclude patterns (with globs) to have more control over directory scans for resource files.
* DirectorySource now supports processing conformance resources in json
* FhirClient now has async support
* You can now have List<> properties (like Extensions and other repeating elements) with null elements - these will simply be ignored and not serialized. Thanks wdebeau1!
* Made date->string and string->date conversion more consistent, fixing problems with locales using something else than ':' for time separators.
* Fixed an error where the If-None-Exists header included the base url of the server. Thanks angusmiller+tstolker!
* All Search() overloads on FhirClient now also have a reverseInclude parameter
* Update with a conditional would not set the If-Match header when doing a version-aware update. Thanks tstolker!
* DeepCopy() did not actually deep copy collections - if you changed the original collection before you iterated over the clone, you would see the changes. This has been fixed. Thanks mattiasflodin!
* Client would not pass on 1xx and 3xx errors to client, instead throwing a generic NotSupported exception, making it harder to handle these errors by the client. Thanks tstolker!
* Added a fall-back terminology service so the validator can now invoke an external terminology service if the local in-memory service (provided with the API)  fails.
* You can now specify a binding on an Extension, which translates to a binding on Extension.value[x]
* Fixed a bug where -if the definition of element[x] had a binding and a choice of bindeable and non-bindeable types- the validator would complain if the instance was actually a non-bindeable type.
* BREAKING: FhirClientOperation.Operation has been renamed to RestOperation
* BREAKING: Revision of calls to terminology services to support all parameters and overloads
* Validation across references will now include the path of the referring resource in errors about the referred resource to make interpretation of the outcomes easier.
* FhirPath's resolve() now actually works, and will resolve contained/bundled resources in the instance under evaluation. This also means the FhirPath evaluator will now take an EvaluationContext in which you can pass your resolver over to the evaluator.
* The enums in the generated code now also have an attribute on them with information about the codesystem, which can be retrieved using GetSystem() on any enum. Thanks brianpos!
* Added a few specific [Serializable] attributes to make the POCOs serializable with the Microsoft Orleans serializer. Thanks alexmarchis!
* Several improvements & bug fixes on the SnapshotGenerator
* [Excluded for now] CachedResolver can now be asked to clear its contents. As well, when doing a cache lookup, you can now optionally specify whether the cache should only be consulted, updated with a new copy or just return the cached entry (last one being the original behaviour)
* [Excluded for now] Improvements to the serializer to better support _summary serialization of Bundles

DSTU2: 
* Fixed small errors in the generated ConstraintComponent properties, giving more correct validation results

DSTU3:
* Fixes to the snapshot generator to create better ElementDefinition ids
* _sort parameter now uses STU3 format (_sort=a,-b,c) instead of modifier
* You can now set the preferred return to OperationOutcome. Thanks cknaap!
* You can now request the server to notify the client about unsupported search parameters. Thanks tstolker!


## In 0.90.6 (released 2016MMDD)
* Fix: FhirClient will no longer always add `_summary=false` to search queries
* Fix: FhirClient will not throw parse errors anymore if the server indicated a non-success status (i.e. a 406)

## In 0.90.5 (released 20160804)

* Enhancement: Portable45 target includes support for validation, and no longer depends on Silverlight 5 SDK. Thanks Tilo!
* Enhancement: Support for serialization where `_summary=data` (and automatically adds the Subsetted flag - temporarily adds the Tag then removes after serialization, if it wasn't there already)
* Enhancement: Added Debugger Displays for commonly used types
* Enhancement: Debugger Display for BundleEntries to show the HttpMethod and FullURL
* Enhancement: Additional method in ModelInfo (Thanks Marten)

```c#
public static bool IsKnownResource(FhirDefinedType type)
```

* Enhancement: You can (and should) now create an instance of a FhirXmlParser or FhirJsonParser instead of using the static methods on FhirParser, so you can set error policies per instance. 
* Enhancement: Introduced ParserSettings to configure parser on a per-instance basis

```c#
FhirXmlParser parser = new FhirXmlParser(new ParserSettings { AcceptUnknownMembers = true });
var patient = parser.Parse<Patient>(xmlWithPatientData);
```

* Enhancement: Introduced a setting to allow parser to parse (and serialize) unrecognized enumeration values. Use `Code<T>.ObjectValue` to get to get/set the string as it was encountered in the stream. The FhirClient now has a `ParserSettings` property to manage the parser used by the `FhirClient`.
* Enhancement: By popular demand: re-introduced `FhirClient.Refresh()`
* Enhancement: Snapshot generator now supports all DSTU2 features (re-slicing limited to extensions)

```c#
ArtifactResolver source = ArtifactResolver.CreateCachedDefault();
var settings = new SnapshotGeneratorSettings { IgnoreMissingTypeProfiles = true };
StructureDefinition profile;

var generator = new SnapshotGenerator(source, _settings);
generator.Generate(profile);
```

* Fix: Status 500 from a FHIR server with an HTML error message results in a FhirOperationException, not a FormatException. Thanks Tilo!
* Fix: `Code<T>` did not correctly implement `IsExactly()` and `Matches()`
* Fix: Now parses enumeration values with a member called "Equals" correctly.
* Fix: `Base.TypeName` would return incorrect name "Element" for Primitives and Code<T> (codes with enumerated values)
* And of course numerous bugfixes and code cleanups.

## In 0.90.4 (released 20160105)

* Enhancement: Additional Extension methods for converting native types to/from FHIR types

```
public static DateTime? ToDateTime(this Model.FhirDateTime me)
public static DateTime? ToDateTime(this Model.Date me)
public static string ToFhirDate(this System.DateTime me)
public static string ToFhirDateTime(this System.DateTime me)
public static string ToFhirId(this System.Guid me)
```

* Enhancement: Added the `SnapshotGenerator` class to turn differential representations of a StructureDefinition into a snapshot. Note: we're still working with the Java and HAPI people to get the snapshots 100% compatible. 
* Breaking change: All `BackboneElement` derived classes are now named as found on [BackboneElement](http://hl7.org/fhir/backboneelement.html#summary) page in the specification, under the specializations heading.
Usual fix for this will be removing the resource typename prefix from the classname, e.g. Bundle.BundleEntryComponent -> Bundle.EntryComponent
* Fix: Elements are not serialized correctly in summary mode
* Fix: Validate Operation does not work
* Fix: DeepCopy does not work on Careplan f201
* Fix: SearchParameters in ModelInfo are missing/have invalid Target values

From this version on, the model is now code generated using T4 templates within the build from the specification profile files (profiles-resources.xml, profiles-types.xml, search-parameters.xml and expansions.xml)


## In 0.90.3 (released 20151201)

* Enhancement: IConformanceResource now also exposes the xxxElement members. Thanks, wmrutten!
* Enhancement: Parameters.GetSingleValue<> now accepts non-primtives as generic param. Thanks, yunwang!
* Enhancement: ContentType.GetResourceFormatFromContentType now supports charset information. Thanks, CorinaCiocanea!
* Enhancement: Operations can now be invoked using GET
* Fix: Small code analysis fixes. Thanks, bnantz!
* Fix: SearchParams now supports `_sort` without modifiers. Thanks, sunvenu!
* Fix: FhirClient: The "Prefer" header was never set. Thanks, CorinaCiocanea!
* Fix: FhirClient could not handle spurious OperationOutcome results on successful POST/PUT when Prefer=minimal. Thanks, CorinaCiocanea!
* Fix: Json serializer serialized decimal value "6" to "6.0". Thanks, CorinaCiocanea!
* Fix: Json serializer now retains full precision of decimal on roundtrip.
* Fix: ETag header was not correctly parsed. Thanks, CorinaCiocanea! 
* Fix: Parameters with an "=" in the value (like pre-DSTU2 =<=) would become garbled when doing FhirClient.Continue(). Thanks rtaixghealth!
* Fix: FhirClient.Meta() operations will use GET and return Meta (not Parameters)


## In 0.90.2

* Added support for $translate operations on ConceptMap
* Added support for the changed _summary parameter
* ArtifactResolver can now resolve ValueSets based on system
* The CachedArtifactSource is now thread-safe


## In 0.90.0

* Updated the model to be compatible with DSTU2 (1.0.1)
* Added support for comments in Json
* Fixed a bug where elements called 'value' in Json could not have extensions or comments
* FhirClient now returns the status code in an OperationException
* Bugfixes


## In 0.50.2

* Many bug and stability fixes
* ReturnFullResource will not only set the Prefer header, but will do a subsequent read if the server ignores the Prefer header.
* Client will accept 4xx and 5xx responses when the server does not return an OperationOutcome
* Client gives clearer errors when the server returns HTML instead of xml/json 
* Call signatures for `OnBeforeRequest` and `OnAfterResponse` have been changed to give low-level access to body and native .NET objects. OnAfterResponse will now be called even if request failed or if response has parsing errors.
* The FhirClient has a full set of new LastXXX properties which return the last received status/resource/body.
* Serializers now correctly serialize the contents of a Bundle, even if summary=true



## In 0.20.2

* FhirClient updated to handle conditional create/read/update, Preference header
* Introduction of TransactionBuilder class to easily compose Bundles containing transactions
* Model classes updated to the latest DSTU2 changes
* Serialization of extensions back to "DSTU1" style (as agreed in San Antonio)


## In 0.20.1

* Added support for async


## In 0.20.0

* This is the new DSTU2 release
* Supports the new DSTU2 resources and DSTU2 serialization
* Uses the new DSTU2 class hierarchy with Base, Resource, DomainResource and Bundle
* Further alignment between the Java RM and HAPI
* Support for using the DSTU2 Operation framework
* Many API improvements, including:
 * deep compare (IsExactly) and deep copy (DeepCopy)
 * Collections will be created on-demand, so you can just do patient.Name.Add() without having to set patient.Name to a collection first
* Note: support for .NET 4.0 has been dropped, we support .NET 4.5 and PCL 4.5


## In 0.11.1

* Project now contains two assemblies: a "lightweight" core assembly (available across all platforms) and an additional library with profile and validation support.
* Added an XmlNs class with constants for all relevant xml namespaces used in FHIR
* Added `JsonXPathNavigator` to execute XPath statements over a FHIR-Json based document
* Added a new `Hl7.Fhir.Specification.Source` namespace that contains an `ArtifactResolver` class to obtain schema files, profiles and valuesets by uri or id. This class will read the provided validation.zip for the core artifacts. For more info see [here](artifacts.html).
* Changed `FhirUri` to use string internally, rather than the Uri class to guarantee round-trips and avoid url normalization issues
* All Resources and datatypes now support deep-copying using the `DeepCopy()` and `CopyTo()` methods.
* FhirClient supports `OnBeforeRequest` and `OnAfterRequest` hooks to enable the developer to plug in authentication.
* All primitives support `IsValidValue()` to check input against the constraints for FHIR primitives
* Models are up-to-date with FHIR 0.82
* And of course we fixed numerous bugs brought forward by the community


## In 0.10.0

* There's a new `FhirParser.ParseQueryFromUriParameters()` function to parse URL parameters into a FHIR `Query` resource
* The Model classes now implements `INotifyPropertyChanged`
* FhirSerializer supports writing just the summary view of resources
* Model elements of type ResourceReference now have an additional `ReferencesAttribute` (metadata) that indicates the resource names a reference can point to
* ModelInfo now has information telling you which FHIR primitive types map to which .NET Model types (this only used to work for complex datatypes and resources before)
* We now support both .NET 4.0, .NET 4.5 and Portable Class Libraries 4.5
* For .NET 4.5, the FhirClient supports methods with the async signature
* All assemblies now have their associated xml documentation files bundled in the NuGet package
* Models are up-to-date with FHIR 0.80, DSTU build 2408


## In 0.9.5

This release brings the .NET FHIR library up-to-date with the FHIR DSTU (0.8) version. Additionally, some major changes have been carried out:

* There is now *some* documentation
* The `FhirClient` calls have been changed after feedback of the early users. The most important changes are:
 *	the `Read()` call now accepts relative and absolute uri's as a parameter, so you can now do, say, a `Read(obs.subject.Reference)`. This means however that the old calling syntax like `Read("4")` cannot be used anymore, you need to pass at least a correct relative path like `Read("Patient/4")`.
 * Since the FHIR `create` and `update` operations don't return a body anymore, by default the return value of `Create()` and `Update()` will be an empty `ResourceEntry`. If you specify the `refresh` parameter however, the FHIR client will immediately issue a read, to get the latest updated version from the server.
 * The `Search()` signature has been simplified. You can now either use a very basic syntax (like `Search(new string[] {"name=john"})`), or switch to using the `Query` resource, which `Search()` now accepts as a (single) parameter as well.
* The validator has been renamed to `FhirValidator` and now behaves like the standard .NET validators: it validates one level deep only. To validate an object and it's children (e.g. a Bundle and all its entries and all its nested components and contained resources), specify the new `recursive` parameter.
	* The validator will now validate the XHtml according to the restricted FHIR schema, so active content is disallowed. 
*  The library now *incorporates* the 0.8 version of the Resources. This means that developers using the API's source distribution need only to compile the project to have all necessary parts, there is no longer a dependency on the Model assembly compiled as part of publication. Note too that the distribution contains the 0.8 resources *only* (so, no more `Appointment` resources, etc.).
* The library no longer uses the .NET portable class libraries and is based on the normal .NET 4.0 profile. The portable class libraries proved still too unfinished to use comfortably. We've fallen back on conditional compiles for Windows Phone support. Cross-platform compilation has not been rigorously tested.
* After being updated continuously over the past two years, the FHIR client needed a big refactoring. The code should be readable again.


## Before

Is history. If you really want, you can read the SVN and Git logs.
