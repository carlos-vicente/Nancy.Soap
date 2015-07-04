# Script to execute all fixie tests on projects in this directory

$testAssemblies = Get-ChildItem "tests/*.Tests/bin/**/*.Tests.dll"

$fixieConsole = Get-ChildItem "packages/Fixie.*/lib/**/Fixie.Console.exe"

Foreach($test in $testAssemblies)
{
    $cmd = $fixieConsole.FullName
    $arg = $test.FullName
	
	Write-Host "Going to execute test assembly " -nonewline; Write-Host $arg -foregroundcolor red
    
    $testResult = & $cmd $arg
	
	$split = $testResult -split '[\r\n]'
	
	Write-Host "Execution result:";
	Foreach($str in $split){
		Write-Host $str -foregroundcolor red;
	}
	
	#Add-AppveyorCompilationMessage -Message $testResult -Category Information
}