cls
powershell -Command "& { try { [Console]::WindowWidth = 150; [Console]::WindowHeight = 50;} catch {}; Start-Transcript %~dp0runbuild.txt; Import-Module %~dp0..\tools\psake\psake.psm1; Invoke-psake %~dp0..\build\build.ps1 %*; Stop-Transcript; exit !($psake.build_success); }"

ECHO %ERRORLEVEL%
EXIT /B %ERRORLEVEL%
