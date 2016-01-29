properties { 
  $majorWithReleaseVersion = "0.90.6"    # Update this for a new release

  $baseDir  = resolve-path ..
  $sourceDir = "$baseDir\src"

  $zipFileName = "FhirNetApi.zip"
  $nugetPrelease = $null
  $version = GetVersion $majorWithReleaseVersion
  $packageId = "Hl7.Fhir.DSTU21"
  $signAssemblies = $true
  $signKeyPath = "$sourceDir\FhirNetApi.snk"
  $buildNuGet = $true
  $treatWarningsAsErrors = $false
  $workingName = if ($workingName) {$workingName} else {"working"}
  
  $buildDir = "$baseDir\build"
  $toolsDir = "$baseDir\tools"
  $docDir = "$baseDir\doc"
  $releaseDir = "$baseDir\release"
  $workingDir = "$baseDir\$workingName"
  $workingSourceDir = "$workingDir\src"
  $builds = @(
    @{SlnName = "Hl7.Fhir.Net45"; PrjNames = "Hl7.Fhir.Core","Hl7.Fhir.Specification"; TestsNames = "Hl7.Fhir.Core.Tests","Hl7.Fhir.Specification.Tests"; BuildFunction = "MSBuildBuild"; TestsFunction = "MSTests"; Constants="NET45"; FinalDir="Net45"; NuGetDir = "net45"; Framework="net-4.0"}
#    @{Name = "Newtonsoft.Json.Portable45"; TestsName = "Newtonsoft.Json.Tests.Portable40"; BuildFunction = "MSBuildBuild"; TestsFunction = "NUnitTests"; Constants="PORTABLE45"; FinalDir="Portable45"; NuGetDir = "portable-net45+netcore45+wpa81+wp8"; Framework="net-4.0"},
#    @{Name = "Newtonsoft.Json.Net40"; TestsName = "Newtonsoft.Json.Tests.Net40"; BuildFunction = "MSBuildBuild"; TestsFunction = "NUnitTests"; Constants="NET40"; FinalDir="Net40"; NuGetDir = "net40"; Framework="net-4.0"}
  )
}

framework '4.6x86'

task default -depends Test

# Ensure a clean working directory
task Clean {
  Write-Host "Setting location to $baseDir"
  Set-Location $baseDir
  
  if (Test-Path -path $workingDir)
  {
    Write-Host "Deleting existing working directory $workingDir"
    
    Execute-Command -command { del $workingDir -Recurse -Force }
  }
  
  Write-Host "Creating working directory $workingDir"
  New-Item -Path $workingDir -ItemType Directory
}

# Build each solution, optionally signed
task Build -depends Clean { 

  Write-Host "Copying source to working source directory $workingSourceDir"
  robocopy $sourceDir $workingSourceDir /MIR /NP /XD bin obj TestResults AppPackages $packageDirs .vs artifacts /XF *.suo *.user *.lock.json | Out-Default

  Write-Host -ForegroundColor Green "Updating assembly version"
  Write-Host
  Update-AssemblyInfoFiles $workingSourceDir ($majorWithReleaseVersion) $version


  foreach ($build in $builds)
  {
    $slnName = $build.SlnName
    if ($slnName -ne $null)
    {
      Write-Host -ForegroundColor Green "Building solution: " $slnName
      Write-Host -ForegroundColor Green "Strong Name:       " $signAssemblies
      Write-Host -ForegroundColor Green "Strong Name Key:   " $signKeyPath

      & $build.BuildFunction $build
    }
  }
}

