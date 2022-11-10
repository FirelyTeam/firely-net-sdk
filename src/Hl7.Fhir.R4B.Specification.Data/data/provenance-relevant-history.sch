<?xml version="1.0" encoding="UTF-8"?>
<sch:schema xmlns:sch="http://purl.oclc.org/dsdl/schematron" queryBinding="xslt2">
  <sch:ns prefix="f" uri="http://hl7.org/fhir"/>
  <sch:ns prefix="h" uri="http://www.w3.org/1999/xhtml"/>
  <!-- 
    This file contains just the constraints for the profile Provenance
    It includes the base constraints for the resource as well.
    Because of the way that schematrons and containment work, 
    you may need to use this schematron fragment to build a, 
    single schematron that validates contained resources (if you have any) 
  -->
  <sch:pattern>
    <sch:title>f:Provenance</sch:title>
    <sch:rule context="f:Provenance">
      <sch:assert test="count(f:occurred[x]) &gt;= 1">occurred[x]: minimum cardinality of 'occurred[x]' is 1</sch:assert>
      <sch:assert test="count(f:activity) &gt;= 1">activity: minimum cardinality of 'activity' is 1</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance</sch:title>
    <sch:rule context="f:Provenance">
      <sch:assert test="not(parent::f:contained and f:contained)">If the resource is contained in another resource, it SHALL NOT contain nested Resources (inherited)</sch:assert>
      <sch:assert test="not(exists(for $contained in f:contained return $contained[not(exists(parent::*/descendant::f:reference/@value=concat('#', $contained/*/f:id/@value)) or exists(descendant::f:reference[@value='#']))]))">If the resource is contained in another resource, it SHALL be referred to from elsewhere in the resource or SHALL refer to the containing resource (inherited)</sch:assert>
      <sch:assert test="not(exists(f:contained/*/f:meta/f:versionId)) and not(exists(f:contained/*/f:meta/f:lastUpdated))">If a resource is contained in another resource, it SHALL NOT have a meta.versionId or a meta.lastUpdated (inherited)</sch:assert>
      <sch:assert test="not(exists(f:contained/*/f:meta/f:security))">If a resource is contained in another resource, it SHALL NOT have a security label (inherited)</sch:assert>
      <sch:assert test="exists(f:text/h:div)">A resource should have narrative for robust management (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.meta</sch:title>
    <sch:rule context="f:Provenance/f:meta">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.implicitRules</sch:title>
    <sch:rule context="f:Provenance/f:implicitRules">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.language</sch:title>
    <sch:rule context="f:Provenance/f:language">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.text</sch:title>
    <sch:rule context="f:Provenance/f:text">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.contained</sch:title>
    <sch:rule context="f:Provenance/f:contained">
      <sch:assert test="not(f:Citation|f:Evidence|f:EvidenceReport|f:EvidenceVariable|f:MedicinalProductDefinition|f:PackagedProductDefinition|f:AdministrableProductDefinition|f:Ingredient|f:ClinicalUseDefinition|f:RegulatedAuthorization|f:SubstanceDefinition|f:SubscriptionStatus|f:SubscriptionTopic) or not(parent::f:Citation|parent::f:Evidence|parent::f:EvidenceReport|parent::f:EvidenceVariable|parent::f:MedicinalProductDefinition|parent::f:PackagedProductDefinition|parent::f:AdministrableProductDefinition|parent::f:Ingredient|parent::f:ClinicalUseDefinition|parent::f:RegulatedAuthorization|parent::f:SubstanceDefinition|f:SubscriptionStatus|f:SubscriptionTopic)">Containing new R4B resources within R4 resources may cause interoperability issues if instances are shared with R4 systems (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.extension</sch:title>
    <sch:rule context="f:Provenance/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.modifierExtension</sch:title>
    <sch:rule context="f:Provenance/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.target</sch:title>
    <sch:rule context="f:Provenance/f:target">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.occurred[x] 1</sch:title>
    <sch:rule context="f:Provenance/f:occurred[x]">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.recorded</sch:title>
    <sch:rule context="f:Provenance/f:recorded">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.policy</sch:title>
    <sch:rule context="f:Provenance/f:policy">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.location</sch:title>
    <sch:rule context="f:Provenance/f:location">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.reason</sch:title>
    <sch:rule context="f:Provenance/f:reason">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.activity</sch:title>
    <sch:rule context="f:Provenance/f:activity">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>f:Provenance/f:agent</sch:title>
    <sch:rule context="f:Provenance/f:agent">
      <sch:assert test="count(f:type) &gt;= 1">type: minimum cardinality of 'type' is 1</sch:assert>
      <sch:assert test="count(f:type) &gt;= 1">type: minimum cardinality of 'type' is 1</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.agent</sch:title>
    <sch:rule context="f:Provenance/f:agent">
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.agent.extension</sch:title>
    <sch:rule context="f:Provenance/f:agent/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.agent.modifierExtension</sch:title>
    <sch:rule context="f:Provenance/f:agent/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.agent.type</sch:title>
    <sch:rule context="f:Provenance/f:agent/f:type">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.agent.role</sch:title>
    <sch:rule context="f:Provenance/f:agent/f:role">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.agent.who</sch:title>
    <sch:rule context="f:Provenance/f:agent/f:who">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.agent.onBehalfOf</sch:title>
    <sch:rule context="f:Provenance/f:agent/f:onBehalfOf">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.entity</sch:title>
    <sch:rule context="f:Provenance/f:entity">
      <sch:assert test="@value|f:*|h:div|self::f:Parameters">All FHIR elements must have a @value or children unless an empty Parameters resource (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.entity.extension</sch:title>
    <sch:rule context="f:Provenance/f:entity/f:extension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.entity.modifierExtension</sch:title>
    <sch:rule context="f:Provenance/f:entity/f:modifierExtension">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
      <sch:assert test="exists(f:extension)!=exists(f:*[starts-with(local-name(.), &quot;value&quot;)])">Must have either extensions or value[x], not both (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.entity.role</sch:title>
    <sch:rule context="f:Provenance/f:entity/f:role">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.entity.what</sch:title>
    <sch:rule context="f:Provenance/f:entity/f:what">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.entity.agent</sch:title>
    <sch:rule context="f:Provenance/f:entity/f:agent">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
  <sch:pattern>
    <sch:title>Provenance.signature</sch:title>
    <sch:rule context="f:Provenance/f:signature">
      <sch:assert test="@value|f:*|h:div">All FHIR elements must have a @value or children (inherited)</sch:assert>
    </sch:rule>
  </sch:pattern>
</sch:schema>
