param(
    [string] $task = "Test",
    [hashtable] $parameters = @{},
    [hashtable] $properties = @{}
)

$path = Split-Path -Path $MyInvocation.MyCommand.Path

write-host "Importing psake"
Import-Module ($path + '\..\tools\PSake\psake.psm1')

Try
{
  write-host "Starting build.ps1"
  Invoke-psake ($path + '\build.ps1') $task -properties $properties -parameters $parameters

  if ($psake.build_success -eq $false)
  {
    write-host "build.ps1 failed" -fore RED
    exit 1
  }
  else
  {
    write-host "build.ps1 succeeded" -fore GREEN
    exit 0
  }
}
Finally
{
  Remove-Module psake
}
