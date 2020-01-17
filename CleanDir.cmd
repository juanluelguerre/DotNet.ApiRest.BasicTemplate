@echo off

CHOICE /C YN /M "Do you want to continue"
IF ERRORLEVEL == 2 GOTO END

ECHO CLEANING ...
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO RMDIR /S /Q "%%G" > NUL
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO RMDIR /S /Q "%%G"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S Binaries') DO RMDIR /S /Q "%%G"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S packages') DO RMDIR /S /Q "%%G"
ECHO.
ECHO Proceso Finalizado.
ECHO.
:END