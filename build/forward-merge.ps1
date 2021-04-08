Param(
    [Parameter(Mandatory=$true)]
    [string] $source_branch,
    [Parameter(Mandatory=$true)]
    [string] $target_branch
)

function Get-Latest-Changes([string] $branch)
{
    # Pull the lastest changes on branch
    git fetch origin --recurse-submodules
    git checkout $branch --recurse-submodules
    git merge --ff-only origin/$branch
}

# get latest change of the source branch
Get-Latest-Changes -branch $source_branch
# get latest change of the target branch
Get-Latest-Changes -branch $target_branch

# create a update branch
git checkout -b feature/update-$target_branch-with-$source_branch $target_branch

# merge changes from source into target
git merge --no-ff origin/$source_branch

