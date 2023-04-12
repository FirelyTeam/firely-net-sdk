# ---------------------------------------------------------------------------
# Download the published content from the FHIR specification
# ---------------------------------------------------------------------------
# cls

# Script to be run from 'build' directory
using namespace System.Collections.Generic

param(
     [Parameter(Mandatory, HelpMessage="Enter the url 	from which the specification is to be downloaded")]
     [string]$server = "http://hl7.org/fhir/STU3/",
	 
	 [ValidateSet('STU3', 'R4', 'R4B', 'R5')]
     [Parameter(Mandatory, HelpMessage="Enter the Fhir Release ('STU3', 'R4', 'R4B' or 'R5')")]
     [string] $fhirRelease = "STU3" 
)

$baseDir = Resolve-Path ..
$srcdir = "$baseDir\src";

# These are all files from the spec that we need. Narratives are stripped after download
$allFiles = [List[string]]@(
				"conceptmaps.xml",
				"extension-definitions.xml", 
				"namingsystem-registry.xml",
				"profiles-others.xml", 
				"profiles-resources.xml", 
				"profiles-types.xml" 
				"search-parameters.xml",
				"valuesets.xml"
				"fhir-all-xsd.zip"
				);

if ($fhirRelease -eq "R5")
{
	# remove extension-definitions.xml and namingsystem-registry.xml because from R5 those files do not exist anymore.
	$allFiles.Remove("extension-definitions.xml"); #
	$allFiles.Remove("namingsystem-registry.xml"); #
}

function New-TemporaryDirectory {
    $parent = [System.IO.Path]::GetTempPath()
    $name = [System.IO.Path]::GetRandomFileName()
	$fullpath = Join-Path $parent $name
    New-Item -ItemType Directory -Path $fullpath
}

$tempDir = New-TemporaryDirectory

# Download a file from a URL and place it at the specified target path.
# This also has some basic capabilities to go through proxies without any additional configuration.
function PowerCurl($targetPath, $sourceUrl)
{
	Try
	{
		Write-Host -ForegroundColor White "downloading $sourceUrl to $targetPath ..."
        $webclient = New-Object System.Net.WebClient
		$webclient.Encoding = [System.Text.Encoding]::UTF8
        $webclient.DownloadFile($sourceUrl,$targetPath)
	} Catch
	{
		Write-Host -ForegroundColor Red "$_ occurred while downloading $sourceUrl to $targetPath"
		$_ | Format-List * -Force
	}
}

function GetSpecFile($name)
{
	$targetPath = Join-Path $tempDir $name
	$sourceUrl = $server + $name
	PowerCurl $targetPath $sourceUrl
}

function CopySpecFile($name, $destDir)
{
	$sourcePath = Join-Path $tempDir $name
	$destPath = Join-Path $destDir $name
	Copy-Item $sourcePath $destPath
}

function RemoveXmlNode($xml, $nsmgr, $xpath)
{
	$node = $xml.SelectSingleNode($xpath, $nsmgr )
	
	if ($node -ne $null) {
		$node.ParentNode.RemoveChild($node)
	}
	else {
	    Write-Host "not found"
	}
}

function RemoveIncorrectXsdElements($file)
{
	[xml]$xml = Get-Content $file

	$nametable = new-object System.Xml.NameTable;
	$nsmgr = new-object System.Xml.XmlNamespaceManager($nametable);
	$nsmgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
		
	RemoveXmlNode  $xml $nsmgr "//xs:simpleType[@name='Event Or Request Resource Types-list']"
	RemoveXmlNode  $xml $nsmgr "//xs:complexType[@name='Event Or Request Resource Types']"

	$xml.save($file)
}

