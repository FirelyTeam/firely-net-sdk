Param(
    [Parameter(Mandatory=$true, HelpMessage="Enter the new version for the next development phase.")]
    [string] $newVersion,
    [Parameter(Mandatory=$true, HelpMessage="Enter the new suffix for the next development phase (alpha is default).")]
    [string] $versionSuffix = "alpha"
)

$fhir_releases = @('stu3', 'r4', 'r4B', 'r5')

# the root path of the repository
$repo_root = "$PsScriptRoot\.."
Push-Location $repo_root

function Update-Version([string] $develop_branch, [string] $propFile)
{
    Write-Host "Update-Version in develop branch with version $newVersion-$versionSuffix"

    # Pull the lastest changes on develop_branch
    git fetch origin --recurse-submodules
    git checkout $develop_branch --recurse-submodules
    git merge --ff-only origin/$develop_branch

    # update version number
    & $repo_root\build\UpdateVersion.ps1 -propFile $propFile -newVersion $newVersion -suffix $versionSuffix

    # commit and push changes
    git commit --all --message="Start new development phase: version $newVersion-$versionSuffix"
    git push
}

function Update-Common([string] $fhir_release)
{
    Write-Host "Start release for version $fhir_release"

    # start release for sdk 
    Update-Version -develop_branch "develop-$fhir_release" -propFile $repo_root\src\firely-net-sdk.props

    # Move to submodule common
    Push-Location .\common

    # set common pointer to the latest commit in branch develop
    git checkout develop

    # back to root directory
    Pop-Location

    # commit and push changes
    git commit --all --message="Start new development phase: $newVersion-$versionSuffix"
    git push
}

# Move to submodule common
Push-Location .\common

# start release for common first
Update-Version -develop_branch "develop" -propFile $repo_root\common\src\firely-net-common.props

# back to root directory
Pop-Location

$fhir_releases | ForEach-Object { Update-Common -fhir_release $PSItem}

# back to the directory when this script was started
Pop-Location