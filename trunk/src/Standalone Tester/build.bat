@echo off
set dlls=..\NmliTests\bin\Debug
copy /y %dlls%\Nmli.dll .
copy /y %dlls%\NmliTests.exe .
copy /y %dlls%\nunit.framework.dll .


csc /r:NmliTests.exe /out:test32.exe /platform:x86 test.cs
csc /r:NmliTests.exe /out:test64.exe /platform:x64 test.cs