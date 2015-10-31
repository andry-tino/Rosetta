# Runs the renderer in order to generate output files

param(
  [parameter(mandatory = $true)] [string] $WorkspacePath,
  [parameter(mandatory = $true)] [string] $OutputPath
);

write-output "Running renderer...";
write-output "Workspace path is: $WorkspacePath";

$RenderPathTranslationUnits = "$OutputPath\TranslationUnits.Renderings";
$RenderPathAST = "$OutputPath\ASTWalker.Renderings";
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

# Running renderers for Translation units
$Result = & $RendererExecPathAST "$RenderPathAST";
if ($LASTEXITCODE -gt 0)
{
  throw new-object 'System.Exception';
}

write-output "Files generated in: $RenderPathAST!";
get-childitem "$RenderPathAST";
