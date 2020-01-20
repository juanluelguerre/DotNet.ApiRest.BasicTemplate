@echo off

setlocal

IF [%1]==[] SET SLN=".\ElGuerre.Items.sln"
IF NOT [%1]==[] SET SLN=%1

IF [%2]==[] SET TEST_FOLDER_NAME="ElGuerre.Items.Api.Tests"
IF NOT [%2]==[] SET TEST_FOLDER_NAME=%2

IF [%3]==[] SET INT_TEST_FOLDER_NAME="ElGuerre.Items.Api.IntegrationTests"
IF NOT [%3]==[] SET INT_TEST_FOLDER_NAME=%3

dotnet build-server shutdown

:: review to  update with the latest version
dotnet tool install --global dotnet-sonarscanner > nul
dotnet tool install --global dotnet-reportgenerator-globaltool > nul

dotnet restore %SLN%
dotnet build %SLN%

:: Unitests and Code Coverage
dotnet test --logger trx /p:CollectCoverage=true /p:CoverletOutputFormat=Cobertura /p:CoverletOutput=%CD%\TestResults\Coverage\Tests.Cobertura.xml "./test/%TEST_FOLDER_NAME%/%TEST_FOLDER_NAME%.csproj"

:: Integration Tests
dotnet test "./test/%INT_TEST_FOLDER_NAME%/%INT_TEST_FOLDER_NAME%.csproj"

:: Show unit tests report after run all tests.
reportgenerator -reports:%CD%\TestResults\Coverage\Tests.Cobertura.xml -targetdir:%CD%\TestResults\Reports\ -reportTypes:"HTML;HTMLInline;Cobertura"

.\TestResults\reports\index.htm