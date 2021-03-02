Param(
    [Parameter(Mandatory=$true)]
    [string] $version,
    [ValidateSet('release', 'hotfix')]
    [Parameter(Mandatory=$true)] [string] $release_type
)

$fhir_releases = @('stu3', 'r4', 'r5')

# the root path of the repository
$repo_root = "$PsScriptRoot\.."
Push-Location $repo_root

function Update-Version([string] $develop_branch, [string] $release_branch, [string] $propFile)
{
    Write-Host "Update-Version in release branch $release_branch"

    # Pull the lastest changes on develop_branch
    git fetch origin
    git checkout $develop_branch
    git merge --ff-only origin/$develop_branch

    # create a release branch
    git checkout -b $release_branch $develop_branch

    # update version number
    & $PSScriptRoot\UpdateVersion.ps1 -propFile $propFile -newVersion $version

    # commit changes
    git commit --all --message="bumped version to $version"
}

function Update-Common([string] $fhir_release)
{
    Write-Host "Start release for version $fhir_release"

    # start release for sdk 
    Update-Version -develop_branch "develop-$fhir_release" -release_branch "$release_type/$version-$fhir_release" -propFile $repo_root\src\firely-net-sdk.props

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
Update-Version -develop_branch "develop" -release_branch "$release_type/$version" -propFile $repo_root\common\src\firely-net-common.props

# back to root directory
Pop-Location

$fhir_releases | ForEach-Object { Update-Common -fhir_release $PSItem}