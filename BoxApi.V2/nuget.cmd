%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe ..\BoxApiV2.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger

nuget.exe update -self
nuget.exe pack BoxApiV2.nuspec -Symbols
