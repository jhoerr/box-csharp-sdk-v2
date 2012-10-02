%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe BoxApiV2.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger

rd /s /q nuget

mkdir nuget\lib\net40\

copy BoxApi.V2\bin\Release\BoxApi.V2.dll nuget\lib\net40
copy BoxApi.V2\bin\Release\BoxApi.V2.pdb nuget\lib\net40
copy BoxApi.V2\bin\Release\BoxApi.V2.xml nuget\lib\net40

nuget.exe update -self
nuget.exe pack BoxApiV2.nuspec -Symbols -BasePath nuget -Output nuget
