<?xml version="1.0" encoding="UTF-8"?>
<sch:schema xmlns:sch="http://purl.oclc.org/dsdl/schematron" queryBinding="xslt2">
  <sch:ns prefix="f" uri="http://hl7.org/fhir"/>
  <sch:ns prefix="h" uri="http://www.w3.org/1999/xhtml"/>
  <!-- 
    This file contains just the constraints for the profile TestScript
    It includes the base constraints for the resource as well.
    Because of the way that schematrons and containment work, 
    you may need to use this schematron fragment to build a, 
    single schematron that validates contained resources (if you have any) 
  -->
  <sch:pattern>
    <sch:title>f:TestScript</sch:title>
    <sch:rule context="f:TestScript">
      <sch:assert test="count(f:url) &gt;= 1">url: minimum cardinality of 'url' is 1</sch:assert>
      <sch:assert test="count(f:version) &gt;= 1">version: minimum cardinality of 'version' is 1</sch:assert>
      <sch:assert test="count(f:experimental) &gt;= 1">experimental: minimum cardinality of 'experimental' is 1</sch:assert>
      <sch:assert test="count(f:publisher) &gt;= 1">publisher: minimum cardinality of 'publisher' is 1</sch:assert>
      <sch:assert test="count(f:description) &gt;= 1">description: minimum cardinality of 'description' is 1</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript</sch:title>
    <sch:rule context="f:TestScript">
      <sch:assert test="not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')">Name should be usable as an identifier for the module by machine processing applications such as code generation (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.meta</sch:title>
    <sch:rule context="f:TestScript/f:meta">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.implicitRules</sch:title>
    <sch:rule context="f:TestScript/f:implicitRules">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.language</sch:title>
    <sch:rule context="f:TestScript/f:language">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.text</sch:title>
    <sch:rule context="f:TestScript/f:text">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.extension</sch:title>
    <sch:rule context="f:TestScript/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.url</sch:title>
    <sch:rule context="f:TestScript/f:url">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.identifier</sch:title>
    <sch:rule context="f:TestScript/f:identifier">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.version</sch:title>
    <sch:rule context="f:TestScript/f:version">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.name</sch:title>
    <sch:rule context="f:TestScript/f:name">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.title</sch:title>
    <sch:rule context="f:TestScript/f:title">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.status</sch:title>
    <sch:rule context="f:TestScript/f:status">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.experimental</sch:title>
    <sch:rule context="f:TestScript/f:experimental">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.date</sch:title>
    <sch:rule context="f:TestScript/f:date">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.publisher</sch:title>
    <sch:rule context="f:TestScript/f:publisher">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.contact</sch:title>
    <sch:rule context="f:TestScript/f:contact">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.description</sch:title>
    <sch:rule context="f:TestScript/f:description">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.useContext</sch:title>
    <sch:rule context="f:TestScript/f:useContext">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.jurisdiction</sch:title>
    <sch:rule context="f:TestScript/f:jurisdiction">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.purpose</sch:title>
    <sch:rule context="f:TestScript/f:purpose">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.copyright</sch:title>
    <sch:rule context="f:TestScript/f:copyright">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.origin</sch:title>
    <sch:rule context="f:TestScript/f:origin">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.origin.extension</sch:title>
    <sch:rule context="f:TestScript/f:origin/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.origin.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:origin/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.origin.index</sch:title>
    <sch:rule context="f:TestScript/f:origin/f:index">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.origin.profile</sch:title>
    <sch:rule context="f:TestScript/f:origin/f:profile">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.destination</sch:title>
    <sch:rule context="f:TestScript/f:destination">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.destination.extension</sch:title>
    <sch:rule context="f:TestScript/f:destination/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.destination.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:destination/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.destination.index</sch:title>
    <sch:rule context="f:TestScript/f:destination/f:index">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.destination.profile</sch:title>
    <sch:rule context="f:TestScript/f:destination/f:profile">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata</sch:title>
    <sch:rule context="f:TestScript/f:metadata">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="f:capability/f:required or f:capability/f:validated or (f:capability/f:required and f:capability/f:validated)">TestScript metadata capability SHALL contain required or validated or both. (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.extension</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.link</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:link">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.link.extension</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:link/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.link.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:link/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.link.url</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:link/f:url">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.link.description</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:link/f:description">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability.extension</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability.required</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability/f:required">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability.validated</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability/f:validated">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability.description</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability/f:description">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability.origin</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability/f:origin">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability.destination</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability/f:destination">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability.link</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability/f:link">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.metadata.capability.capabilities</sch:title>
    <sch:rule context="f:TestScript/f:metadata/f:capability/f:capabilities">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.scope</sch:title>
    <sch:rule context="f:TestScript/f:scope">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.scope.extension</sch:title>
    <sch:rule context="f:TestScript/f:scope/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.scope.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:scope/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.scope.artifact</sch:title>
    <sch:rule context="f:TestScript/f:scope/f:artifact">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.scope.conformance</sch:title>
    <sch:rule context="f:TestScript/f:scope/f:conformance">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.scope.phase</sch:title>
    <sch:rule context="f:TestScript/f:scope/f:phase">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.fixture</sch:title>
    <sch:rule context="f:TestScript/f:fixture">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.fixture.extension</sch:title>
    <sch:rule context="f:TestScript/f:fixture/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.fixture.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:fixture/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.fixture.autocreate</sch:title>
    <sch:rule context="f:TestScript/f:fixture/f:autocreate">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.fixture.autodelete</sch:title>
    <sch:rule context="f:TestScript/f:fixture/f:autodelete">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.fixture.resource</sch:title>
    <sch:rule context="f:TestScript/f:fixture/f:resource">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.profile</sch:title>
    <sch:rule context="f:TestScript/f:profile">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable</sch:title>
    <sch:rule context="f:TestScript/f:variable">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="not(f:expression and f:headerField and f:path)">Variable can only contain one of expression, headerField or path. (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.extension</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.name</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:name">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.defaultValue</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:defaultValue">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.description</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:description">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.expression</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:expression">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.headerField</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:headerField">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.hint</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:hint">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.path</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:path">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.variable.sourceId</sch:title>
    <sch:rule context="f:TestScript/f:variable/f:sourceId">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup</sch:title>
    <sch:rule context="f:TestScript/f:setup">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.extension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="(f:operation or f:assert) and not(f:operation and f:assert)">Setup action SHALL contain either an operation or assert but not both. (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.extension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="f:sourceId or ((f:targetId or f:url or f:params) and (count(f:targetId) + count(f:url) + count(f:params) =1)) or (f:type/f:code/@value='capabilities' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')">Setup operation SHALL contain either sourceId or targetId or params or url. (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.extension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.type</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:type">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.resource</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:resource">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.label</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:label">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.description</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:description">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.accept</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:accept">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.contentType</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:contentType">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.destination</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:destination">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.encodeRequestUrl</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:encodeRequestUrl">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.method</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:method">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.origin</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:origin">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.params</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:params">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.requestHeader</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:requestHeader">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.requestHeader.extension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:requestHeader/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.requestHeader.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:requestHeader/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.requestHeader.field</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:requestHeader/f:field">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.requestHeader.value</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:requestHeader/f:value">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.requestId</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:requestId">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.responseId</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:responseId">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.sourceId</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:sourceId">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.targetId</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:targetId">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.operation.url</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:operation/f:url">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="count(f:contentType) + count(f:expression) + count(f:headerField) + count(f:minimumId) + count(f:navigationLinks) + count(f:path) + count(f:requestMethod) + count(f:resource) + count(f:responseCode) + count(f:response) + count(f:rule) + count(f:ruleset) + count(f:validateProfileId)  &lt;=1">Only a single assertion SHALL be present within setup action assert element. (inherited)</sch:assert>
      <sch:assert test="(f:compareToSourceId and f:compareToSourceExpression) or (f:compareToSourceId and f:compareToSourcePath) or not(f:compareToSourceId or f:compareToSourceExpression or f:compareToSourcePath)">Setup action assert SHALL contain either compareToSourceId and compareToSourceExpression, compareToSourceId and compareToSourcePath or neither. (inherited)</sch:assert>
      <sch:assert test="((count(f:response) + count(f:responseCode)) = 0 and (f:direction/@value='request')) or (count(f:direction) = 0) or (f:direction/@value='response')">Setup action assert response and responseCode SHALL be empty when direction equals request (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.extension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.label</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:label">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.description</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:description">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.direction</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:direction">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.compareToSourceId</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:compareToSourceId">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.compareToSourceExpression</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:compareToSourceExpression">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.compareToSourcePath</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:compareToSourcePath">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.contentType</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:contentType">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.expression</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:expression">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.headerField</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:headerField">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.minimumId</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:minimumId">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.navigationLinks</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:navigationLinks">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.operator</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:operator">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.path</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:path">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.requestMethod</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:requestMethod">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.requestURL</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:requestURL">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.resource</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:resource">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.response</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:response">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.responseCode</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:responseCode">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.sourceId</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:sourceId">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.stopTestOnFail</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:stopTestOnFail">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.validateProfileId</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:validateProfileId">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.value</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:value">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.setup.action.assert.warningOnly</sch:title>
    <sch:rule context="f:TestScript/f:setup/f:action/f:assert/f:warningOnly">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test</sch:title>
    <sch:rule context="f:TestScript/f:test">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test.extension</sch:title>
    <sch:rule context="f:TestScript/f:test/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:test/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test.name</sch:title>
    <sch:rule context="f:TestScript/f:test/f:name">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test.description</sch:title>
    <sch:rule context="f:TestScript/f:test/f:description">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test.action</sch:title>
    <sch:rule context="f:TestScript/f:test/f:action">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="(f:operation or f:assert) and not(f:operation and f:assert)">Test action SHALL contain either an operation or assert but not both. (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test.action.extension</sch:title>
    <sch:rule context="f:TestScript/f:test/f:action/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test.action.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:test/f:action/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test.action.operation</sch:title>
    <sch:rule context="f:TestScript/f:test/f:action/f:operation">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="f:sourceId or (f:targetId or f:url or f:params) and (count(f:targetId) + count(f:url) + count(f:params) =1) or (f:type/f:code/@value='capabilities' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')">Test operation SHALL contain either sourceId or targetId or params or url. (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.test.action.assert</sch:title>
    <sch:rule context="f:TestScript/f:test/f:action/f:assert">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="count(f:contentType) + count(f:expression) + count(f:headerField) + count(f:minimumId) + count(f:navigationLinks) + count(f:path) + count(f:requestMethod) + count(f:resource) + count(f:responseCode) + count(f:response) + count(f:rule) + count(f:ruleset) + count(f:validateProfileId)  &lt;=1">Only a single assertion SHALL be present within test action assert element. (inherited)</sch:assert>
      <sch:assert test="(f:compareToSourceId and f:compareToSourceExpression) or (f:compareToSourceId and f:compareToSourcePath) or not(f:compareToSourceId or f:compareToSourceExpression or f:compareToSourcePath)">Test action assert SHALL contain either compareToSourceId and compareToSourceExpression, compareToSourceId and compareToSourcePath or neither. (inherited)</sch:assert>
      <sch:assert test="((count(f:response) + count(f:responseCode)) = 0 and (f:direction/@value='request')) or (count(f:direction) = 0) or (f:direction/@value='response')">Test action assert response and response and responseCode SHALL be empty when direction equals request (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.teardown</sch:title>
    <sch:rule context="f:TestScript/f:teardown">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.teardown.extension</sch:title>
    <sch:rule context="f:TestScript/f:teardown/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.teardown.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:teardown/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.teardown.action</sch:title>
    <sch:rule context="f:TestScript/f:teardown/f:action">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.teardown.action.extension</sch:title>
    <sch:rule context="f:TestScript/f:teardown/f:action/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.teardown.action.modifierExtension</sch:title>
    <sch:rule context="f:TestScript/f:teardown/f:action/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>TestScript.teardown.action.operation</sch:title>
    <sch:rule context="f:TestScript/f:teardown/f:action/f:operation">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="f:sourceId or (f:targetId or f:url or (f:params and f:resource)) and (count(f:targetId) + count(f:url) + count(f:params) =1) or (f:type/f:code/@value='capabilities' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')">Teardown operation SHALL contain either sourceId or targetId or params or url. (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
</sch:schema>
