@echo off
cls

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)

.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

echo.
echo ***** MSBUILD *****
msbuild src\cpcontrib.core.sln /p:Configuration=Release /p:OutputPath=Build\Release

