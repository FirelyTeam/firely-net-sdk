---
layout: default
title: Validating instance data
---

### Invoking the validator
Resources, Bundles, even individual Elements and BundleEntries can be validated using the `FhirValidator` class from the Hl7.Model.Validation namespace:

```csharp
var pat = new Patient() { /* set up data */ };

// Will throw a ValidationException when an error is encountered
FhirValidator.Validate(pat);

// Alternatively, use the TryXXXX pattern
var errors = new List<ValidationResult>();
var success = FhirValidator.TryValidate(pat, errors)

if(!success) { /* handle errors */ }
```

Normally, the validator will validate only the elements within the instance passed to the `Validate` call, but will not validate the contents of those elements (so, it will validate `Encounter`'s members, but not those of the nested `Hospitalization`). To recursively validate an instance, including all its children, pass an extra `recursive` parameter to the validation calls:

```csharp
var success = FhirValidator.TryValidate(pat, errors, recursive: true);
```

### Using the FHIR XSD schemas
The FHIR distribution contains a set of XSD files supplied as an aid for the validation of XML content. These files can be used to validate individual resources and (atom) feeds.

To facilitate access to these schemas, a `SchemaCollection` class is provided in the `Fhir.Model.Validation` namespace. The class has one static property `ValidationSchemaSet` which returns a .NET XmlSchemaSet containing all relevant XSD schemas, which can then readily be used in validation, like so:

```csharp
var doc = XDocument.Parse(value);
doc.Validate(SchemaCollection.ValidationSchemaSet, validationEventHandler: null);
``` 


### Supported validations
Currently, the `FhirValidator` will validate the following aspects of the FHIR datamodel:

* Cardinalty of an element.
* Regex patterns of the primitives code, date, dateTime, id, instant, oid, uri, uuid.
* Allowed types for a choice element (value[x]).
* Whether Narrative is actually valid HTML
* Catches active content in XHTML in Narrative
* Rules about nesting of Contained resources.


Notable rules not yet validated:

* Conformance to ValueSet bindings
* Invariants specified in the spec for Resources and datatypes
* Profile conformance
* Validation against the provided XSD schemas
