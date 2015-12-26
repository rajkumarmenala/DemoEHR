@Echo OFF
SETLOCAL
SET ERRORLEVEL=

call "%ProgramFiles(x86)%\Microsoft SQL Server\120\DAC\bin\SqlPackage.exe"  -Action:"Publish" -SourceFile:"src\Monad.EHR.Database\bin\Release\Monad.EHR.Database.dacpac" -Profile:"src\Monad.EHR.Database\Monad.EHR.Database.publish.xml"

rmdir /S /Q "C:\work\Infinite\EABSourceCode\Build-Temp\EHR"

call %userprofile%\.dnx\runtimes\dnx-clr-win-x86.1.0.0-beta8\bin\dnu.cmd publish "C:\work\Infinite\EABSourceCode\Output\EHR\SourceCode\src\Monad.EHR.Web.App" --out "C:\work\Infinite\EABSourceCode\Build-Temp\EHR" --configuration Release --runtime dnx-clr-win-x86.1.0.0-beta8 --wwwroot-out "wwwroot" --quiet

IF NOT  EXIST "C:\work\Infinite\EHR" GOTO NOWINDIR
     rmdir /S /Q "C:\work\Infinite\EHR"
:NOWINDIR

call "%ProgramFiles(x86)%\IIS\Microsoft Web Deploy V3\msdeploy.exe" -source:contentPath='C:\work\Infinite\EABSourceCode\Build-Temp\EHR' -dest:contentPath='C:\work\Infinite\EHR' -verb:sync -enableRule:DoNotDeleteRule -retryAttempts:2 -disablerule:BackupRule

exit /b %ERRORLEVEL%
ENDLOCAL
