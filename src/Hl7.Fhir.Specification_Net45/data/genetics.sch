<?xml version="1.0" encoding="UTF-8"?>
<sch:schema xmlns:sch="http://purl.oclc.org/dsdl/schematron" queryBinding="xslt2">
  <sch:ns prefix="f" uri="http://hl7.org/fhir"/>
  <sch:ns prefix="h" uri="http://www.w3.org/1999/xhtml"/>
  <sch:pattern>
    <sch:title>f:Observation</sch:title>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsGenomeBuild']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsGenomeBuild': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsChromosome']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsChromosome': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsGenomicStart']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsGenomicStart': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsGenomicStop']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsGenomicStop': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsReferenceAllele']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsReferenceAllele': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsObservedAllele']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsObservedAllele': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsTranscriptReferenceSequenceId']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsTranscriptReferenceSequenceId': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsProteinReferenceSequenceId']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsProteinReferenceSequenceId': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsCIGAR']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsCIGAR': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsDNASequenceVariation']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsDNASequenceVariation': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsVariationId']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsVariationId': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsDNASequenceVariationType']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsDNASequenceVariationType': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsAminoAcidChange']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsAminoAcidChange': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsAminoAcidChangeType']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsAminoAcidChangeType': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsGene']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsGene': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsDNARegionName']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsDNARegionName': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsAlleleName']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsAlleleName': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsGenomicSourceClass']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsGenomicSourceClass': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsSpecies']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsSpecies': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsResult']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsResult': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsAssessedCondition']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsAssessedCondition': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsAllelicState']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsAllelicState': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsAllelicFrequency']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsAllelicFrequency': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsCopyNumberEvent']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsCopyNumberEvent': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="count(f:extension[@url = 'http://hl7.org/fhir/StructureDefinition/geneticsReadCoverage']) &lt;= 1">extension with URL = 'http://hl7.org/fhir/StructureDefinition/geneticsReadCoverage': maximum cardinality is 1</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="not(exists(f:dataAbsentReason)) or (not(exists(*[starts-with(local-name(.), 'value')])))">SHALL only be present if Observation.value[x] is not present (inherited</sch:assert>
    </sch:rule>
    <sch:rule context="f:Observation">
      <sch:assert test="not(exists(f:component/f:code)) or count(for $coding in f:code/f:coding return parent::*/f:component/f:code/f:coding[f:code/@value=$coding/f:code/@value and f:system/@value=$coding/f:system/@value])=0">Component code SHALL not be same as observation code (inherited</sch:assert>
    </sch:rule>
    </sch:pattern>
  <sch:pattern>
    <sch:title>Observation.referenceRange</sch:title>
    <sch:rule context="f:Observation/f:referenceRange">
      <sch:assert test="(exists(f:low) or exists(f:high)or exists(f:text))">Must have at least a low or a high or text (inherited</sch:assert>
    </sch:rule>
    </sch:pattern>
</sch:schema>
