Param(
    [Parameter(Mandatory=$true, HelpMessage="Enter the version for the new release.")]
    [string] $version,
    [ValidateSet('release', 'hotfix')]
    [Parameter(Mandatory=$true, HelpMessage="Enter the type of release (release|hotfix).")] [string] $release_type
)

$fhir_releases = @('stu3', 'r4', 'r4B', 'r5')

# the root path of the repository
$repo_root = "$PsScriptRoot\.."
Push-Location $repo_root

# release is based on develop, hotfix is based on master
if ($release_type -eq "release")
{
   $from_branch = "develop"
}
else
{
	$from_branch = "master"
}

function Update-Version([string] $start_branch, [string] $release_branch, [string] $propFile)
{
    Write-Host "Update-Version in release branch $release_branch"

    # Pull the lastest changes on start_branch
    git fetch origin --recurse-submodules
    git checkout $start_branch --recurse-submodules
    git merge --ff-only origin/$start_branch

    # create a release branch
    git checkout -b $release_branch $start_branch

    # update version number
    & $repo_root\build\UpdateVersion.ps1 -propFile $propFile -newVersion $version

    # commit changes
    git commit --all --message="bumped version to $version"
}

function Update-Common([string] $fhir_release)
{
    Write-Host "Start release for version $fhir_release"

    # start release for sdk 
    Update-Version -start_branch "$from_branch-$fhir_release" -release_branch "$release_type/$version-$fhir_release" -propFile $repo_root\src\firely-net-sdk.props

    # Move to submodule common
    Push-Location .\common

    # set common pointer to the new version
    git checkout $release_type/$version

    # back to root directory
    Pop-Location

    # commit changes
    git commit --all --message="bumped version to $version-$fhir_release"
}

# Move to submodule common
Push-Location .\common

# start release for common first
Update-Version -start_branch $from_branch -release_branch "$release_type/$version" -propFile $repo_root\common\src\firely-net-common.props

# back to root directory
Pop-Location

$fhir_releases | ForEach-Object { Update-Common -fhir_release $PSItem}

# back to the directory when this script was started
Pop-Location