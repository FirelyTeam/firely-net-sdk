Param(
    [Parameter(Mandatory=$true, HelpMessage="Enter the version for the new release")]
    [string] $version,
    [ValidateSet('release', 'hotfix')]
    [Parameter(Mandatory=$true, HelpMessage="Enter the type of release (release|hotfix).")] [string] $release_type
)

$fhir_releases = @('stu3', 'r4', 'r4B', 'r5')

Push-Location $PsScriptRoot\..

function TagAndPush([string] $release_branch, [string] $tag_name)
{
    Write-Host "Tag $release_type with $tag_name"

    # go to release branch
    git checkout $release_branch 

    # tag the release
    git tag -a -m "version $tag_name" $tag_name

    # push commits and tags
    git push --follow-tags --set-upstream origin $release_branch
}

function Merge-Push([string] $source_branch, [string] $target_branch)
{
    Write-Host "Merge and push $source_branch into $target_branch" 

    # save current branch name
    $current = git branch --show-current

    # go to target branch
    git checkout $target_branch 

    # merge source branch (fast forward)
    git merge --ff origin/$source_branch

    # push the commits to origin
    git push 

    # go back to the previous branch
    git checkout $current 
}

function Finalize([string] $fhir_release_suffix)
{
    # tag common first
    TagAndPush -release_branch "$release_type/$version$fhir_release_suffix" -tag_name "v$version$fhir_release_suffix"

    # merge release in master
    Merge-Push -source_branch "$release_type/$version$fhir_release_suffix" -target_branch "master$fhir_release_suffix"
    
    # merge release in develop
    Merge-Push -source_branch "$release_type/$version$fhir_release_suffix" -target_branch "develop$fhir_release_suffix"
}

# Move to submodule common
Push-Location .\common

# tag and merge in develop and master
Finalize -fhir_release_suffix ""

# back to root directory
Pop-Location

$fhir_releases | ForEach-Object { Finalize -fhir_release_suffix "-$PSItem"}

# back to the directory when this script was started
Pop-Location