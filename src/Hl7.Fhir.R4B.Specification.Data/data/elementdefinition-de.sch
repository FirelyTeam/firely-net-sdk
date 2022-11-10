<?xml version="1.0" encoding="UTF-8"?>
<sch:schema xmlns:sch="http://purl.oclc.org/dsdl/schematron" queryBinding="xslt2">
  <sch:ns prefix="f" uri="http://hl7.org/fhir"/>
  <sch:ns prefix="h" uri="http://www.w3.org/1999/xhtml"/>
  <!-- 
    This file contains just the constraints for the profile ElementDefinition
    It includes the base constraints for the resource as well.
    Because of the way that schematrons and containment work, 
    you may need to use this schematron fragment to build a, 
    single schematron that validates contained resources (if you have any) 
  -->
  <sch:pattern>
    <sch:title>f:ElementDefinition</sch:title>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/elementdefinition-allowedUnits']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/elementdefinition-allowedUnits': maximum cardinality of 'extension' is 1</sch:assert>
      <sch:assert test="count(f:representation) &lt;= 0">representation: maximum cardinality of 'representation' is 0</sch:assert>
      <sch:assert test="count(f:slicing) &lt;= 0">slicing: maximum cardinality of 'slicing' is 0</sch:assert>
      <sch:assert test="count(f:short) &lt;= 0">short: maximum cardinality of 'short' is 0</sch:assert>
      <sch:assert test="count(f:contentReference) &lt;= 0">contentReference: maximum cardinality of 'contentReference' is 0</sch:assert>
      <sch:assert test="count(f:fixed[x]) &lt;= 0">fixed[x]: maximum cardinality of 'fixed[x]' is 0</sch:assert>
      <sch:assert test="count(f:pattern[x]) &lt;= 0">pattern[x]: maximum cardinality of 'pattern[x]' is 0</sch:assert>
      <sch:assert test="count(f:isModifier) &lt;= 0">isModifier: maximum cardinality of 'isModifier' is 0</sch:assert>
      <sch:assert test="count(f:isSummary) &lt;= 0">isSummary: maximum cardinality of 'isSummary' is 0</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition</sch:title>
    <sch:rule context="f:ElementDefinition">
      <sch:assert test="not(exists(f:min)) or not(exists(f:max)) or (not(f:max/@value) and not(f:min/@value)) or (f:max/@value = '*') or (number(f:max/@value) &gt;= f:min/@value)">Min &lt;= Max (inherited)</sch:assert>
      <sch:assert test="not(exists(f:contentReference) and (exists(f:type) or exists(f:*[starts-with(local-name(.), 'value')]) or exists(f:*[starts-with(local-name(.), 'defaultValue')])  or exists(f:*[starts-with(local-name(.), 'fixed')]) or exists(f:*[starts-with(local-name(.), 'pattern')]) or exists(f:*[starts-with(local-name(.), 'example')]) or exists(f:*[starts-with(local-name(.), 'f:minValue')]) or exists(f:*[starts-with(local-name(.), 'f:maxValue')]) or exists(f:maxLength) or exists(f:binding)))">if the element definition has a contentReference, it cannot have type, defaultValue, fixed, pattern, example, minValue, maxValue, maxLength, or binding (inherited)</sch:assert>
      <sch:assert test="not(exists(f:*[starts-with(local-name(.), 'fixed')])) or (count(f:type)&lt;=1)">Fixed value may only be specified if there is one type (inherited)</sch:assert>
      <sch:assert test="not(exists(f:*[starts-with(local-name(.), 'pattern')])) or (count(f:type)&lt;=1)">Pattern may only be specified if there is one type (inherited)</sch:assert>
      <sch:assert test="not(exists(f:*[starts-with(local-name(.), 'pattern')])) or not(exists(f:*[starts-with(local-name(.), 'fixed')]))">Pattern and fixed are mutually exclusive (inherited)</sch:assert>
      <sch:assert test="not(exists(f:binding)) or (count(f:type/f:code) = 0) or  f:type/f:code/@value=('code','Coding','CodeableConcept','Quantity','string', 'uri', 'Duration')">Binding can only be present for coded elements, string, and uri (inherited)</sch:assert>
      <sch:assert test="not(exists(for $type in f:type return $type/preceding-sibling::f:type[f:code/@value=$type/f:code/@value]))">Types must be unique by code (inherited)</sch:assert>
      <sch:assert test="count(f:constraint) = count(distinct-values(f:constraint/f:key/@value))">Constraints must be unique by key (inherited)</sch:assert>
      <sch:assert test="not(exists(f:*[starts-with(local-name(.), 'fixed')])) or not(exists(f:meaningWhenMissing))">default value and meaningWhenMissing are mutually exclusive (inherited)</sch:assert>
      <sch:assert test="not(exists(f:sliceName/@value)) or matches(f:sliceName/@value, '^[a-zA-Z0-9\/\-_\[\]\@]+$')">sliceName must be composed of proper tokens separated by&quot;/&quot; (inherited)</sch:assert>
      <sch:assert test="not(f:isModifier/@value = 'true') or exists(f:isModifierReason)">Must have a modifier reason if isModifier = true (inherited)</sch:assert>
      <sch:assert test="matches(f:path/@value, '^[^\s\.,:;\'&amp;quot;\/|?!@#$%&amp;*()\[\]{}]{1,64}(\.[^\s\.,:;\'&amp;quot;\/|?!@#$%&amp;*()\[\]{}]{1,64}(\[x\])?(\:[^\s\.]+)?)*$')">Element names cannot include some special characters (inherited)</sch:assert>
      <sch:assert test="matches(f:path/@value, '^[A-Za-z][A-Za-z0-9]*(\.[a-z][A-Za-z0-9]*(\[x])?)*$')">Element names should be simple alphanumerics with a max of 64 characters, or code generation tools may be broken (inherited)</sch:assert>
      <sch:assert test="exists(f:sliceName) or not(exists(f:sliceIsConstraining))">sliceIsConstraining can only appear if slicename is present (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.extension</sch:title>
    <sch:rule context="f:ElementDefinition/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.modifierExtension</sch:title>
    <sch:rule context="f:ElementDefinition/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.path</sch:title>
    <sch:rule context="f:ElementDefinition/f:path">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.representation</sch:title>
    <sch:rule context="f:ElementDefinition/f:representation">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.sliceName</sch:title>
    <sch:rule context="f:ElementDefinition/f:sliceName">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.sliceIsConstraining</sch:title>
    <sch:rule context="f:ElementDefinition/f:sliceIsConstraining">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.label</sch:title>
    <sch:rule context="f:ElementDefinition/f:label">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.code</sch:title>
    <sch:rule context="f:ElementDefinition/f:code">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing">
      <sch:assert test="(f:discriminator) or (f:description)">If there are no discriminators, there must be a definition (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing.extension</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing.discriminator</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing/f:discriminator">
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing.discriminator.extension</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing/f:discriminator/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing.discriminator.type</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing/f:discriminator/f:type">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing.discriminator.path</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing/f:discriminator/f:path">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing.description</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing/f:description">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing.ordered</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing/f:ordered">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.slicing.rules</sch:title>
    <sch:rule context="f:ElementDefinition/f:slicing/f:rules">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.short</sch:title>
    <sch:rule context="f:ElementDefinition/f:short">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.definition</sch:title>
    <sch:rule context="f:ElementDefinition/f:definition">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.comment</sch:title>
    <sch:rule context="f:ElementDefinition/f:comment">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.requirements</sch:title>
    <sch:rule context="f:ElementDefinition/f:requirements">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.alias</sch:title>
    <sch:rule context="f:ElementDefinition/f:alias">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.min</sch:title>
    <sch:rule context="f:ElementDefinition/f:min">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.max</sch:title>
    <sch:rule context="f:ElementDefinition/f:max">
      <sch:assert test="@value='*' or (normalize-space(@value)!='' and normalize-space(translate(@value, '0123456789',''))='')">Max SHALL be a number or &quot;*&quot; (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.base</sch:title>
    <sch:rule context="f:ElementDefinition/f:base">
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.base.extension</sch:title>
    <sch:rule context="f:ElementDefinition/f:base/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.base.path</sch:title>
    <sch:rule context="f:ElementDefinition/f:base/f:path">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.base.min</sch:title>
    <sch:rule context="f:ElementDefinition/f:base/f:min">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.base.max</sch:title>
    <sch:rule context="f:ElementDefinition/f:base/f:max">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.contentReference</sch:title>
    <sch:rule context="f:ElementDefinition/f:contentReference">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>f:ElementDefinition/f:type</sch:title>
    <sch:rule context="f:ElementDefinition/f:type">
      <sch:assert test="count(f:profile) &lt;= 0">profile: maximum cardinality of 'profile' is 0</sch:assert>
      <sch:assert test="count(f:aggregation) &lt;= 0">aggregation: maximum cardinality of 'aggregation' is 0</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.type</sch:title>
    <sch:rule context="f:ElementDefinition/f:type">
      <sch:assert test="not(exists(f:aggregation)) or exists(f:code[@value = 'Reference']) or exists(f:code[@value = 'canonical'])">Aggregation may only be specified if one of the allowed types for the element is a reference (inherited)</sch:assert>
      <sch:assert test="not(exists(f:targetProfile)) or (f:code/@value = ('Reference', 'canonical', 'CodeableReference'))">targetProfile is only allowed if the type is Reference or canonical (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.type.extension</sch:title>
    <sch:rule context="f:ElementDefinition/f:type/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.type.code</sch:title>
    <sch:rule context="f:ElementDefinition/f:type/f:code">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.type.profile</sch:title>
    <sch:rule context="f:ElementDefinition/f:type/f:profile">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.type.targetProfile</sch:title>
    <sch:rule context="f:ElementDefinition/f:type/f:targetProfile">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.type.aggregation</sch:title>
    <sch:rule context="f:ElementDefinition/f:type/f:aggregation">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.type.versioning</sch:title>
    <sch:rule context="f:ElementDefinition/f:type/f:versioning">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.defaultValue[x] 1</sch:title>
    <sch:rule context="f:ElementDefinition/f:defaultValue[x]">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.meaningWhenMissing</sch:title>
    <sch:rule context="f:ElementDefinition/f:meaningWhenMissing">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.orderMeaning</sch:title>
    <sch:rule context="f:ElementDefinition/f:orderMeaning">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.fixed[x] 1</sch:title>
    <sch:rule context="f:ElementDefinition/f:fixed[x]">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.pattern[x] 1</sch:title>
    <sch:rule context="f:ElementDefinition/f:pattern[x]">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.example</sch:title>
    <sch:rule context="f:ElementDefinition/f:example">
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.example.extension</sch:title>
    <sch:rule context="f:ElementDefinition/f:example/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.example.label</sch:title>
    <sch:rule context="f:ElementDefinition/f:example/f:label">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.example.value[x] 1</sch:title>
    <sch:rule context="f:ElementDefinition/f:example/f:value[x]">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.minValue[x] 1</sch:title>
    <sch:rule context="f:ElementDefinition/f:minValue[x]">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.maxValue[x] 1</sch:title>
    <sch:rule context="f:ElementDefinition/f:maxValue[x]">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.maxLength</sch:title>
    <sch:rule context="f:ElementDefinition/f:maxLength">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.condition</sch:title>
    <sch:rule context="f:ElementDefinition/f:condition">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.constraint</sch:title>
    <sch:rule context="f:ElementDefinition/f:constraint">
      <sch:assert test="exists(f:expression/@value)">Constraints should have an expression or else validators will not be able to enforce them (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.constraint.extension</sch:title>
    <sch:rule context="f:ElementDefinition/f:constraint/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.constraint.key</sch:title>
    <sch:rule context="f:ElementDefinition/f:constraint/f:key">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.constraint.requirements</sch:title>
    <sch:rule context="f:ElementDefinition/f:constraint/f:requirements">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.constraint.severity</sch:title>
    <sch:rule context="f:ElementDefinition/f:constraint/f:severity">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.constraint.human</sch:title>
    <sch:rule context="f:ElementDefinition/f:constraint/f:human">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.constraint.expression</sch:title>
    <sch:rule context="f:ElementDefinition/f:constraint/f:expression">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.constraint.xpath</sch:title>
    <sch:rule context="f:ElementDefinition/f:constraint/f:xpath">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.constraint.source</sch:title>
    <sch:rule context="f:ElementDefinition/f:constraint/f:source">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.mustSupport</sch:title>
    <sch:rule context="f:ElementDefinition/f:mustSupport">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.isModifier</sch:title>
    <sch:rule context="f:ElementDefinition/f:isModifier">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.isModifierReason</sch:title>
    <sch:rule context="f:ElementDefinition/f:isModifierReason">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.isSummary</sch:title>
    <sch:rule context="f:ElementDefinition/f:isSummary">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.binding</sch:title>
    <sch:rule context="f:ElementDefinition/f:binding">
      <sch:assert test="(starts-with(string(f:valueSet/@value), 'http:') or starts-with(string(f:valueSet/@value), 'https:') or starts-with(string(f:valueSet/@value), 'urn:') or starts-with(string(f:valueSet/@value), '#'))">ValueSet SHALL start with http:// or https:// or urn: (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.binding.extension</sch:title>
    <sch:rule context="f:ElementDefinition/f:binding/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.binding.strength</sch:title>
    <sch:rule context="f:ElementDefinition/f:binding/f:strength">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.binding.description</sch:title>
    <sch:rule context="f:ElementDefinition/f:binding/f:description">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.binding.valueSet</sch:title>
    <sch:rule context="f:ElementDefinition/f:binding/f:valueSet">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.mapping</sch:title>
    <sch:rule context="f:ElementDefinition/f:mapping">
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.mapping.extension</sch:title>
    <sch:rule context="f:ElementDefinition/f:mapping/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.mapping.identity</sch:title>
    <sch:rule context="f:ElementDefinition/f:mapping/f:identity">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.mapping.language</sch:title>
    <sch:rule context="f:ElementDefinition/f:mapping/f:language">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.mapping.map</sch:title>
    <sch:rule context="f:ElementDefinition/f:mapping/f:map">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>ElementDefinition.mapping.comment</sch:title>
    <sch:rule context="f:ElementDefinition/f:mapping/f:comment">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
</sch:schema>
