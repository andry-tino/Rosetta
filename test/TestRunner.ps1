# Test runner to be executed after building Rosetta
# This script expects to have defined a few things

param(
  [parameter(mandatory = $true)] [string] $MSTestPath,
  [parameter(mandatory = $true)] [string] $WorkspacePath,
  [string[]] $TestAssemblies = @('ASTWalker.UnitTests', 'TranslationUnits.UnitTests')
);

foreach ($TestAssembly in $TestAssemblies)
{
  $PathToTestAssembly = join-path -Path $WorkspacePath -ChildPath 'test';
  $PathToTestAssembly = join-path -Path $PathToTestAssembly -ChildPath $TestAssembly;
  $PathToTestAssembly = join-path -Path $PathToTestAssembly -ChildPath "bin/Debug/$TestAssembly.dll";
  
  write-output "Executing $PathToTestAssembly...";
	
  # Testing
  $Result = & $MSTestPath "/testcontainer:$PathToTestAssembly";
  if ($LASTEXITCODE -gt 0)
  {
    throw new-object 'System.Exception';
  }
}
