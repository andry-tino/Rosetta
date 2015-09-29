# Test runner to be executed after building Rosetta
# This script expects to have defined a few things

param(
	[parameter(mandatory = $true)] [string]	$MSTestPath,
	[parameter(mandatory = $true)] [string]	$WorkspacePath,
	[string[]] $TestAssemblies = @('ASTWalker.UnitTests', 'TranslationUnits.UnitTests')
);

foreach ($TestAssembly in $TestAssemblies)
{
	$PathToTestAssembly = join-path -Path $WorkspacePath -ChildPath $TestAssembly;
	$PathToTestAssembly = join-path -Path $WorkspacePath -ChildPath "bin/Debug/$TestAssembly.dll";
	
	# Testing
	& $MSTestPath "/testcontainer:$PathToTestAssembly"
}