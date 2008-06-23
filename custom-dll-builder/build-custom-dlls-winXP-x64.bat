@echo off

set _vs=C:\Program Files (x86)\Microsoft Visual Studio 8
set _mkl=C:\Program Files\Intel\MKL\10.0.3.021

mkdir lib
copy "%_mkl%\tools\builder\lib\*.lib" .\lib\

REM 32-bit

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

copy mkl.dll 32bit-mkl.dll
copy /y 32bit-mkl.dll ..\bin\x86\MKL\mkl.dll
del mkl.*

echo Done building 32-bit dll
pause


REM 64-bit
set path=%_mkl%\em64t\bin;%_vs%\VC\BIN\amd64;C:\Program Files\Microsoft.NET\SDK\v2.0 64bit\Bin
set LIBRARY_PATH=%_mkl%\em64t\lib
set lib=%_mkl%\em64t\lib;%_vs%\VC\PlatformSDK\Lib\AMD64;%_vs%\VC\lib\amd64

nmake em64t export=function_list name=mkl
mt -manifest mkl.dll.manifest -outputresource:mkl.dll;2

copy mkl.dll 64bit-mkl.dll
copy /y 64bit-mkl.dll ..\bin\x64\MKL\mkl.dll
del mkl.*

echo Done building 64-bit dll
pause

rmdir /s /q lib