@echo off

CLS
setlocal

set /p token=<sonar.txt
IF [%1]==[] SET SLN="./ElGuerre.Taskin.Full.sln"
IF NOT [%1]==[] SET SLN=%1

:: dotnet sonarscanner begin /k:"company:project" /n:"Project" /v:"#.#.#" /o:"companyname" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="%token%" /d:sonar.language="cs" /d:sonar.exclusions="**/bin/**/*,**/obj/**/*" /d:sonar.coverage.exclusions="Project.Tests/**,**/*Tests.cs" /d:sonar.cs.opencover.reportsPaths="%cd%\lcov.opencover.xml"
::dotnet SonarScanner.MSBuild.exe begin /k:"Taskin" /d:sonar.organization="juanluelguerre-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="%token%"
dotnet sonarScanner begin /k:"Taskin" /d:sonar.organization="juanluelguerre-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="%token%" /d:sonar.language="cs" /d:sonar.exclusions="**/bin/**/*,**/obj/**/*" /d:sonar.coverage.exclusions="test/**" /d:sonar.cs.opencover.reportsPaths="%cd%\lcov.opencover.xml"
dotnet restore %sln%
dotnet build %sln%
dotnet test test/UnitTets/ElGuerre.Taskin.Api.Services.Tests/ElGuerre.Taskin.Api.Services.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=\"opencover,lcov\" /p:CoverletOutput=../../lcov
dotnet sonarscanner end /d:sonar.login="%token%"
::dotnet SonarScanner.MSBuild.exe end /d:sonar.login="%token%"