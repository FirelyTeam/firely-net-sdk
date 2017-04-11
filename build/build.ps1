properties {
  $productName = "Hl7.Fhir .Net Library" 
  $productVersion = "0.91.3"             # Update this for a new release
 # $nugetPrelease = ""              # Set this to something like "alpha", if desired

  $ignoreTestFailure = $true             # Report build success, even though tests failed. Useful for alpha versions

  $localNugetPath = "c:\temp\fhir-net-api-NUGET"    # Optional: Set this to a path where your local NuGet server resides (this is used by the "Redeploy" task)

  $ProgressColor = "Magenta"

  $appVeyor = $false
  if(Test-Path -Path env:\APPVEYOR) 
  {
    $appVeyor = $true
    Write-Host -ForegroundColor $ProgressColor "Running on Appveyor"
  }

# ATTENTION: The Assembly Version scheme is Major.Minor.BUILD.Revision. 
# Do NOT use humble Open Source numbering, e.g. "0.90.6", as this would put the 6 into the BUILD number, not into the Minor number.
# Bump up Major, if the new library is not backward compatible to the old one. Bump up Minor, if it is FULLY backward compatible, but enhanced.
# See the following for some explanations: http://blogs.msdn.com/b/jjameson/archive/2009/04/03/best-practices-for-net-assembly-versioning.aspx 
#  $assemblyVersion = "1.0"               # Update this according to the assembly version scheme, with Major.Minor. DO NOT INCLUDE BUILD OR REVISION here! 

  $assemblyVersion = "0.91.3"    # This is a violation of the assembly version scheme, but the Fhir .Net library wants to go with this for all 0.x releases.

  $nugetPkgs = @(                        # Update this for new DSTU version
    @{CsProj="Hl7.Fhir.Core"; AssemblyPattern="Hl7.Fhir.*.Core"; PkgId="Hl7.Fhir.DSTU2"},
    @{CsProj="Hl7.Fhir.Specification"; AssemblyPattern="Hl7.Fhir.*.Specification"; PkgId="Hl7.Fhir.Specification.DSTU2"}
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

# The following two have been commented out to enable Fhir .Net 0.x release assembly version scheme.
#  $assemblyFileVersion = GetAssemblyFileVersion $assemblyVersion
#  $assemblyVersion = $assemblyVersion + ".0.0"

  $assemblyFileVersion = $assemblyVersion

  $assemblyInfoVersion = $productName + " " + $nugetVersion

  $signAssemblies = $true
  $signKeyPath = "$sourceDir\FhirNetApi.snk"   # This key has to be placed by the build server, e.g. as described here: https://www.appveyor.com/docs/how-to/secure-files
  $ignoreSignatureFailure = $true              # Should an inability to strong-name the library be tolerated (e.g. due to missing SNK file)?

  $signatureFailed = $false  

  $dirPairs = @(                               # Update this when new target frameworks are added
    @{BinDir="Net40"; LibDir="net40"},
    @{BinDir="Net45"; LibDir="net45"},
    @{BinDir="Portable45"; LibDir="portable-net45+win+wpa81+wp80"},
    @{BinDir="Portable45"; LibDir="netcore45"},
    @{BinDir="Portable45"; LibDir="wp8"},
    @{BinDir="Portable45"; LibDir="wpa81"},
    @{BinDir="Portable45"; LibDir="win81"},
    @{BinDir="Portable45"; LibDir="portable-win81+wpa81"},
    @{BinDir="Portable45"; LibDir="MonoAndroid"},
    @{BinDir="Portable45"; LibDir="Xamarin.iOS10"},
    @{BinDir="Portable45"; LibDir="Xamarin.Mac20"},
    @{BinDir="Portable45"; LibDir="dotnet"}    # This is using the PCL Profile 259 library as the dotnet library
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
    @{SlnName = "Hl7.Fhir"; Configuration="ReleaseNet45"; PrjNames = "Hl7.Fhir.Core.Net45","Hl7.Fhir.Specification.Net45"; TestNames = "Hl7.Fhir.Core.Tests","Hl7.Fhir.Specification.Tests"; BuildFunction = "MSBuildBuild"; TestsFunction = "VSTests"; Constants="NET45"; FinalDir="Net45"},
    @{SlnName = "Hl7.Fhir"; Configuration="ReleaseNet40"; PrjNames = "Hl7.Fhir.Core.Net40","Hl7.Fhir.Specification.Net40"; TestNames = @(); BuildFunction = "MSBuildBuild"; TestsFunction = "VSTests"; Constants="NET40"; FinalDir="Net40"},
    @{SlnName = "Hl7.Fhir"; Configuration="ReleasePCL45"; PrjNames = "Hl7.Fhir.Core.Portable45"; TestNames = "Hl7.Fhir.Core.Tests"; BuildFunction = "MSBuildBuild"; TestsFunction = "VSTests"; Constants="PORTABLE45"; FinalDir="Portable45"}
  )

  $Script:MSBuild = "MSBuild"
  $Script:VSTest = "VSTest.Console"

  $testCaseFilter = "TestCategory!=IntegrationTest&TestCategory!=LongRunner"
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
            Write-Host "Found MSBuild at '$MSBuild'."
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
            Write-Host "Found VSTest at '$VSTest'."
        }
        else
        {
            Write-Warning "VSTest could not be found. Will simply invoke 'VSTest.Console'."
        }
    }
}