# Optional build documentation, add files to final zip
task Package -depends Build {
  foreach ($build in $builds)
  {
    $prjNames = $build.PrjNames
    $finalDir = $build.FinalDir
    
    foreach($prj in $prjNames)
    {
        robocopy "$workingSourceDir\$prj\bin\Release\$finalDir" $workingDir\Package\Bin\$finalDir *.dll *.pdb *.xml /NFL /NDL /NJS /NC /NS /NP /XO /XF *.CodeAnalysisLog.xml | Out-Default
    }
  }
  
  if ($buildNuGet)
  {
    $nugetVersion = $majorWithReleaseVersion
    if ($nugetPrelease -ne $null)
    {
      $nugetVersion = $nugetVersion + "-" + $nugetPrelease
    }

    New-Item -Path $workingDir\NuGet -ItemType Directory

    foreach($build in $builds)
    {
      $prjNames = $build.PrjNames
      foreach($prj in $prjNames)
      {
        $nuspecPath = "$workingDir\NuGet\$prj.nuspec"
        Copy-Item -Path "$buildDir\$prj.nuspec" -Destination $nuspecPath -recurse

        Write-Host "Updating nuspec file at $nuspecPath" -ForegroundColor Green
        Write-Host

        $xml = [xml](Get-Content $nuspecPath)
        Edit-XmlNodes -doc $xml -xpath "//*[local-name() = 'id']" -value $packageId
        Edit-XmlNodes -doc $xml -xpath "//*[local-name() = 'version']" -value $nugetVersion

        Write-Host $xml.OuterXml

        $xml.save($nuspecPath)
      }
    }

    New-Item -Path $workingDir\NuGet\tools -ItemType Directory
    Copy-Item -Path "$buildDir\install.ps1" -Destination $workingDir\NuGet\tools\install.ps1 -recurse
    
    foreach ($build in $builds)
    {
      if ($build.NuGetDir)
      {
        $name = $build.TestsName
        $finalDir = $build.FinalDir
        $frameworkDirs = $build.NuGetDir.Split(",")
        
        foreach ($frameworkDir in $frameworkDirs)
        {
          $prjNames = $build.PrjNames
    
          foreach($prj in $prjNames)
          {
            robocopy "$workingSourceDir\$prj\bin\Release\$finalDir" $workingDir\NuGet\lib\$frameworkDir *.dll *.pdb *.xml /NFL /NDL /NJS /NC /NS /NP /XO /XF *.CodeAnalysisLog.xml | Out-Default
          }
        }
      }
    }
  
# TODO: Use Test Names from build parameter for /XD parameter  
    robocopy $workingSourceDir $workingDir\NuGet\src *.cs /S /NFL /NDL /NJS /NC /NS /NP /XD Fhir.Core.Tests Fhir.Specification.Tests obj .vs artifacts | Out-Default

    Write-Host "Building NuGet package with ID $packageId and version $nugetVersion" -ForegroundColor Green
    Write-Host

    exec { & "$toolsDir\NuGet\NuGet.exe" pack $nuspecPath -Symbols }
    move -Path .\*.nupkg -Destination $workingDir\NuGet
  }

 
# TODO: Include a Readme and a License  
#  Copy-Item -Path $docDir\readme.txt -Destination $workingDir\Package\
#  Copy-Item -Path $docDir\license.txt -Destination $workingDir\Package\

  robocopy $workingSourceDir $workingDir\Package\Source\Src /MIR /NFL /NDL /NJS /NC /NS /NP /XD bin obj TestResults AppPackages .vs artifacts /XF *.suo *.user *.lock.json | Out-Default
  robocopy $buildDir $workingDir\Package\Source\Build /MIR /NFL /NDL /NJS /NC /NS /NP /XF runbuild.txt | Out-Default
  robocopy $docDir $workingDir\Package\Source\Doc /MIR /NFL /NDL /NJS /NC /NS /NP | Out-Default
  robocopy $toolsDir $workingDir\Package\Source\Tools /MIR /NFL /NDL /NJS /NC /NS /NP | Out-Default
 
  $destinationZip = "$workingDir\$zipFileName"
  $sourceDir = "$workingDir\Package"
  $includeBaseDirectory = $false
  $compressionLevel= [System.IO.Compression.CompressionLevel]::Optimal 
  If(Test-path $destinationZip) {Remove-item $destinationZip}
  Add-Type -assembly "system.io.compression.filesystem"
  exec { [io.compression.zipfile]::CreateFromDirectory($sourceDir, $destinationZip, $compressionLevel, $includeBaseDirectory) } "Error zipping"
}

# Unzip package to a location
task Deploy -depends Package {
  exec { .\Tools\7-zip\7za.exe x -y "-o$workingDir\Deployed" $workingDir\$zipFileName | Out-Default } "Error unzipping"
}

