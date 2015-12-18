@Echo OFF
SETLOCAL
SET ERRORLEVEL=

call %userprofile%\.dnx\runtimes\dnx-clr-win-x86.1.0.0-beta8\bin\dnu.cmd restore

exit /b %ERRORLEVEL%
ENDLOCAL
