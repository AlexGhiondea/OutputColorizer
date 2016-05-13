@echo off
set DOTNET_PATH=%~dp0tools\dotnetcli\
set DOTNET_HOST=%DOTNET_PATH%dotnet.exe
set DOTNET_VERSION=1.0.0-preview1-002700
set DOTNET_LOCAL_PATH=%DOTNET_PATH%dotnet-dev-win-x64.%DOTNET_VERSION%.zip
set DOTNET_REMOTE_PATH=https://dotnetcli.blob.core.windows.net/dotnet/beta/Binaries/%DOTNET_VERSION%/dotnet-dev-win-x64.%DOTNET_VERSION%.zip
if exist "%DOTNET_HOST%" (
	echo Tools already initialized.
	goto :EOF
)
if not exist "%DOTNET_PATH%" mkdir "%DOTNET_PATH%"
:DownloadDotNet
echo Downloading the DotNET CLI...
powershell -NoProfile -ExecutionPolicy unrestricted -Command "(New-Object Net.WebClient).DownloadFile('%DOTNET_REMOTE_PATH%', '%DOTNET_LOCAL_PATH%'); Add-Type -Assembly 'System.IO.Compression.FileSystem' -ErrorVariable AddTypeErrors; if ($AddTypeErrors.Count -eq 0) { [System.IO.Compression.ZipFile]::ExtractToDirectory('%DOTNET_LOCAL_PATH%', '%DOTNET_PATH%') } else { (New-Object -com shell.application).namespace('%DOTNET_PATH%').CopyHere((new-object -com shell.application).namespace('%DOTNET_LOCAL_PATH%').Items(),16) }"
if not exist "%DOTNET_HOST%" (
	echo Error: Could not download the CLI.
	exit /b 1
)
echo Finished Downloading the DotNET CLI succesfully.
:EOF
exit /b 0