# Build script

param(
	[Parameter(Mandatory = $true)]
	[ValidateNotNullOrEmpty()]
    [string] $MSBuildPath
)

echo "Cleaning..."

$ScriptPath = split-path -parent $MyInvocation.MyCommand.Definition

& $MSBuildPath "$ScriptPath\Rosetta.sln" /target:clean

echo 'Clean finished!';