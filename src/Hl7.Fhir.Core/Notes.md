# Implementation notes

## Model classes and namespaces

Classes and enumeration that are common for all FHIR versions are in the `Hl7.Fhir.Model` namespace

Classes and enumeration that are specific for a FHIR version are in the `Hl7.Fhir.Model.XXXX` namespace , where `xxxx` = `DSTU2`, `STU3` etc.

So for example FHIR CodeableConcept - whose definition is the same across all FHIR version - is represented by the `Hl7.Fhir.Model.CodeableConcept` class, whereas FHIR Patient - that is different between DSTU2 and STU3 - is represented by the `Hl7.Fhir.Model.DSTU2.Patient` and `Hl7.Fhir.Model.STU3.Patient` classes.

Similarly for enumerations: there is a single `Hl7.Fhir.Model.SearchEntryMode` enumeration, because the [search-entry-mode](http://hl7.org/fhir/DSTU2/valueset-search-entry-mode.html) value set is the same across all FHIR version, wheres there are different `Hl7.Fhir.Model.DSTU2.AllergyIntoleranceCategory` and `Hl7.Fhir.Model.STU3.AllergyIntoleranceCategory` enumerations because the allergy-intolerance-category values set is different between [DSTU2](http://hl7.org/fhir/DSTU2/valueset-allergy-intolerance-category.html) and [STU3](http://hl7.org/fhir/STU3/valueset-allergy-intolerance-category.html).

Enumerations are always declared globally and never inside another class, even when they are specific to one resource (like `AllergyIntoleranceCategory` in the example above). This allow them to be shared if they are common across versions.

Classes and enumeration are either considered common or specific to each version, they cannot be common between version X and Y but different in version Z. This does not shows up currently because there are only two FHIR versions, but it would make a difference in the future when more versions are available and supported.

### Version enumeration

There is an automatically generated `Hl7.Fhir.Model.Version` enumeration that is used to indicate FHIR versions - e.g. `Version.DSTU2`, `Version.STU3` or `Version.All` to indicate all (any) version.

All generated classes are tagged with the version they belong to via `FhirTypeAttribute.Version`.

### Comparisons

Value sets are considered 'the same' - and hence generate a common shared enumeration - if they have the same name and values.

Resources and data types are considered 'the same' - and hence generate a common shared class - if they have the same name, base type or class, primitive type (if they are primityve data type), validation pattern, abstract or concrete attribute, elements (properties) and components (sub-classes). In turn elements (properties) are considered 'the same' if they have the same name, type, minimum and maximum cardinality and target reference types (if they are a reference).

Note that if a data type X is different between version this difference propagates to all resources or data types that have elements of type X - so for example ResourceReference is different between DSTU2 and STU3, and hence all resources that have a ResourceReference element are different between DSTU2 and STU3.

### Summary membership

Elements are still considered the same even if they have different summary membership - i.e. if an element is part of the summary in one FHIR version and not part of the summary in another FHIR version this does _not_ per se makes the resource or data type containing it version-specific. For example the DSTU2 and STU3 Annotation data types have one single `Hl7.Fhir.Model.Annotation` class common across FHIR versions even if its `text` element is part of the summary in DSTU2 and not part of the summary in STU3.

The C# `FhirElementAttribute.InSummary` property indicating summary membership for a property specifies the version(s) in which the property is part of the summary, so there is:

```CSharp
   [FhirElement("time", InSummary=new[]{Hl7.Fhir.Model.Version.All}, Order=40)]
   public Hl7.Fhir.Model.FhirDateTime TimeElement
```

indicating that `TimeElement` is part of the summary in all versions, and
```CSharp
    [FhirElement("text", InSummary=new[]{Hl7.Fhir.Model.Version.DSTU2}, Order=50)]
    public Hl7.Fhir.Model.FhirString TextElement
```
indicating that `TextElement` is part of the summary only for FHIR DSTU2.

### Polymorphic elements

Polymorphic elements are still considered the same across versions even if they have different allowed types in different FHIR versions - i.e. if a polymorphic can have type X, Y in one FHIR version and X, Z in another FHIR version this does _not_ per se makes the resource or data type containing it version-specific. For example the DSTU2 and STU3 Annotation data types have one single `Hl7.Fhir.Model.Annotation` class common across FHIR versions even if its `author` element can be a DSTU2 ResourceReference or a string in DSTU2 and a STU3 ResourceReference or a string in STU3 (because the ResourceReference type is different between STU3 and DSTU2).

The C# attribute indicating the allowed types can appear multiple times, once for each different versions, so there is:

```CSharp
    [AllowedTypes(Version=Version.DSTU2, Types=new[]{typeof(Hl7.Fhir.Model.DSTU2.ResourceReference),typeof(Hl7.Fhir.Model.FhirString)})]
    [AllowedTypes(Version=Version.STU3, Types=new[]{typeof(Hl7.Fhir.Model.STU3.ResourceReference),typeof(Hl7.Fhir.Model.FhirString)})]
    public Hl7.Fhir.Model.Element Author
```
indicating that the allowed types are `Hl7.Fhir.Model.DSTU2.ResourceReference` and `Hl7.Fhir.Model.FhirString` for DSTU2 and `Hl7.Fhir.Model.STU3.ResourceReference` and `Hl7.Fhir.Model.FhirString` for STU3, and there is plain:

```CSharp
    [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.DSTU2.ResourceReference))]
    public Hl7.Fhir.Model.Element Trigger
```

indicating that the allowed types are `Hl7.Fhir.Model.CodeableConcept` and `Hl7.Fhir.Model.DSTU2.ResourceReference` regardless of the FHIR version.

### Compatibility interfaces and classes

To allow using the same code to manipulate data from different FHIR versions there are a number of interfaces that are implemented by resource and data type classes across different versions (`Hl7.Fhir.Model` implied in all the class and interface names):

- `IBinary`: implemented by `Dstu2.Binary` and `Stu3.Binary` - allow read-write access to `Content` and `ContentType`

- `IBundle`, `IBundleEntry`, `IBundleSearch`, `IBundleRequest`, `IBundleResponse`: implemented by `Dstu2.Bundle` and `Stu3.Bundle` and their sub-classes - allow read access to all the data in a bundle;

- `IIdentifier`: implemented by `Dstu2.Identifier`, `Stu3.Identifier` and `CommonIdentifier` - allow read access to all identifiers properties;

- `IResourceReference`: implemented by `Dstu2.ResourceReference`, `Stu3.ResourceReference` and `CommonResourceReference` - allow read access to all resource reference properties

- `IMetadata`: implemented by `Dstu2.Conformance` and `Stu3.CapabilityStatement` - allow read access to `FhirVersion` (the numeric FHIR version contained in the conformance / capability statement)

There are also some common concrete classes that can be used when the interfaces are not enough:

- `CommonIdentifier` - corresponding to `Dstu2.Identifier` and `Stu3.Identifier`

- `CommonResourceReference` - corresponding to `Dstu2.ResourceReference` and `Stu3.ResourceReference`

- `CommonOperationOutcome` - corresponding to `Dstu2.OperationOutcome` and `Stu3.OperationOutcome`

All the above interface and classes and defined and maintained manually.

## Parsing and serialization

## Client
