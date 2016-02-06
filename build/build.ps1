
properties {
  $productName = "Hl7.Fhir .Net Library" 
  $productVersion = "0.90.6"             # Update this for a new release
  $nugetPrelease = $null                 # Set this to something like "alpha", if desired

  $localNugetPath = "\\karoo\Develop\Running\Furore\NUGET"    # Optional: Set this to a path where your local NuGet server resides (this is used by the "Redeploy" task)

# ATTENTION: The Assembly Version scheme is Major.Minor.BUILD.Revision. 
# Do NOT use humble Open Source numbering, e.g. "0.90.6", as this would put the 6 into the BUILD number, not into the Minor number.
# Bump up Major, if the new library is not backward compatible to the old one. Bump up Minor, if it is FULLY backward compatible, but enhanced.
# See the following for some explanations: http://blogs.msdn.com/b/jjameson/archive/2009/04/03/best-practices-for-net-assembly-versioning.aspx 
  $assemblyVersion = "1.0"               # Update this according to the assembly version scheme, with Major.Minor. DO NOT INCLUDE BUILD OR REVISION here! 

  $nugetPkgs = @(                        # Update this for new DSTU version
    @{CsProj="Hl7.Fhir.Core"; AssemblyPattern="Hl7.Fhir.*.Core"; PkgId="Hl7.Fhir.DSTU21"},
    @{CsProj="Hl7.Fhir.Specification"; AssemblyPattern="Hl7.Fhir.*.Specification"; PkgId="Hl7.Fhir.Specification.DSTU21"}
   )

  $zipFileName = "FhirNetApi.zip"


  $baseDir  = resolve-path ..
  $sourceDir = "$baseDir\src"

  $buildNuGet = $true
  $nugetVersion = $productVersion
  if ($nugetPrelease -ne $null)
  {
    $nugetVersion = $nugetVersion + "-" + $nugetPrelease
  }

  $assemblyFileVersion = GetAssemblyFileVersion $assemblyVersion
  $assemblyVersion = $assemblyVersion + ".0.0"
  $assemblyInfoVersion = $productName + " " + $productVersion

# TODO: Support Debug builds as well?
  $signAssemblies = $false
  $signKeyPath = "$sourceDir\FhirNetApi.snk"   # TODO: Clarify everything around usage of the key. Secret: Yes/No? Dedicated key for unit tests? How to get matching PublicToken into AssemblyInfo?
  

  $dirPairs = @(                               # Update this when new target frameworks are added
    @{BinDir="Net40"; LibDir="net40"},
    @{BinDir="Net45"; LibDir="net45"},
    @{BinDir="Portable45"; LibDir="portable-net45+netcore45+wpa81+wp8"}
  )

  $treatWarningsAsErrors = $false
  
  $buildDir = "$baseDir\build"
  $toolsDir = "$baseDir\tools"
  $docDir = "$baseDir\doc"
  $releaseDir = "$baseDir\release"
  $workingDir = "$baseDir\working"
  $workingSourceDir = "$workingDir\src"
  $packageDirs = "$sourceDir\packages"         # Comment this out if you need to build while offline

  $builds = @(                           # Update this to add new target frameworks 
    @{SlnName = "Hl7.Fhir.MultiTarget"; Configuration="ReleaseNet45"; PrjNames = "Hl7.Fhir.Core","Hl7.Fhir.Specification"; TestNames = "Hl7.Fhir.Core.Tests","Hl7.Fhir.Specification.Tests"; BuildFunction = "MSBuildBuild"; TestsFunction = "VSTests"; Constants="NET45"; FinalDir="Net45"},
    @{SlnName = "Hl7.Fhir.MultiTarget"; Configuration="ReleaseNet40"; PrjNames = "Hl7.Fhir.Core.Net40","Hl7.Fhir.Specification.Net40"; TestNames = @(); BuildFunction = "MSBuildBuild"; TestsFunction = "VSTests"; Constants="NET40"; FinalDir="Net40"},
    @{SlnName = "Hl7.Fhir.MultiTarget"; Configuration="ReleasePCL45"; PrjNames = "Hl7.Fhir.Core.Portable45"; TestNames = "Hl7.Fhir.Core.Tests"; BuildFunction = "MSBuildBuild"; TestsFunction = "VSTests"; Constants="PORTABLE45"; FinalDir="Portable45"}
  )

  $Script:MSBuild = "MSBuild"
  $Script:VSTest = "VSTest.Console"
}


