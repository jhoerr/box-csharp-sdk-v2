%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe BoxApiV2.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger

del nuget\* /F /Q

mkdir nuget\lib\net40\

copy SDK\bin\Release\BoxApi.V2.SDK.dll nuget\lib\net40
copy SDK\bin\Release\BoxApi.V2.SDK.pdb nuget\lib\net40
copy SDK\bin\Release\BoxApi.V2.SDK.xml nuget\lib\net40

nuget.exe update -self
nuget.exe pack BoxApiV2.nuspec -Symbols -BasePath nuget -Output nuget
