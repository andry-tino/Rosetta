# Test runner to be executed after building Rosetta
# This script expects to have defined a few things

param(
  [parameter(mandatory = $true)] [string] $MSTestPath,
  [parameter(mandatory = $true)] [string] $WorkspacePath,
  [string] $OutputPath = $WorkspacePath,
  [string[]] $TestAssemblies = @(
	'ASTWalker.UnitTests\bin\Debug\Rosetta.ASTWalker.UnitTests.dll', 
	'ASTWalker.Helpers.UnitTests\bin\Debug\Rosetta.ASTWalker.Helpers.UnitTests.dll', 
	'TranslationUnits.UnitTests\bin\Debug\Rosetta.TranslationUnits.UnitTests.dll', 
	'Runner.UnitTests\bin\Debug\Rosetta.Runner.UnitTests.dll'
  )
);

# Save the trx file in the workspace
$ResultsFilePath = join-path -Path $OutputPath -ChildPath 'Rosetta.TestResults.trx';
$ResultsFile = "/resultsfile:""$ResultsFilePath""";

$Options = @();
foreach ($TestAssembly in $TestAssemblies)
{
  $PathToTestAssembly = join-path -Path $WorkspacePath -ChildPath 'test' | 
						join-path -ChildPath $TestAssembly;
  $Options += "/testcontainer:""$PathToTestAssembly""";
}

$Options += $ResultsFile;

# Testing
& $MSTestPath $Options;

exit $LASTEXITCODE;