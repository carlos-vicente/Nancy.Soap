# Script to execute all fixie tests on projects in this directory

$testAssemblies = Get-ChildItem "tests/*.Tests/bin/**/*.Tests.dll"

$fixieConsole = Get-ChildItem "packages/Fixie.*/lib/**/Fixie.Console.exe"

Foreach($test in $testAssemblies)
{
    $cmd = $fixieConsole.FullName
    $arg = $test.FullName
	$xml = $test.FullName + ".xml"
	
	Write-Host "Going to execute test assembly " -nonewline; Write-Host $arg -foregroundcolor red
	Write-Host $xml
	
	& ($cmd) ($arg) --NUnitXml ($xml)
}