framework '4.6x86'                       # Set this to different version if required (e.g. '4.5.2x86')


TaskSetup {

    if (-not (Get-Command $MSBuild -ea SilentlyContinue))
    {
        Write-Verbose "Looking for location of MSBuild..."
        $Keys = 'HKLM:\SOFTWARE\Wow6432Node\Microsoft\MSBuild', 'HKLM:\SOFTWARE\Microsoft\MSBuild'
        $MSBuild = $Keys | Get-ChildItem -ea SilentlyContinue | Where-Object { $_.PSChildName -match '\d+\.\d+' } `
                         | Get-ItemProperty -Name MSBuildOverrideTasksPath -ea SilentlyContinue `
                         | Where-Object { Test-Path (Join-Path $_.MSBuildOverrideTasksPath 'MSBuild.exe') } `
                         | Select-Object MSBuildOverrideTasksPath -First 1 `
                         | ForEach-Object { Join-Path $_.MSBuildOverrideTasksPath 'MSBuild.exe' }

        if ($MSBuild.Length)
        {
            $Script:MSBuild = $MSBuild
            Write-Host "Found MSBuild at '$MSBuild'." -ForegroundColor Green
        }
        else
        {
            Write-Warning "MSBuild could not be found. Will simply invoke 'MSBuild'."
       }
    }

    if (-not (Get-Command $VSTest -ea SilentlyContinue))
    {
        Write-Verbose "Looking for location of MSTest..."
        $Keys = 'HKLM:\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\12.0\Setup\VS', 'HKLM:\SOFTWARE\Microsoft\VisualStudio\12.0\Setup\VS',
                'HKLM:\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\11.0\Setup\VS', 'HKLM:\SOFTWARE\Microsoft\VisualStudio\11.0\Setup\VS',
                'HKLM:\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\14.0\Setup\VS', 'HKLM:\SOFTWARE\Microsoft\VisualStudio\14.0\Setup\VS'
        $VSTest = $Keys | Get-Item -ea SilentlyContinue | Get-ItemProperty -Name ProductDir -ea SilentlyContinue `
                        | Where-Object { Test-Path (Join-Path $_.ProductDir 'Common7\IDE\CommonExtensions\Microsoft\TestWindow\VSTest.Console.exe') } `
                        | Select-Object ProductDir -First 1 `
                        | ForEach-Object { Join-Path $_.ProductDir 'Common7\IDE\CommonExtensions\Microsoft\TestWindow\VSTest.Console.exe' }

        if ($VSTest.Length)
        {
            $Script:VSTest = $VSTest
            Write-Host "Found VSTest at '$VSTest'." -ForegroundColor Green
        }
        else
        {
            Write-Warning "VSTest could not be found. Will simply invoke 'VSTest.Console'."
        }
    }
}


task default -depends Package

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
  Update-AssemblyInfoFiles $workingSourceDir ($assemblyVersion) $assemblyFileVersion ($assemblyInfoVersion)


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

# TODO: Should Packaging depend on successful testing?
# Optional build NuGet, add files to final zip
task Package -depends Build {
  foreach ($build in $builds)
  {
# TODO: Can projects and final directories be pulled out of the list of builds?
    $prjNames = $build.PrjNames
    $finalDir = $build.FinalDir

    foreach($prj in $prjNames)
    {
        robocopy "$workingSourceDir\$prj\bin\Release\$finalDir" $workingDir\Package\Bin\$finalDir *.dll *.pdb *.xml /NFL /NDL /NJS /NC /NS /NP /XO /XF *.CodeAnalysisLog.xml | Out-Default
    }
  }
  
  # Build NuGet packages
  if ($buildNuGet)
  {
    $nugetDir = "$workingDir\NuGet"

    foreach($nugetPkg in $nugetPkgs)
    {
      $pkg = $nugetPkg.CsProj
      $packageId = $nugetPkg.PkgId

      # Start out each package with a fresh directory
      if (Test-Path -path $nugetDir)
      {
        Write-Host "Deleting existing working directory $nugetDir"
    
        Execute-Command -command { del $nugetDir -Recurse -Force }
      }
  
      Write-Host "Creating working directory $nugetDir"
      New-Item -Path $nugetDir -ItemType Directory

      $nuspecPath = "$nugetDir\$pkg.nuspec"
      Copy-Item -Path "$buildDir\nuget\$pkg\$pkg.nuspec" -Destination $nuspecPath

      Update-NuspecFile "$nuspecPath" "$packageId" "$nugetVersion"


      New-Item -Path $workingDir\NuGet\tools -ItemType Directory
      if (Test-Path "$buildDir\nuget\$pkg\tools\")
      {
        Copy-Item -Path "$buildDir\nuget\$pkg\tools\*.ps1" -Destination $workingDir\NuGet\tools -recurse
      }

      # Copy e.g. validation data zip file.
      New-Item -Path $workingDir\NuGet\content -ItemType Directory
      Copy-Item -Path "$sourceDir\$pkg\*.xml.zip" -Destination $workingDir\NuGet\content -recurse

      # Copy the Assemblies to the /lib directories
      foreach($dirPair in $dirPairs)
      {
        $binDir = $dirPair.BinDir
        $libDir = $dirPair.LibDir
        $assemblyPattern = $nugetPkg.AssemblyPattern
        robocopy "$workingSourceDir\$pkg\bin\Release\$binDir" "$workingDir\NuGet\lib\$libDir" "$assemblyPattern.dll" "$assemblyPattern.pdb" "$assemblyPattern.xml" /NFL /NDL /NJS /NC /NS /NP /XO /XF *.CodeAnalysisLog.xml | Out-Default
      }

      # Copy source code for symbol package
      robocopy "$workingSourceDir\$pkg" "$workingDir\NuGet\src\$pkg" "*.cs" /S /NFL /NDL /NJS /NC /NS /NP /XD obj .vs artifacts | Out-Default

      Write-Host "Building NuGet package with ID $packageId and version $nugetVersion" -ForegroundColor Green
      Write-Host

      exec { & "$toolsDir\NuGet\NuGet.exe" pack $nuspecPath -Symbols }
      move -Path .\*.nupkg -Destination $workingDir
    }  
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
  Write-Host "Zipping $sourceDir to $destinationZip" -ForegroundColor Green 
  Write-Host
  $includeBaseDirectory = $false
  $compressionLevel= [System.IO.Compression.CompressionLevel]::Optimal 
  If(Test-path $destinationZip) {Remove-item $destinationZip}
  Add-Type -assembly "system.io.compression.filesystem"
  exec { [io.compression.zipfile]::CreateFromDirectory($sourceDir, $destinationZip, $compressionLevel, $includeBaseDirectory) } "Error zipping"
}


# Build the packages and place them onto a local NuGet server
task Deploy -depends Package, Redeploy {
}

# Place an already packaged set of NuGet packages onto a local NuGet server
task Redeploy {
  robocopy "$workingDir\NuGet" $localNugetPath "*.nupkg" /MIR /NFL /NDL /NJS /NC /NS /NP | Out-Default 
}


# Build the project and test it
task Test -depends Build, Retest {
}

# Run tests on already built projects
task Retest {
  foreach ($build in $builds)
  {
    if ($build.TestsFunction -ne $null)
    {
      & $build.TestsFunction $build
    }
  }
}


# Update source files and nuspec files WITHIN the project source (i.e. not just in a temporary working directory)
# Use this to update a project to also build assemblies with proper version number and signature key from Visual Studio
# I.e. set values at top of build-script to new values, then run this task
task UpdateSource {
  Write-Host -ForegroundColor Red "Answering yes will patch the AssemblyVersionInfo.cs files and the .nuspec files in your project."
  Write-Host -ForegroundColor Red "If your project is controlled in a version control system, this will overwrite the source-controlled versions."

  $choice = ""
  while ($choice -notmatch "[y|n]"){
    $choice = read-host "Do you want to continue? (Y/N)"
  }

  if ($choice -eq "n"){
    break  
  }
    
  Write-Host -ForegroundColor Green "Updating assembly info in source code project"
  Write-Host
  Update-AssemblyInfoFiles $sourceDir ($assemblyVersion) $assemblyFileVersion ($assemblyInfoVersion)

  # Patch the .nuspec files
  foreach($nugetPkg in $nugetPkgs)
  {
    $pkg = $nugetPkg.CsProj
    $packageId = $nugetPkg.PkgId

    $nuspecPath = "$buildDir\nuget\$pkg\$pkg.nuspec"

    Update-NuspecFile "$nuspecPath" "$packageId" "$nugetVersion"
  }
}


function MSBuildBuild($build)
{
  $slnName = $build.SlnName
  $finalDir = $build.FinalDir
  $configuration = $build.Configuration

  Write-Host
  Write-Host "Restoring $workingSourceDir\$slnName.sln" -ForegroundColor Green
  [Environment]::SetEnvironmentVariable("EnableNuGetPackageRestore", "true", "Process")
  exec { & "$toolsDir\NuGet\NuGet.exe" update -self }
  exec { & "$toolsDir\NuGet\NuGet.exe" restore "$workingSourceDir\$slnName.sln" -verbosity detailed -configfile $workingSourceDir\nuget.config | Out-Default } "Error restoring $slnName"

  $constants = GetConstants $build.Constants $signAssemblies

  Write-Host
  Write-Host "Building $workingSourceDir\$slnName.sln" -ForegroundColor Green
  exec { & "$MSBuild" "/t:Clean;Rebuild" /p:Configuration=$configuration "/p:CopyNuGetImplementations=true" "/p:Platform=Any CPU" "/p:PlatformTarget=AnyCPU" /p:OutputPath=bin\Release\$finalDir\ /p:AssemblyOriginatorKeyFile=$signKeyPath "/p:SignAssembly=$signAssemblies" "/p:TreatWarningsAsErrors=$treatWarningsAsErrors" "/p:VisualStudioVersion=14.0" /p:DefineConstants=`"$constants`" "$workingSourceDir\$slnName.sln" | Out-Default } "Error building $slnName"
}


