# Build script

param(
	[Parameter(Mandatory = $true)]
	[ValidateNotNullOrEmpty()]
    [string] $MSBuildPath,
	
	[ValidateNotNullOrEmpty()]
	[ValidateSet('Debug', 'Release')]
	[string] $Configuration = 'Debug'
)

echo "Building..."

$ScriptPath = split-path -parent $MyInvocation.MyCommand.Definition

& $MSBuildPath "$ScriptPath\Rosetta.sln" "/p:Configuration=$Configuration"

echo 'Build finished!';