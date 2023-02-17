<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
  xmlns:fhir="http://hl7.org/fhir">
  
    <!-- [WMR 20170421] Remove invalid slice names from Chris Grenz examples -->
  
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template match="fhir:sliceName[contains(@value, '.')]">
    <xsl:comment>
      <xsl:value-of select="local-name()"/>
      <xsl:text> </xsl:text>
      <xsl:for-each select="@*">
        <xsl:value-of select="local-name()"/>
        <xsl:text>="</xsl:text>
        <xsl:value-of select="."/>
        <xsl:text>" </xsl:text>
      </xsl:for-each>      
    </xsl:comment>
  </xsl:template>
  
</xsl:stylesheet>