function VSTests($build)
{
  $testNames = $build.TestNames
  $finalDir = $build.FinalDir

  foreach($testName in $testNames)
  {
    $resultsDir = "$workingDir\TestResults\$finalDir"
    if (-Not (Test-Path -path $resultsDir))
    {
      New-Item -Path $resultsDir -ItemType Directory
    }

    Push-Location $resultsDir          # VSTest creates Test Results relative to current directory
  
    Write-Host "Testing $testName on $finalDir" -ForegroundColor Green
    Write-Host
    try
    {
       & "$VSTest" $workingSourceDir\$testName\bin\Release\$finalDir\$testName.dll /Logger:Trx /TestCaseFilter:”TestCategory!=IntegrationTest" | Out-Default     # TODO: Find out why Trx logger is often not writing anything to file.
    }
    catch {
      Write-Host -ForegroundColor Red "Tests have failed. Continuing..."
    }

    Pop-Location
  }
}


function GetConstants($constants, $includeSigned)
{
  $signed = switch($includeSigned) { $true { ";SIGNED" } default { "" } }

  return "CODE_ANALYSIS;TRACE;$constants$signed"
}

function GetAssemblyFileVersion($majorMinorVersion)
{
    $now = [DateTime]::Now
    
    $year = $now.Year - 2000
    $month = $now.Month
    $totalMonthsSince2000 = ($year * 12) + $month
    $day = $now.Day
    $build = "{0}{1:00}" -f $totalMonthsSince2000, $day
        
    return $majorMinorVersion + "." + $build + ".0"
}

