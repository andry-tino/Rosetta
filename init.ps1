# Initialization script

param(
	[Parameter(Mandatory = $true)]
	[ValidateNotNullOrEmpty()]
    [string] $NUGetPath
)

echo "Initializing..."

echo "Installing NuGet packages..."

$ScriptPath = split-path -parent $MyInvocation.MyCommand.Definition

$ConfigFiles = gci $ScriptPath -Recurse -Depth 2 | where { $_.name -eq 'packages.config' }
foreach ($ConfigFile in $ConfigFiles)
{
	& $NUGetPath install $ConfigFile.FullName -solutiondirectory $ScriptPath;
}

echo 'NuGet packages installation finished!';

echo 'Initialization finished!';