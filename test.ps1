# Test script

param(
	[Parameter(Mandatory = $true)]
	[ValidateNotNullOrEmpty()]
    [string] $VSTestPath
)

echo "Testing..."

$ScriptPath = split-path -parent $MyInvocation.MyCommand.Definition

$TestRunnerScriptPath = "$ScriptPath\test\TestRunner.ps1"
& $TestRunnerScriptPath -WorkspacePath $ScriptPath -VSTestConsolePath $ENV:$VSTestPath;

echo 'Tests run finished!';

echo "Generating TypeScript files..."

$RendererScriptPath = "$ScriptPath\test\RendererRunner.ps1"
& $RendererScriptPath -WorkspacePath $ScriptPath -OutputPath $ScriptPath;

echo 'Generation finished!'