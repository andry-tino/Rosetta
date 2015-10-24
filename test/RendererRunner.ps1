# Runs the renderer in order to generate output files

param(
  [parameter(mandatory = $true)] [string] $WorkspacePath,
  [parameter(mandatory = $true)] [string] $OutputPath
);

write-output "Running renderer...";
write-output "Workspace path is: $WorkspacePath";

$RenderPath = "$OutputPath\RenderResults";
new-item "$RenderPath" -type directory | out-null;

$RendererExecPath = "$WorkspacePath\test\renderers\TranslationUnits.Renderings\bin\Debug\Rosetta.TranslationUnits.Renderings.exe";

# Testing
$Result = & $RendererExecPath "$RenderPath";
if ($LASTEXITCODE -gt 0)
{
  throw new-object 'System.Exception';
}

write-output "Files generated in: $RenderPath!";
get-childitem "$RenderPath";