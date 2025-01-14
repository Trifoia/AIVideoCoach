@echo off
set TargetFramework=%1
set ProjectName=%2

del "*.nupkg"
REN  Trifoia.Module.AIVideoCoach.nuspec.REMOVE Trifoia.Module.AIVideoCoach.nuspec 
"..\..\oqtane.framework\oqtane.package\nuget.exe" pack Trifoia.Module.AIVideoCoach.nuspec -Properties targetframework=net9.0;projectname=Trifoia.Module.AIVideoCoach
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\Packages\" /Y