function Update-AssemblyInfoFiles ([string] $workingSourceDir, [string] $assemblyVersionNumber, [string] $fileVersionNumber, [string] $infoVersion)
{
    # Updates a separate version info file, as described here: http://blogs.msdn.com/b/jjameson/archive/2009/04/03/best-practices-for-net-assembly-versioning.aspx
    $assemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $fileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $infoVersionPattern = 'AssemblyInformationalVersion\("[0-9a-zA-Z\.\- ]+"\)'
    $assemblyVersionStatement = 'AssemblyVersion("' + $assemblyVersionNumber + '")';
    $fileVersionStatement = 'AssemblyFileVersion("' + $fileVersionNumber + '")';
    $infoVersionStatement = 'AssemblyInformationalVersion("' + $infoVersion + '")';
    
    Get-ChildItem -Path $workingSourceDir -r -filter AssemblyVersionInfo.cs | ? { $_.Directory.ToString() -inotmatch "packages"} | ForEach-Object {
        
        $filename = $_.Directory.ToString() + '\' + $_.Name
        Write-Host $filename + ' -> ' + $assemblyVersionNumber

        # Only rewrite when really changed
        $inputFile = (Get-Content $filename)    
        $outputFile = $inputFile | ForEach-Object {
            % {$_ -replace $assemblyVersionPattern, $assemblyVersionStatement } |
            % {$_ -replace $fileVersionPattern, $fileVersionStatement } |
            % {$_ -replace $infoVersionPattern, $infoVersionStatement }
        }
        $deltaFile = Compare-Object ($inputFile) ($outputFile)
        if ($deltaFile.Length -gt 0)
        {
          Write-Information "File contents really changed. Writing back."
          $outputFile | Set-Content $filename
        }
        else
        {
          Write-Verbose "File contents remained identical. Not writing back."
        }
    }
}