function RemoveNarrative($name)
{
	$file = Join-Path $tempDir $name
	[xml]$xml = Get-Content $file
	
    $ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
    $ns.AddNamespace("ns", "http://hl7.org/fhir")
	$ns.AddNamespace("xh", "http://www.w3.org/1999/xhtml")

	$childNodes = $xml.SelectNodes("//ns:resource//ns:text/xh:div/child::*", $ns)
	
	foreach ($child in $childNodes)
	{
		[void]$child.ParentNode.RemoveChild($child)
	}

	$divs = $xml.SelectNodes("//ns:resource//ns:text/xh:div", $ns)
	ForEach ($div in $divs) 
	{
		$newNode = $xml.CreateElement("p", $ns.LookupNamespace("xh"))
		$xmlText = $xml.CreateTextNode("The narrative has been removed to reduce the size of the distribution of the Hl7.Fhir.Specification library")
		$newNode.AppendChild($xmlText) | Out-null
		$div.AppendChild($newNode) | Out-null
	}
	Write-Output "Removed narratives from $file"
	# Store it again
	$xml.Save($file)
}

function RemoveDoubleVersionsInValueSets($name)
{
	if ($server.EndsWith('2021May/') )
	{
		# Correction for R5, 4.6.0:
		# Removed double version '|4.6.0|4.6.0' from <valueSet> 
		
		$filename = Join-Path $tempDir $name
		[xml]$xml = Get-Content $filename
		
		$ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
		$ns.AddNamespace("ns", "http://hl7.org/fhir") 
		
		$valuesets = $xml.SelectNodes('//ns:valueSet[substring(@value,string-length(@value) - string-length("|4.6.0|4.6.0") + 1) = "|4.6.0|4.6.0"]', $ns)
		
		foreach ($valueset in $valuesets)
		{
			$valueAttribute = $valueset.GetAttribute('value')
			$valueAttribute = $valueAttribute.Replace("|4.6.0|", "|");
			$valueset.SetAttribute('value', $valueAttribute)  | Out-null
		}
		
		Write-Output "Removed double version '|4.6.0|4.6.0' from '<valueSet> $file"
		$xml.Save($filename)
	}
}

function ChangeValueElement($name)
{
	# Correction for 4B:
	# changes Element "valueUri" to "valueUrl" of extension `structuredefinition-fhir-type`

	$filename = Join-Path $tempDir $name
	[xml]$xml = Get-Content $filename
	
	$ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
	$ns.AddNamespace("ns", "http://hl7.org/fhir")

	$extensionNodes = $xml.SelectNodes('//ns:extension[@url="http://hl7.org/fhir/StructureDefinition/structuredefinition-fhir-type"]', $ns)
	
	foreach ($extension in $extensionNodes)
	{
		$valueElement = $extension.FirstChild
		$correctedValueElement = $xml.CreateElement("valueUrl", $ns.LookupNamespace("ns"))
		$correctedValueElement.SetAttribute("value", $valueElement.Value) | Out-null
		$extension.ReplaceChild($correctedValueElement, $valueElement)  | Out-null
	}

	Write-Output "Changed 'valueUri' to 'valueUrl' for extension 'structuredefinition-fhir-type' from $file"
	$xml.Save($filename)
}


function RemoveDefinitonExtension($name)
{
	# Correction for 4B:
	# remove the extension http://hl7.org/fhir/build/StructureDefinition/definition
	
	$filename = Join-Path $tempDir $name
	[xml]$xml = Get-Content $filename
	
	$ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
	$ns.AddNamespace("ns", "http://hl7.org/fhir")

	$extensionNodes = $xml.SelectNodes('//ns:extension[@url="http://hl7.org/fhir/build/StructureDefinition/definition"]', $ns)
	
	foreach ($extension in $extensionNodes)
	{
		[void]$extension.ParentNode.RemoveChild($extension)
	}
	
	Write-Output "Removed the extension http://hl7.org/fhir/build/StructureDefinition/definition from $file"
	
	$xml.Save($filename)
}


