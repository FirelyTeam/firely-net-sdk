<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fhir="http://hl7.org/fhir"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" 
>
    <xsl:output method="xml" indent="yes" encoding="UTF-8"  />

	<xsl:template match="fhir:meta"><xsl:text disable-output-escaping="yes">&lt;!-- Removed Meta/LastModified --&gt;</xsl:text></xsl:template>
	<xsl:template match="fhir:CapabilityStatement/fhir:date"><xsl:text disable-output-escaping="yes">&lt;!-- Removed date --&gt;</xsl:text></xsl:template>
	<xsl:template match="fhir:Conformance/fhir:date"><xsl:text disable-output-escaping="yes">&lt;!-- Removed date --&gt;</xsl:text></xsl:template>
	<xsl:template match="fhir:CompartmentDefinition/fhir:date"><xsl:text disable-output-escaping="yes">&lt;!-- Removed date --&gt;</xsl:text></xsl:template>
	<xsl:template match="fhir:OperationDefinition/fhir:date"><xsl:text disable-output-escaping="yes">&lt;!-- Removed date --&gt;</xsl:text></xsl:template>
	<xsl:template match="fhir:StructureDefinition/fhir:date"><xsl:text disable-output-escaping="yes">&lt;!-- Removed date --&gt;</xsl:text></xsl:template>
	<xsl:template match="fhir:ValueSet/fhir:date"><xsl:text disable-output-escaping="yes">&lt;!-- Removed date --&gt;</xsl:text></xsl:template>
	<xsl:template match="fhir:SearchParameter/fhir:date"><xsl:text disable-output-escaping="yes">&lt;!-- Removed date --&gt;</xsl:text></xsl:template>
	<xsl:template match="fhir:NamingSystem/fhir:date"><xsl:text disable-output-escaping="yes">&lt;!-- Removed date --&gt;</xsl:text></xsl:template>
	<xsl:template match="@*"><xsl:value-of select="." disable-output-escaping="yes"></xsl:value-of></xsl:template>
	
    <xsl:template match="@*|node()" >
        <xsl:copy >
            <xsl:apply-templates select="@*|node()" />
        </xsl:copy>
    </xsl:template>
</xsl:stylesheet>
