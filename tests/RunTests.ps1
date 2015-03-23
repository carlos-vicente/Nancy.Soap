# Script to execute all fixie tests on projects in this directory

$testAssemblies = Get-ChildItem *.Tests/bin/**/*.Tests.dll

$fixieConsole = Get-ChildItem "packages/Fixie.*/lib/**/Fixie.Console.exe"

Foreach($test in $testAssemblies){
	
	$cmd = $fixieConsole.FullName
	$arg = $test.FullName
	
	& $cmd $arg
}