function ExtractXsdZipFile($destPath)
{
	Write-Host -ForegroundColor White "Extract xsd zip file..."
	$zipPath = Join-Path $tempDir "fhir-all-xsd.zip"
	$extractPath = Join-Path $tempDir "extracted"
	Expand-Archive -path $zipPath -destinationpath $extractPath
	
	if ($server.EndsWith('2020Sep/') -or $server.EndsWith('2021Mar/') )
	{
		# In release 2020Sep is an error in the fhir-single.xsd.  
		Write-Host -ForegroundColor White ".. corrected errors in fhir-single.xsd"
		$xsdFile = Join-Path $extractPath "fhir-single.xsd"
		RemoveIncorrectXsdElements $xsdFile
	}
	Write-Host -ForegroundColor White "Copy extracted files to $destPath ..."
	Copy-Item -Path $extractPath\* -Recurse -Container:$false -Destination $destPath
}

function ChangeValueElementOfFhirType($name)
{
	# Correction for R5 (5.0.0-snapshot1):
	# Change type of value[x] of the extension structuredefinition-fhir-type to url
	
	$filename = Join-Path $tempDir $name
	[xml]$xml = Get-Content $filename
	
	$ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
	$ns.AddNamespace("ns", "http://hl7.org/fhir")

	$typeNodes = $xml.SelectNodes('//ns:StructureDefinition[ns:id[@value = "structuredefinition-fhir-type"]]//ns:element[@id = "Extension.value[x]"]/ns:type', $ns)
	
	foreach ($typeNode in $typeNodes)
	{
		$codeElement = $typeNode.FirstChild
		$correctedCodeElement = $xml.CreateElement("code", $ns.LookupNamespace("ns"))
		$correctedCodeElement.SetAttribute("value", "url") | Out-null
		$typeNode.ReplaceChild($correctedCodeElement, $codeElement)  | Out-null
	}
	
	Write-Output "Changed type of 'value[x]' of structuredefinition-fhir-type from 'uri' to 'url'"
	
	$xml.Save($filename)
}

function CorrectConceptmap($name)
{
	# Correction for R4B (4.3.0-snapshot1):
	# Change node <relationship> to <equivalence> for resource ConceptMap
	
	if (!$name.EndsWith('.xml'))
	{
		return;
	}
	
	$filename = Join-Path $tempDir $name
	[xml]$xml = Get-Content $filename
	
	$ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
	$ns.AddNamespace("ns", "http://hl7.org/fhir")

	$relationships = $xml.SelectNodes('//ns:ConceptMap/ns:group/ns:element/ns:target/ns:relationship', $ns)
	
	foreach ($relationship in $relationships)
	{
		$correctedElement = $xml.CreateElement("equivalence", $ns.LookupNamespace("ns"))
		
		$equivalenceValue = $relationship.Value
		if ($equivalenceValue -eq "source-is-broader-than-target")
		{
			$equivalenceValue = "wider"
		}
		if ($equivalenceValue -eq "source-is-narrower-than-target")
		{
			$equivalenceValue = "narrower"
		}
		$correctedElement.SetAttribute("value", $equivalenceValue) | Out-null
		
		$childNodes = $relationship.ChildNodes;
		
		foreach ($childNode in $childNodes)
		{
			$newChild = $childNode.CloneNode("False")
			$correctedElement.AppendChild($newChild)| Out-null
		}
		
		$relationship.ParentNode.AppendChild($correctedElement) | Out-null
        $relationship.ParentNode.RemoveChild($relationship) | Out-null
	}
	
	Write-Output "Change node <relationship> to <equivalence> for resource ConceptMap"
	
	$xml.Save($filename)
}

function ExtractPackageToDirectory($packageBaseUrl, $packageName, $packageVersion, $destDir)
{
	$packageUrl = $packageBaseUrl + $packageName + "/" + $packageVersion;

	$destFile = Join-Path $tempDir $packageName
	$destFile = $destFile + ".tgz";

	$extractDir = Join-Path $tempDir $packageName

	# Download extension package
	PowerCurl $destFile $packageUrl
	
	# Unpack extension package
	New-Item -ItemType Directory $extractDir -Force | Out-Null
	tar -xf $destFile -C $extractDir

	# move part of the content to specification dir
	New-Item -ItemType Directory $destDir -Force | Out-Null
	$exclude = @('.index.json','package.json')
	Copy-Item -Path $extractDir\Package\* -Filter "*.json" -Container:$false -Destination  $destDir  -Exclude $exclude 
}

