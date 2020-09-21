# This script takes a single parameter, the (possible) concatenated list of platforms to build against.
# There is a sensible default, so it will run without a param as well.

Param(
  [string]$newTF = "netstandard2.0;net45;netstandard1.6"
)

# Scan for all project files in the /src directory, but exclude test projects
$projectFiles =  Get-Childitem .. -Recurse -Include *.csproj -Exclude *.Tests.csproj

foreach($currentProjFile in $projectFiles)
{
  Write "Updating target frameworks for $currentProjFile to: $newTF"

  # read the XML for the project file
  $xml = [xml](get-content $currentProjFile) 

  # If the project specifies a target framework, overwrite it with the new one
  if($xml.Project.PropertyGroup[0].TargetFrameworks)
  {
    $xml.Project.PropertyGroup[0].TargetFrameworks = $newTF
  }

  # save the xml back to the original project file  
  $xml.Save($currentProjFile)  
}
