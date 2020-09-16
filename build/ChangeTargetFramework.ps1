Param(
  [string]$TFparam
)

$newTF = "netstandard2.0;net45;netstandard1.6"

If (![string]::IsNullOrEmpty($TFparam)) 
{
	$newTF = $TFparam
}

$projectFiles = Get-Childitem .. -Recurse -Include *.csproj

foreach($currentProjFile in $projectFiles)
{
  $isTest = $currentProjFile.Name.Contains(".Tests.csproj")  

  # test files are untouched - they are just using only one platform anyway
  if($isTest) { continue }

  Write "Updating target frameworks for $currentProjFile to: $newTF"

  $xml = [xml](get-content $currentProjFile) 

  if($xml.Project.PropertyGroup[0].TargetFrameworks)
  {
    $xml.Project.PropertyGroup[0].TargetFrameworks = $newTF
  }
  
  $xml.Save($currentProjFile)  
}
