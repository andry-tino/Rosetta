# Runs the renderer in order to generate output files

param(
  # Path to `Rosetta` folder
  [parameter(mandatory = $true)] [string] $WorkspacePath,
  # Path to folder hosting output files
  [parameter(mandatory = $true)] [string] $OutputPath,
  # If `true` forces all translated files to be emitted in shell
  [bool] $PrintRenderedFiles = $true
);

# Handling output folders, if they already exist, remove them and all their content (TODO)
$RenderPathTranslationUnits = "$OutputPath\TranslationUnits.Renderings";
$RenderPathAST = "$OutputPath\ASTWalker.Renderings";

if (test-path $RenderPathTranslationUnits)
{
	remove-item $RenderPathTranslationUnits -recurse
}
if (test-path $RenderPathAST)
{
	remove-item $RenderPathAST -recurse
}

new-item "$RenderPathTranslationUnits" -type directory | out-null;
new-item "$RenderPathAST" -type directory | out-null;

$RendererExecPathTranslationUnits = "$WorkspacePath\test\renderers\TranslationUnits.Renderings\bin\Debug\Rosetta.TranslationUnits.Renderings.exe";
$RendererExecPathAST = "$WorkspacePath\test\renderers\ASTWalker.Renderings\bin\Debug\Rosetta.ASTWalker.Renderings.exe";

# Running renderers for Translation units
$Result = & $RendererExecPathTranslationUnits "$RenderPathTranslationUnits";
if ($LASTEXITCODE -gt 0)
{
  throw new-object 'System.Exception';
}

write-output "Files generated in: $RenderPathTranslationUnits!";
get-childitem "$RenderPathTranslationUnits";

if ($PrintRenderedFiles)
{
  foreach ($file in get-childitem "$RenderPathTranslationUnits")
  {
    write-output "Printing: $file!";
	write-output "----------------";
    get-content $file.FullName;
  }
}

# Running renderers for AST walkers
$Result = & $RendererExecPathAST "$RenderPathAST";
if ($LASTEXITCODE -gt 0)
{
  throw new-object 'System.Exception';
}

write-output "Files generated in: $RenderPathAST!";
get-childitem "$RenderPathAST";

if ($PrintRenderedFiles)
{
  foreach ($file in get-childitem "$RenderPathAST")
  {
    write-output "Printing: $file!";
	write-output "----------------";
    get-content $file.FullName;
  }
}
