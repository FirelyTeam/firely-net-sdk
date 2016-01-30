param($installPath, $toolsPath, $package, $project)
echo "Project: $project"

$validation_file = $project.ProjectItems.Item("validation.xml.zip")
 
# set 'Copy To Output Directory' to 'Copy if newer'
$copyToOutput1 = $validation_file.Properties.Item("CopyToOutputDirectory")
$copyToOutput1.Value = 2
 
# 0: Do not copy (default, you don’t need this script then)
# 1: Copy always
# 2: Copy if newer