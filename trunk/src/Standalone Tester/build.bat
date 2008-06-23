@echo off
csc /r:NmliTests.dll /out:test32.exe /platform:x86 test.cs
csc /r:NmliTests.dll /out:test64.exe /platform:x64 test.cs