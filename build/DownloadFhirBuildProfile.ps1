# ---------------------------------------------------------------------------
# Download the published content from the FHIR specification
# ---------------------------------------------------------------------------
# cls

# Script to be run from 'build' directory

$server = "http://hl7.org/fhir/STU3/";
$baseDir = Resolve-Path ..
$srcdir = "$baseDir\src";

# Apply this transform to remove all the Meta sections from the profiles (to remove the LastUpdated tags) 
# this makes it easier to see the actual changes between versions
$xslt = New-Object System.Xml.Xsl.XslCompiledTransform;
$xslt.Load("$baseDir\Build\StripLastModified.xslt");

# These are all files from the spec that we need. Last modified dates are stripped after download
$allFiles = @("conceptmaps.xml", "dataelements.xml", "expansions.xml", "extension-definitions.xml", "namingsystem-registry.xml",
				"profiles-others.xml", "profiles-resources.xml", "profiles-types.xml", "search-parameters.xml", "v2-tables.xml",
				"v3-codesystems.xml", "valuesets.xml");

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

foreach($file in $allFiles)			
{
	GetSpecFile $file
	TransformSpecfile $file
}

Write-Host -ForegroundColor White "Copy files to project..."

# Copy the files necessary for code generation
CopySpecFile "expansions.xml" "$srcdir\Hl7.Fhir.Core\Model\Source"
CopySpecFile "profiles-resources.xml" "$srcdir\Hl7.Fhir.Core\Model\Source"
CopySpecFile "profiles-types.xml" "$srcdir\Hl7.Fhir.Core\Model\Source"
CopySpecFile "profiles-others.xml" "$srcdir\Hl7.Fhir.Core\Model\Source"
CopySpecFile "search-parameters.xml" "$srcdir\Hl7.Fhir.Core\Model\Source"

# Copy the files necessary for the specification library (specification.zip / data)
CopySpecFile "conceptmaps.xml" "$srcdir\Hl7.Fhir.Specification\data"
CopySpecFile "dataelements.xml" "$srcdir\Hl7.Fhir.Specification\data"
#CopySpecFile "expansions.xml" "$srcdir\Hl7.Fhir.Specification\data"
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

# Still need to add:
# extract schemas and xsd from fhir-all.zip -> data
