@echo off


REM     ------ set these paths as necessary -------

set _vs=C:\Program Files (x86)\Microsoft Visual Studio 8
set _mkl=C:\Program Files\Intel\MKL\10.0.3.021
set _mkl_ds_short=C:\\PROGRA~1\\Intel\\MKL\\10.0.3.021\\

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
echo The environment variable NMLI_PATH must be set to your NMLI directory
pause
exit /B
)




set _nmli32mkl=%NMLI_PATH%\bin\x86\MKL
set _nmli64mkl=%NMLI_PATH%\bin\x64\MKL

echo.
echo.
echo This batch will setup both 32-bit and 64-bit MKL
echo  for use with NMLI on Windows XP x64
echo.
echo The custom DLL and its dependencies will be placed in
echo       %_nmli32mkl%
echo  and  %_nmli64mkl%
echo.
pause



mkdir lib
copy "%_mkl%\tools\builder\lib\*.lib" .\lib\

set _mkl_makefile="%_mkl%\tools\builder\makefile"
set _nmakeCmd=nmake /f %_mkl_makefile% /E local_mklpath=%_mkl_ds_short% /E mkl32_libpath=$(local_mklpath)ia32\\lib\\ /E mklem64t_libpath=$(local_mklpath)em64t\\lib\\
set _nmakeParams= export=function_list name=mkl

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

%_nmakeCmd% ia32 %_nmakeParams%
mt -manifest mkl.dll.manifest -outputresource:mkl.dll;2

move /y mkl.dll "%_nmli32mkl%\mkl.dll"
copy /y "%_mkl%\ia32\bin\libguide40.dll" "%_nmli32mkl%\libguide40.dll"
del mkl.*

echo.
echo Done building 32-bit dll
pause
echo.


echo.
echo Starting to build 64-bit dll...
echo.

md "%_nmli64mkl%"

set path=%_mkl%\em64t\bin;%_vs%\VC\BIN\amd64;C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin
set LIBRARY_PATH=%_mkl%\em64t\lib
set lib=%_mkl%\em64t\lib;%_vs%\VC\PlatformSDK\Lib\AMD64;%_vs%\VC\lib\amd64

%_nmakeCmd% em64t %_nmakeParams%
mt -manifest mkl.dll.manifest -outputresource:mkl.dll;2

move /y mkl.dll "%_nmli64mkl%\mkl.dll"
copy /y "%_mkl%\em64t\bin\libguide40.dll" "%_nmli64mkl%\libguide40.dll"
del mkl.*

echo.
echo Done building 64-bit dll
echo.

rmdir /s /q lib

echo.
echo All done!
pause


