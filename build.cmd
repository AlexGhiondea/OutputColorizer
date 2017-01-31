@echo off
setlocal

set _config=%1
if not defined _config (
  set _config=Debug
)

echo Building Config '%_config%'

echo Restoring packages
dotnet restore

echo Cleaning projects
dotnet clean OutputColorizer.sln

echo Building solution
dotnet build OutputColorizer.sln -c %_config%

echo Running tests
dotnet test --no-build -c %_config% test\OutputColorizer.Tests.csproj

echo Creating NuGet package
dotnet pack --no-build -c %_config% src\OutputColorizer.csproj

endlocal

@echo on