function Update-NuspecFile ([string] $nuspecPath, [string] $packageId, [string] $nugetVersion)
{
      Write-Host "Updating nuspec file at $nuspecPath ($packageId) to version $nugetVersion" -ForegroundColor Green
      Write-Host

      $xml = New-Object System.Xml.XmlDocument
      $xml.Load($nuspecPath)
      Edit-XmlNodes -doc $xml -xpath "//*[local-name() = 'id']" -value $packageId
      Edit-XmlNodes -doc $xml -xpath "//*[local-name() = 'version']" -value $nugetVersion

      $depNodes=$xml.SelectNodes("/package/metadata/dependencies/dependency")

      foreach ($depNode in $depNodes)
      {
        $dependencyId = $depNode.id;
        Write-Verbose $dependencyId
        if ($dependencyId -eq "Hl7.Fhir.DSTU21")   # TODO: Do not hard-code this?
        {
          Write-Host "Replacing dependency version for '$dependencyId' with $nugetVersion"          
          $depNode.version = $nugetVersion
        }
      }

      Write-Verbose $xml.OuterXml

# TODO: Avoid rewrite when unchanged    
      $xml.save($nuspecPath)
}


function Edit-XmlNodes {
    param (
        [xml] $doc,
        [string] $xpath = $(throw "xpath is a required parameter"),
        [string] $value = $(throw "value is a required parameter")
    )
    
    $nodes = $doc.SelectNodes($xpath)
    $count = $nodes.Count

    Write-Verbose "Found $count nodes with path '$xpath'"
    
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
