<?xml version="1.0" encoding="UTF-8"?>
<sch:schema xmlns:sch="http://purl.oclc.org/dsdl/schematron" queryBinding="xslt2">
  <sch:ns prefix="f" uri="http://hl7.org/fhir"/>
  <sch:ns prefix="h" uri="http://www.w3.org/1999/xhtml"/>
  <sch:pattern>
    <sch:title>f:ElementDefinition</sch:title>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/elementdefinition-allowedUnits']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/elementdefinition-allowedUnits': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:representation) &lt;= 0">representation: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:slicing) &lt;= 0">slicing: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:short) &lt;= 0">short: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:nameReference) &lt;= 0">nameReference: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:fixed[x]) &lt;= 0">fixed[x]: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:pattern[x]) &lt;= 0">pattern[x]: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:isModifier) &lt;= 0">isModifier: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:isSummary) &lt;= 0">isSummary: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="not(exists(for $type in f:type return $type/preceding-sibling::f:type[f:code/@value=$type/f:code/@value and f:profile/@value = $type/f:profile/@value]))">Types must be unique by the combination of code and profile (inherited</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:constraint) = count(distinct-values(f:constraint/f:key/@value))">Constraints must be unique by key (inherited</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="not(exists(f:binding)) or (count(f:type/f:code) = 0) or  f:type/f:code/@value=('code','Coding','CodeableConcept','Quantity','Extension', 'string', 'uri')">Binding can only be present for coded elements, string, and uri (inherited</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="not(exists(f:*[starts-with(local-name(.), 'pattern')])) or not(exists(f:*[starts-with(local-name(.), 'value')]))">Pattern and value are mutually exclusive (inherited</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="not(exists(f:*[starts-with(local-name(.), 'fixed')])) or not(exists(f:meaningWhenMissing))">default value and meaningWhenMissing are mutually exclusive (inherited</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="not(exists(f:min)) or not(exists(f:max)) or (not(f:max/@value) and not(f:min/@value)) or (f:max/@value = '*') or (number(f:max/@value) >= f:min/@value)">Min <= Max (inherited</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="not(exists(f:*[starts-with(local-name(.), 'pattern')])) or (count(f:type)=1)">Pattern may only be specified if there is one type (inherited</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="not(exists(f:*[starts-with(local-name(.), 'fixed')])) or (count(f:type)=1)">Fixed value may only be specified if there is one type (inherited</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="not(exists(f:nameReference) and exists(f:*[starts-with(local-name(.), 'value')]))">Either a namereference or a fixed value (but not both) is permitted (inherited</sch:assert>
    </sch:rule>
    </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing">
      <sch:assert test="(f:discriminator) or (f:description)">If there are no discriminators, there must be a definition (inherited</sch:assert>
    </sch:rule>
    </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.max</sch:title>
    <sch:rule context="f:ElementDefinition/f:max">
      <sch:assert test="@value='*' or (normalize-space(@value)!='' and normalize-space(translate(@value, '0123456789',''))='')">Max SHALL be a number or "*" (inherited</sch:assert>
    </sch:rule>
    </sch:pattern>
  <sch:pattern>
    <sch:title>f:ElementDefinition/f:type</sch:title>
    <sch:rule context="f:ElementDefinition/f:type">
      <sch:assert test="count(f:profile) &lt;= 0">profile: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition/f:type">
      <sch:assert test="count(f:aggregation) &lt;= 0">aggregation: maximum cardinality is 0</sch:assert>
    </sch:rule>
    <sch:rule context="f:ElementDefinition/f:type">
      <sch:assert test="not(exists(f:aggregation)) or exists(f:code[@value = 'Reference'])">Aggregation may only be specified if one of the allowed types for the element is a resource (inherited</sch:assert>
    </sch:rule>
    </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.binding</sch:title>
    <sch:rule context="f:ElementDefinition/f:binding">
      <sch:assert test="(exists(f:valueSetUri) or exists(f:valueSetReference)) or exists(f:description)">provide either a reference or a description (or both) (inherited</sch:assert>
    </sch:rule>
    </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.binding.valueSet[x]</sch:title>
    <sch:rule context="f:ElementDefinition/f:binding/f:valueSet[x]">
      <sch:assert test="starts-with(string(@value), 'http:') or starts-with(string(@value), 'https:')">URI SHALL start with http:// or https:// (inherited</sch:assert>
    </sch:rule>
    </sch:pattern>
</sch:schema>
