@echo off


REM     ------ set this path as necessary -------

set _acml32=C:\Programs\AMD\ACML 4.0.0\32-bit

REM     -----------------------------------------



if NOT EXIST "%_acml32%" (
echo Please edit this batch file so that the _acml32 variable points to your 32-bit IFORT ACML installation
pause
exit /B
)

if NOT EXIST "%NMLI_PATH%\src\Nmli.sln" (
echo The environment variable NMLI_PATH must be set to your NMLI directory.
pause
exit /B
)

set _nmli32acml=%NMLI_PATH%\bin\x86\ACML

echo.
echo.
echo This batch will setup 32-bit ACML for use with NMLI on Windows XP 
echo.
echo The ACML libraries will be placed in
echo       %_nmli32acml%
echo.
pause

echo.
md "%_nmli32acml%"
echo.


IF %NUMBER_OF_PROCESSORS% == 1 (
echo  This machine has only one processor, so we're installing the single processor version...
copy /y "%_acml32%\ifort32\lib\*.dll" "%_nmli32acml%"
) ELSE (
echo  This machine has more than one processor, so we're installing the multi-processor version...
copy /y "%_acml32%\ifort32_mp\lib\*.dll" "%_nmli32acml%"
echo Standardizing nomenclature...
move "%_nmli32acml%\libacml_mp_dll.dll" "%_nmli32acml%\libacml_dll.dll"
)
echo.
echo All done.
echo.
pause


