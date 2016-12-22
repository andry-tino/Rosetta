# Test runner to be executed after building Rosetta
# This script expects to have defined a few things

param(
  [parameter(mandatory = $true)] [string] $VSTestConsolePath,
  [parameter(mandatory = $true)] [string] $WorkspacePath,
  [string[]] $TestAssemblies = @(
	'ASTWalker.UnitTests\bin\Debug\Rosetta.ASTWalker.UnitTests.dll', 
	'ASTWalker.Factories.UnitTests\bin\Debug\Rosetta.ASTWalker.Factories.UnitTests.dll', 
	'ASTWalker.Helpers.UnitTests\bin\Debug\Rosetta.ASTWalker.Helpers.UnitTests.dll', 
	'TranslationUnits.UnitTests\bin\Debug\Rosetta.TranslationUnits.UnitTests.dll', 
	'Runner.UnitTests\bin\Debug\Rosetta.Runner.UnitTests.dll',
	'ScriptSharp.Definition.ASTWalker.Factories.UnitTests\bin\Debug\Rosetta.ScriptSharp.Definition.ASTWalker.Factories.UnitTests.dll',
	'ScriptSharp.Definition.ASTWalker.UnitTests\bin\Debug\Rosetta.ScriptSharp.Definition.ASTWalker.UnitTests.dll'
  )
);

$RunSettingsPath = join-path -Path $WorkspacePath -ChildPath 'test' | 
				   join-path -ChildPath 'Default.runsettings';
$RunSettings = "/Settings:""$RunSettingsPath""";

$Options = @();
foreach ($TestAssembly in $TestAssemblies)
{
  $PathToTestAssembly = join-path -Path $WorkspacePath -ChildPath 'test' | 
						join-path -ChildPath $TestAssembly;
  $Options += $PathToTestAssembly;
}

$Options += $RunSettings;

# Testing
& $VSTestConsolePath $Options;

exit $LASTEXITCODE;