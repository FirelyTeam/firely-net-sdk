# ---------------------------------------------------------------------------
# Download the published content from the FHIR specification
# ---------------------------------------------------------------------------
# cls

# Script to be run from 'build' directory

$server = "http://hl7.org/fhir/";
$baseDir = Resolve-Path ..
$srcdir = "$baseDir\src";


# These are all files from the spec that we need. Narratives are stripped after download
$allFiles = @("conceptmaps.xml",				
				"extension-definitions.xml", 
				"namingsystem-registry.xml",
				"profiles-others.xml", 
				"profiles-resources.xml", 
				"profiles-types.xml" 
				"search-parameters.xml",
				"v2-tables.xml",
				"v3-codesystems.xml",
				"valuesets.xml"
				"fhir-all-xsd.zip"
				);

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


function TransformSpecfile($name)
{
	Write-Host -ForegroundColor White "transforming $name..."
	$transformPath = Join-Path $tempDir $name
	$tempTransformPath = $transformPath + "-temp"

	$xslt.Transform($transformPath, $tempTransformPath);
	Remove-Item $transformPath
	Move-Item $tempTransformPath $transformPath
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

function ExtractXsdZipFile($destPath)
{
	Write-Host -ForegroundColor White "Extract xsd zip file..."
	$zipPath = Join-Path $tempDir "fhir-all-xsd.zip"
	$extractPath = Join-Path $tempDir "extracted"
	expand-archive -path $zipPath -destinationpath $extractPath
	
	if ($server.EndsWith('2020Sep/') )
	{
		# In release 2020Sep is an error in the fhir-single.xsd.  
		Write-Host -ForegroundColor White ".. correct errors in fhir-single.xsd"
		$xsdFile = Join-Path $extractPath "fhir-single.xsd"
		RemoveIncorrectXsdElements $xsdFile
	}
	Write-Host -ForegroundColor White "Copy extracted files to $destPath ..."
	Copy-Item -Path $extractPath\* -Recurse -Container:$false -Destination $destPath
}

foreach($file in $allFiles)			
{
	GetSpecFile $file
	if ($file.EndsWith('.xml'))
	{
		RemoveNarrative $file
	}
}

Write-Host -ForegroundColor White "Copy files to project..."

# Copy the files necessary for the specification library (specification.zip / data)
Remove-Item "$srcdir\Hl7.Fhir.Specification\data\*.*" -Force
CopySpecFile "conceptmaps.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "extension-definitions.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "namingsystem-registry.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "profiles-others.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "profiles-resources.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "profiles-types.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "search-parameters.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "v2-tables.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "v3-codesystems.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "valuesets.xml" "$srcdir\Hl7.Fhir.Specification\data"

# Add example files used for testing
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\careplan-example-f201-renal.xml" "$server/careplan-example-f201-renal.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\testscript-example(example).xml" "$server/testscript-example.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\valueset-v2-0717.json" "$server/v2/0717/v2-0717.vs.json"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\examples.zip" "$server/examples.zip"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\examples-json.zip" "$server/examples-json.zip"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\json-edge-cases.json" "$server/json-edge-cases.json"
PowerCurl "$srcdir\Hl7.Fhir.Serialization.Tests\TestData\json-edge-cases.json" "$server/json-edge-cases.json"
PowerCurl "$srcdir\Hl7.Fhir.Specification.Tests\TestData\careplan-example-integrated.xml" "$server/careplan-example-integrated.xml"
PowerCurl "$srcdir\Hl7.Fhir.Specification.Tests\TestData\profiles-types.json" "$server/profiles-types.json"

# extract schemas and xsd from fhir-all.zip -> data
ExtractXsdZipFile "$srcdir\Hl7.Fhir.Specification\data"