# Run tests on deployed files
task Test -depends Deploy {

  Update-Project $workingSourceDir\Newtonsoft.Json\project.json $false

  foreach ($build in $builds)
  {
    if ($build.TestsFunction -ne $null)
    {
      & $build.TestsFunction $build
    }
  }
}

function MSBuildBuild($build)
{
  $slnName = $build.SlnName
  $finalDir = $build.FinalDir

  Write-Host
  Write-Host "Restoring $workingSourceDir\$slnName.sln" -ForegroundColor Green
  [Environment]::SetEnvironmentVariable("EnableNuGetPackageRestore", "true", "Process")
  exec { & "$toolsDir\NuGet\NuGet.exe" update -self }
  exec { & "$toolsDir\NuGet\NuGet.exe" restore "$workingSourceDir\$slnName.sln" -verbosity detailed -configfile $workingSourceDir\nuget.config | Out-Default } "Error restoring $slnName"

  $constants = GetConstants $build.Constants $signAssemblies

  Write-Host
  Write-Host "Building $workingSourceDir\$slnName.sln" -ForegroundColor Green
  exec { msbuild "/t:Clean;Rebuild" /p:Configuration=Release "/p:CopyNuGetImplementations=true" "/p:Platform=Any CPU" "/p:PlatformTarget=AnyCPU" /p:OutputPath=bin\Release\$finalDir\ /p:AssemblyOriginatorKeyFile=$signKeyPath "/p:SignAssembly=$signAssemblies" "/p:TreatWarningsAsErrors=$treatWarningsAsErrors" "/p:VisualStudioVersion=14.0" /p:DefineConstants=`"$constants`" "$workingSourceDir\$slnName.sln" | Out-Default } "Error building $slnName"
}



function GetConstants($constants, $includeSigned)
{
  $signed = switch($includeSigned) { $true { ";SIGNED" } default { "" } }

  return "CODE_ANALYSIS;TRACE;$constants$signed"
}

function GetVersion($majorVersion)
{
    $now = [DateTime]::Now
    
    $year = $now.Year - 2000
    $month = $now.Month
    $totalMonthsSince2000 = ($year * 12) + $month
    $day = $now.Day
    $minor = "{0}{1:00}" -f $totalMonthsSince2000, $day
    
    $hour = $now.Hour
    $minute = $now.Minute
    $revision = "{0:00}{1:00}" -f $hour, $minute
    
    return $majorVersion + "." + $minor
}

function Update-AssemblyInfoFiles ([string] $workingSourceDir, [string] $assemblyVersionNumber, [string] $fileVersionNumber)
{
    $assemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $fileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $assemblyVersion = 'AssemblyVersion("' + $assemblyVersionNumber + '")';
    $fileVersion = 'AssemblyFileVersion("' + $fileVersionNumber + '")';
    
    Get-ChildItem -Path $workingSourceDir -r -filter AssemblyInfo.cs | ForEach-Object {
        
        $filename = $_.Directory.ToString() + '\' + $_.Name
        Write-Host $filename
        $filename + ' -> ' + $version
    
        (Get-Content $filename) | ForEach-Object {
            % {$_ -replace $assemblyVersionPattern, $assemblyVersion } |
            % {$_ -replace $fileVersionPattern, $fileVersion }
        } | Set-Content $filename
    }
}

function Edit-XmlNodes {
    param (
        [xml] $doc,
        [string] $xpath = $(throw "xpath is a required parameter"),
        [string] $value = $(throw "value is a required parameter")
    )
    
    $nodes = $doc.SelectNodes($xpath)
    $count = $nodes.Count

    Write-Host "Found $count nodes with path '$xpath'"
    
    foreach ($node in $nodes) {
        if ($node -ne $null) {
            if ($node.NodeType -eq "Element")
            {
                $node.InnerXml = $value
            }
            else
            {
                $node.Value = $value
            }
        }
    }
}


function Execute-Command($command) {
    $currentRetry = 0
    $success = $false
    do {
        try
        {
            & $command
            $success = $true
        }
        catch [System.Exception]
        {
            if ($currentRetry -gt 5) {
                throw $_.Exception.ToString()
            } else {
                write-host "Retry $currentRetry"
                Start-Sleep -s 1
            }
            $currentRetry = $currentRetry + 1
        }
    } while (!$success)
}
