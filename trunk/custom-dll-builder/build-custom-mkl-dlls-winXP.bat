@echo off


REM     ------ set these paths as necessary -------

set _mkl=C:\Program Files\Intel\MKL\10.0.3.021
set _vs=C:\Program Files\Microsoft Visual Studio 8

REM     -------------------------------------------




if NOT EXIST "%_mkl%" (
echo Please edit this batch file so that the _mkl variable points to your MKL installation
pause
exit /B
)

if NOT EXIST "%_vs%" (
echo Please edit this batch file so that the _vs variable points to your Visual Studio 2005 installation
pause
exit /B
)


if NOT EXIST "%NMLI_PATH%\src\Nmli.sln" (
echo The environment variable NMLI_PATH must be set to your NMLI directory.
pause
exit /B
)


if EXIST "%windir%\syswow64" (
echo  This script is meant for 32-bit Windows XP only.
echo  The x64 batch builds both 32-bit and 64-bit dlls, so use that instead.
pause
exit /B
)

set _nmli32mkl=%NMLI_PATH%\bin\x86\MKL


echo.
echo.
echo This batch will setup 32-bit MKL for use with NMLI on Windows XP 
echo.
echo The custom DLL and its dependencies will be placed in
echo       %_nmli32mkl%
echo.
pause



mkdir lib
copy "%_mkl%\tools\builder\lib\*.lib" .\lib\



echo Starting to build 32-bit dll...
echo.

md "%_nmli32mkl%"

set PATH=%_vs%\Common7\IDE;%_vs%\VC\BIN;%_vs%\Common7\Tools\bin
set INCLUDE=%_vs%\VC\INCLUDE;%_vs%\VC\PlatformSDK\include;%_vs%\SDK\v2.0\include
set LIB=%_vs%\VC\LIB;%_vs%\VC\PlatformSDK\lib;%_vs%\SDK\v2.0\lib

set path=%_mkl%\ia32\bin;%path%
set INCLUDE=%_mkl%\include;%include%
set LIBRARY_PATH=%_mkl%\ia32\lib
set CPATH=%_mkl%\include
set FPATH=%_mkl%\include
set lib=%_mkl%\ia32\lib;%lib%

nmake ia32 export=function_list name=mkl
mt -manifest mkl.dll.manifest -outputresource:mkl.dll;2

move /y mkl.dll "%_nmli32mkl%\mkl.dll"
copy /y "%_mkl%\ia32\bin\libguide40.dll" "%_nmli32mkl%\libguide40.dll"
del mkl.*

echo.
echo Done building 32-bit dll
echo.

rmdir /s /q lib

echo.
echo All done!
pause