# Start of processing:


foreach($file in $allFiles)			
{
	GetSpecFile $file
	if ($file.EndsWith('.xml'))
	{
		RemoveNarrative $file
	}
	
	# Corrections for 4B:
	if ($server.EndsWith("2021Mar/"))
	{
		if ($file.EndsWith('.xml'))
		{
			RemoveDefinitonExtension $file
			ChangeValueElement $file
		}
	}
	
	# Corrections for R4B (4.3.0-snapshot1)
	if ($server.EndsWith("4.3.0-snapshot1/"))
	{
		CorrectConceptmap $file
	}
	
	# Corrections for R5 (4.6.0)
	if ($server.EndsWith("2021May/"))
	{
		if ($file.EndsWith('profiles-resources.xml'))
		{
			RemoveDoubleVersionsInValueSets $file
			RemoveDefinitonExtension $file
		}
	}
	
	# Corrections for R5 (5.0.0-snapshot1)
	if ($server.EndsWith("5.0.0-snapshot1/"))
	{
		if ($file.EndsWith('extension-definitions.xml'))
		{
			ChangeValueElementOfFhirType $file
		}
	}
}

$specificationDir = New-TemporaryDirectory

Write-Host -ForegroundColor White "Copy files to project..."

# Copy the files necessary for the specification library (specification.zip / data)
foreach($specFile in $allFiles)			
{
	if ($specFile.EndsWith(".xml")) 
	{
		Write-Host "Copy $specFile to $specificationDir"
		CopySpecFile $specFile $specificationDir
	}
}

if ($fhirRelease -eq "R5")
{
	# download package extensions and content to the specification.zip
	$extensionDir = Join-Path $specificationDir "extensions"
	ExtractPackageToDirectory -packageBaseUrl "http://packages2.fhir.org/packages/" -packageName "hl7.fhir.uv.extensions.r5" -packageVersion "1.0.0" -destDir $extensionDir;
}

# Add example files used for testing

if ($fhirRelease -eq "STU3") 
{
	PowerCurl "$srcdir\Hl7.Fhir.$fhirRelease.Tests\TestData\careplan-example-f201-renal.xml" "$server/careplan-example-f201-renal.xml"
	PowerCurl "$srcdir\Hl7.Fhir.$fhirRelease.Tests\TestData\testscript-example(example).xml" "$server/testscript-example.xml"
	PowerCurl "$srcdir\Hl7.Fhir.Serialization.$fhirRelease.Tests\TestData\json-edge-cases.json" "$server/json-edge-cases.json"
}

PowerCurl "$srcdir\Hl7.Fhir.$fhirRelease.Tests\TestData\examples.zip" "$server/examples.zip"
PowerCurl "$srcdir\Hl7.Fhir.$fhirRelease.Tests\TestData\examples-json.zip" "$server/examples-json.zip"
PowerCurl "$srcdir\Hl7.Fhir.$fhirRelease.Tests\TestData\json-edge-cases.json" "$server/json-edge-cases.json"

PowerCurl "$srcdir\Hl7.Fhir.Specification.$fhirRelease.Tests\TestData\careplan-example-integrated.xml" "$server/careplan-example-integrated.xml"
PowerCurl "$srcdir\Hl7.Fhir.Specification.$fhirRelease.Tests\TestData\profiles-types.json" "$server/profiles-types.json"
PowerCurl "$srcdir\Hl7.Fhir.Specification.$fhirRelease.Tests\TestData\snapshot-test\profiles-resources.xml" "$server/profiles-resources.xml"

# extract schemas and xsd from fhir-all.zip -> temp specification directory
ExtractXsdZipFile $specificationDir

Compress-Archive -Path $specificationDir\* -DestinationPath $srcdir\Hl7.Fhir.Specification.Data.$fhirRelease\specification.zip -Force