---
layout: default
title: Tags in Bundles
---

Tags can be added to Resources and Bundles using the `Tags` property of either a ``BundleEntry`` or a ``Bundle``:

``` csharp	
entry.Tags.Add( new Tag("http://server.com/tags/mytag", Tag.FHIRTAGSCHEME_GENERAL) );
```

In this example a Tag with the term `http://server.com/tags/mytag` within the General FHIR tag scheme is created and added to an entry. When serialized, these Tags are rendered as `<category>` elements on the Atom feed or entry.

The specification currently defines three supported schemes:

<table><tr><th>Type</th><th>Scheme url</th><th>.NET constant</th></tr><tr><td>General tags</td><td>http://hl7.org/fhir/tag</td><td>Tag.FHIRTAGSCHEME_GENERAL</td></tr><tr><td>Profile tags</td><td>http://hl7.org/fhir/tag/profile</td><td>Tag.FHIRTAGSCHEME_PROFILE</td></tr><tr><td>Security labels</td><td>http://hl7.org/fhir/tag/security</td><td>Tag.FHIRTAGSCHEME_SECURITY</td></tr></table>

The specification additionally defines several terms within the General scheme to indicate human-readable labels and a discriminator for types of Bundles (either Document or Message). Instead of explicitly adding Tags, supplying the scheme and using the prescribed terms, the API provides convenience methods to attain the same goals, for example:

``` csharp
entry.SetTextTag("I am adding a textual note");
entry.AddProfileAssertion(new Uri("http://hl7.org/profiles/iso-21090"));
IEnumerable<Uri> labels = entry.GetSecurityLabels();

bundle.SetBundleType(BundleType.Document);
```

Since Atom entries and feeds may contain any number of non-FHIR category elements, the API contains methods to filter a list of Tags on one or more specific schemes:

``` csharp
var categories = entry.Tags;
var fhirTags = categories.FilterFhirSchemes();
var generalTags = categories.FilterByScheme(Tag.FHIRTAGSCHEME_GENERAL)
```
