# Runs Rosetta on different sources in order to generate demo output
# Default values work when running the script from the `demo` folder

# When running from `demo` folder, just run:
# ./DemoRunner.ps1 <Workspace-Path>

param(
  # Path to `Rosetta` folder
  [parameter(mandatory = $true)] [string] $WorkspacePath,
  # The path to the folder containing all CS sources, defaults to demo directory
  [string] $SourcePath = $null,
  # The path to location where to create all demo files, defaults to current directory
  [string] $OutputPath = $null,
  # List of source files to translate
  $SourceFiles = @{ 'Source001.cs' = 'source001.ts' },
  # List of directories containing projects to translate
  # TODO: Use this once translating projects is supported
  $SourceProjects = @{}
);

if ([string]::IsNullOrEmpty($SourcePath)) 
{
  $SourcePath = join-path -Path $WorkspacePath -ChildPath 'demo';
}

if ([string]::IsNullOrEmpty($OutputPath))
{
  $OutputPath = join-path -Path $SourcePath -ChildPath 'Output';
}

# Path to Rosetta executable
$Rosetta = join-path -Path $WorkspacePath -ChildPath 'src\Runner\bin\Debug\Rosetta.Runner.exe';

# Handling output folders, if they already exist, remove them and all their content
if (test-path $OutputPath)
{
  remove-item $OutputPath -recurse
}

new-item "$OutputPath" -type directory | out-null;

# Handling single files to translate
foreach ($SourceEntry in $SourceFiles.GetEnumerator())
{
  $SourceFile = $SourceEntry.Name;
  $OutputFile = $SourceEntry.Value;

  $Options = @(
    '--output', $OutputPath, 
	'--file', $SourceFile, 
	'--filename', $OutputFile
  );

  # Calling Rosetta and providing the output file
  Write-output "$Options"
  & $Rosetta $Options;
}