task default -depends Help

task Help -description "Show help text" {
  WriteDocumentation($false)
}


# Ensure a clean working directory
task Clean -description "Clean all output and temporary files from previous builds" {
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
task Build -depends Clean -description "Build all targets. Output to various bin/ directories." { 

  Copy-Robot -sourcePath "$sourceDir" -destPath "$workingSourceDir" -excludeDirectories "bin","obj","TestResults","AppPackages","$packageDirs",".vs","artifacts" -excludeFiles "*.suo","*.user","project.json","*.lock.json" -item "source"

  Write-Host -ForegroundColor $ProgressColor "Updating assembly version"
  Write-Host
  Update-AssemblyInfoFiles $workingSourceDir ($assemblyVersion) $assemblyFileVersion ($assemblyInfoVersion)


# Check all pre-conditions for strong names
  if ($signAssemblies)
  {
    if (Test-Path -Path $signKeyPath)
    {
        Write-Host -ForegroundColor Green "Strong Name with SNK keyfile: $signKeyPath"
    }
    else
    {
        Write-Warning "SNK keyfile not found: $signKeyPath - Building Release build without a strong name"
        $signAssemblies = $false
        $signatureFailed = $true
    }
  }
  else
  {
    Write-Warning "Building Release build without a strong name"
  }


  foreach ($build in $builds)
  {
    $slnName = $build.SlnName
    $configName = $build.Configuration
    if ($slnName -ne $null)
    {
      Write-Host -ForegroundColor $ProgressColor "Building: $configName from $slnName"
 

      & $build.BuildFunction $build
    }
  }

  if ($signatureFailed -and !$ignoreSignatureFailure)
  {
    throw "Could not sign assembly and ignoreSignatureFailure is not set => Reporting build failure."
  }
}

# TODO: Should Packaging depend on successful testing?
# Optional build NuGet, add files to final zip
task Package -depends Build -description "Build, then package into NuGet packages and a Zip file." {
  foreach ($build in $builds)
  {
# TODO: Can projects and final directories be pulled out of the list of builds?
    $prjNames = $build.PrjNames
    $finalDir = $build.FinalDir

    foreach($prj in $prjNames)
    {
      Copy-Robot -sourcePath "$workingSourceDir\$prj\bin\Release\$finalDir" -destPath "$workingDir\Package\Bin\$finalDir" -includeFiles "*.dll","*.pdb","*.xml" -excludeFiles "*.CodeAnalysisLog.xml" -item "compiled binaries"
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
      Copy-Item -Path "$sourceDir\$pkg\specification*.zip" -Destination $workingDir\NuGet\content -recurse

      # Copy the Assemblies to the /lib directories
      foreach($dirPair in $dirPairs)
      {
        $binDir = $dirPair.BinDir
        $libDir = $dirPair.LibDir
        $assemblyPattern = $nugetPkg.AssemblyPattern
        Copy-Robot -sourcePath "$workingSourceDir\$pkg\bin\Release\$binDir" -destPath "$workingDir\NuGet\lib\$libDir" -includeFiles "$assemblyPattern.dll","$assemblyPattern.pdb","$assemblyPattern.xml" -excludeFiles "*.CodeAnalysisLog.xml" -params "/XO" -item "assemblies for NuGet package"
      }

      # Copy source code for symbol package
      Copy-Robot -sourcePath "$workingSourceDir\$pkg" -destPath "$workingDir\NuGet\src\$pkg" -includeFiles "*.cs" -params "/S" -excludeDirectories "obj",".vs","artifacts" -item "source code for symbol package"

      Write-Host "Building NuGet package with ID $packageId and version $nugetVersion" -ForegroundColor $ProgressColor
      Write-Host

      exec { & "$toolsDir\NuGet\NuGet.exe" pack $nuspecPath -Symbols }
      move -Path .\*.nupkg -Destination $workingDir
    }  
  }

 
# TODO: Include a Readme and a License  
#  Copy-Item -Path $docDir\readme.txt -Destination $workingDir\Package\
#  Copy-Item -Path $docDir\license.txt -Destination $workingDir\Package\

  Copy-Robot -sourcePath "$workingSourceDir" -destPath "$workingDir\Package\Source\Src" -excludeDirectories "bin","obj","TestResults","AppPackages",".vs","artifacts" -excludeFiles "*.suo","*.user","*.lock.json"
  Copy-Robot -sourcePath "$buildDir" -destPath "$workingDir\Package\Source\Build" -excludeFiles "runbuild.txt" 
  Copy-Robot -sourcePath "$docDir" -destPath "$workingDir\Package\Source\Doc"
  Copy-Robot -sourcePath "$toolsDir" -destPath "$workingDir\Package\Source\Tools" 

  $destinationZip = "$workingDir\$zipFileName"
  $sourceDir = "$workingDir\Package"
  Write-Host "Zipping $sourceDir to $destinationZip" -ForegroundColor $ProgressColor 
  Write-Host
  $includeBaseDirectory = $false
  $compressionLevel= [System.IO.Compression.CompressionLevel]::Optimal 
  If(Test-path $destinationZip) {Remove-item $destinationZip}
  Add-Type -assembly "system.io.compression.filesystem"
  exec { [io.compression.zipfile]::CreateFromDirectory($sourceDir, $destinationZip, $compressionLevel, $includeBaseDirectory) } "Error zipping"
}


# Build the packages and place them onto a local NuGet server
task Deploy -depends Package, Redeploy -description "Build, package, then copy NuGet packages to a deployment directory." {
}

# Place an already packaged set of NuGet packages onto a local NuGet server
task Redeploy -description "Copy NuGet packages from a previous packaging run and copy them to a deployment directory." {
  $destinationZip = "$workingDir\$zipFileName"

  if ($appveyor)
  {
    Write-Warning "Executing on AppVeyor => Do nothing. Control deployment through AppVeyor mechanisms."
  }
  else
  {
    Copy-Robot -sourcePath "$workingDir\NuGet" -destPath "$localNugetPath" -includeFiles "*.nupkg"
  }
}


# Build the project and test it
task Test -depends Build, Retest -description "Build and test everything." {
}

# Run tests on already built projects
task Retest -description "Retest a previously executed build." {
  $testFailCount = 0

  foreach ($build in $builds)
  {
    if ($build.TestsFunction -ne $null)
    {
      $testFailure = VSTests($build)
      if ($testFailure) { $testFailCount += 1}
    }
  }

  if ($testFailCount -gt 0)
  {
    if ($ignoreTestFailure)
    {
      Write-Warning "$testFailCount sets of tests have failed. ignoreTestFailure is set to true. => Continuing."
    }
    else
    {
      throw "$testFailCount sets of tests have failed"
    }
  }
}


# Update assembly version files and nuspec files WITHIN the project source (i.e. not just in a temporary working directory)
# Use this to update a project to also build assemblies with proper version number and signature key from Visual Studio
# I.e. set values at top of build-script to new values, then run this task
task UpdateSource -description "Update assembly version files and nuspec files WITHIN the project source (i.e. not just in a temporary working directory)" {
  Write-Warning "Answering yes will patch the AssemblyVersionInfo.cs files and the .nuspec files in your project."
  Write-Warning "If your project is controlled in a version control system, this will overwrite the source-controlled versions."

  $choice = ""
  while ($choice -notmatch "[y|n]"){
    $choice = read-host "Do you want to continue? (Y/N)"
  }

  if ($choice -eq "n"){
    break  
  }
    
  Write-Host -ForegroundColor $ProgressColor "Updating assembly info in source code project"
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


task SNKforAppVeyor -description "Guide through the encryption of an SNK file for usage with Appveyor secure file facility" {
  $encSignKeyPath = $signKeyPath + ".enc"
  if (Test-Path -Path $signKeyPath)
  {
     # SNK file already exists
     if (Test-Path -Path $encSignKeyPath)
     {
       Write-Host "SNK file and encrypted SNK file found."
     }
     else
     {
       Write-Host "SNK file found, but no encrypted SNK file."

       New-Item -Path $workingDir -ItemType Directory -Force | Out-Null

       Push-Location $workingDir
       exec { & "$toolsDir\NuGet\NuGet.exe" install secure-file -ExcludeVersion }
       Pop-Location

       Write-Warning "The passphrase should only be known to you and will later be required again."
       $passPhrase = read-host "Enter a passphrase to protect your SNK file"

       exec { & "$workingDir\secure-file\tools\secure-file.exe" -encrypt "$signKeyPath" -secret $passPhrase }
     }
  }
  else
  {
    Write-Host "No SNK file called $signKeyPath found."
    Write-Host -ForegroundColor Yellow "Manual instructions"
    Write-Host "Launch 'Developer Command Prompt for VS201x'"
    Write-Host "Enter the following commands:"
    Write-Host "sn -k $signKeyPath"
    Write-Host
    Write-Host "Then repeat running SNKforAppVeyor"
    break
  }

  Write-Host -ForegroundColor Green "Results"
  Write-Host "Strong Name Keyfile (do NOT check into source control): $signKeyPath" 
  Write-Host "Encrypted Keyfile (should be included in source control): $encSignKeyPath"
  Write-Host
  Write-Host -ForegroundColor Yellow "In AppVeyor: Settings->Environment->Environment Variables add an Encrypted Variable with name 'snk_passphrase' and your passphrase as the encrypted value."
}


function MSBuildBuild($build)
{
  $slnName = $build.SlnName
  $finalDir = $build.FinalDir
  $configuration = $build.Configuration

  Write-Host
  Write-Host "Restoring $workingSourceDir\$slnName.sln"
  [Environment]::SetEnvironmentVariable("EnableNuGetPackageRestore", "true", "Process")
  exec { & "$toolsDir\NuGet\NuGet.exe" restore "$workingSourceDir\$slnName.sln" -configfile $workingSourceDir\nuget.config | Out-Default } "Error restoring $slnName"

  $constants = GetConstants $build.Constants $signAssemblies

  Write-Host
  Write-Host "Building configuration $configuration from $workingSourceDir\$slnName.sln" -ForegroundColor $ProgressColor
  exec { & "$MSBuild" "/verbosity:minimal" "/t:Clean;Rebuild" /p:Configuration=$configuration "/p:CopyNuGetImplementations=true" "/p:Platform=Any CPU" "/p:PlatformTarget=AnyCPU" /p:OutputPath=bin\Release\$finalDir\ "/p:TreatWarningsAsErrors=$treatWarningsAsErrors" "/p:VisualStudioVersion=14.0" /p:DefineConstants=`"$constants`" "$workingSourceDir\$slnName.sln" | Out-Default } "Error building $slnName"
}


function VSTests($build)
{
  $testNames = $build.TestNames
  $finalDir = $build.FinalDir

  $testFailure = $false

  foreach($testName in $testNames)
  {
    $resultsDir = "$workingDir\TestResults\$finalDir"
    if (-Not (Test-Path -path $resultsDir))
    {
      New-Item -Path $resultsDir -ItemType Directory
    }

    Push-Location $resultsDir          # VSTest creates Test Results relative to current directory
  
    Write-Host "Testing $testName on $finalDir" -ForegroundColor $ProgressColor
    Write-Host
    try
    {
       if ($appVeyor)
       {
         & "$VSTest" $workingSourceDir\$testName\bin\Release\$finalDir\$testName.dll /Logger:Appveyor /TestCaseFilter:$testCaseFilter" | Out-Default  # TODO: Include LongRunners again.
       }
       else
       {
         & "$VSTest" $workingSourceDir\$testName\bin\Release\$finalDir\$testName.dll /Logger:Trx /TestCaseFilter:$testCaseFilter" | Out-Default     # TODO: Find out why Trx logger is often not writing anything to file.
       }
    }
    catch {
      Write-Warning $_
      Write-Host -ForegroundColor Red "Tests of $testName have failed. Moving on..."
      $testFailure = $true
    }

    Pop-Location
  }

  return $testFailure
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
    $publicKeyPattern = 'PublicKey=[0-9a-zA-Z]+'
    $assemblyVersionStatement = 'AssemblyVersion("' + $assemblyVersionNumber + '")'
    $fileVersionStatement = 'AssemblyFileVersion("' + $fileVersionNumber + '")'
    $infoVersionStatement = 'AssemblyInformationalVersion("' + $infoVersion + '")'
    $publicKey = PublicKeyFromSNK($signKeyPath)
    $publicKeyStatement = 'PublicKey=' + $publicKey

    Get-ChildItem -Path $workingSourceDir -r -filter AssemblyVersionInfo.cs | ? { $_.Directory.ToString() -inotmatch "packages"} | ForEach-Object {
        
        $filename = $_.Directory.ToString() + '\' + $_.Name
        Write-Host $filename + ' -> ' + $assemblyVersionNumber

        # Only rewrite when really changed
        $inputFile = (Get-Content $filename)    
        $outputFile = $inputFile | ForEach-Object {
            % {$_ -replace $assemblyVersionPattern, $assemblyVersionStatement } |
            % {$_ -replace $fileVersionPattern, $fileVersionStatement } |
            % {$_ -replace $infoVersionPattern, $infoVersionStatement } |
            % {$_ -replace $publicKeyPattern, $publicKeyStatement }
        }
        $deltaFile = Compare-Object ($inputFile) ($outputFile)
        if ($deltaFile.Length -gt 0)
        {
          Write-Host "File contents really changed. Writing back."
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
      Write-Host "Updating nuspec file at $nuspecPath ($packageId) to version $nugetVersion" -ForegroundColor $ProgressColor
      Write-Host

      $xml = New-Object System.Xml.XmlDocument
      $xml.Load($nuspecPath)
      Edit-XmlNodes -doc $xml -xpath "//*[local-name() = 'id']" -value $packageId
      Edit-XmlNodes -doc $xml -xpath "//*[local-name() = 'version']" -value $nugetVersion

      $depNodes=$xml.SelectNodes("/package/metadata/dependencies/group/dependency")

      foreach ($depNode in $depNodes)
      {
        $dependencyId = $depNode.id;
        Write-Verbose $dependencyId
        if ($dependencyId -eq "Hl7.Fhir.DSTU2")   # TODO: Do not hard-code this?
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

# Execute robocopy with parameters as required, proper handling of output, proper handling of error codes.
function Copy-Robot {
  param(
      [string] $sourcePath = $(throw "sourcePath is a required parameter"),
      [string] $destPath = $(throw "destPath is a required parameter"),
      [string[]] $includeFiles = @(),
      [string[]] $excludeDirectories = $null,
      [string[]] $excludeFiles = $null,
      [string[]] $params = @(),
      [string] $item = ""
  )

  Write-Host "Copying $item from $sourcePath -> $destPath"

  $defaultParams = @("/MIR","/NP","/NFL","/NDL","/NJS","/NC","/NS")  # Mirror and be quiet :-)

  $xdParams = @()

  if ($excludeDirectories -ne $null)
  {
    $xdParams = @("/XD")
    $xdParams += $excludeDirectories
  }

  $xfParams = @()

  if ($excludeFiles -ne $null)
  {
    $xfParams = @("/XF")
    $xfParams += $excludeFiles
  }

  & "robocopy" $robocommand $sourcePath $destPath $includeFiles $defaultParams $params $xdParams $xfParams | Out-Null

  if (($LASTEXITCODE -ge 0) -and ($LASTEXITCODE -le 3))
  {
    Write-Verbose "Robocopy success: $LASTEXITCODE"               
  }
  elseif (($LASTEXITCODE -gt 0) -and ($LASTEXITCODE -lt 16))
  {
    Write-Warning "Robocopy exit code: $LASTEXITCODE"
  }
  elseif ($LASTEXITCODE -eq 16)
  {
    Write-Host "Robocopy did not copy anything."
  }
  else
  {
    Write-Warning "Robocopy unknown exit code: $LASTEXITCODE"
  }
}


function PublicKeyFromSNK {
  param(
    [string] $keyPath = $(throw "keyPath is a required parameter")
  )
  if (Test-Path $keyPath)
  {
    $snkStream = New-Object IO.FileStream "$keyPath", 'Open'
    $snkPair = New-Object Reflection.StrongNameKeyPair $snkStream
    $publicKey = [BitConverter]::ToString($snkPair.PublicKey) -replace "-",""
    $publicKey   # Place this on the return pipeline
  }
  else
  {
    "001234567800"  # Place dummy value on stack
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
