param($installPath, $toolsPath, $package, $project)
echo "hoi"
echo "Project: $project"

$validation_file = $project.ProjectItems.Item("validation.zip")
 
# set 'Copy To Output Directory' to 'Copy always'
$copyToOutput1 = $validation_file.Properties.Item("CopyToOutputDirectory")
$copyToOutput1.Value = 2
 
echo $copyToOutput1

# 0: Do not copy (default, you don’t need this script then)
# 1: Copy always
# 2: Copy if newer