@echo off
call "%VS100COMNTOOLS%vsvars32.bat"
title Command Prompt (MSVC++ 2010) x86

set QMAKESPEC=win32-msvc2010
set QTSRC_DIR=C:\Qt\Qt5.3.1\5.3\Src
set BUILD_DIR=%QTSRC_DIR%\..\qt5.3-build-msvc2010_noopengl

if not exist %QTSRC_DIR% goto error
if not exist %BUILD_DIR% mkdir %BUILD_DIR%

set PATH=%PATH%;%QTSRC_DIR%\qtbase\bin;%QTSRC_DIR%\gnuwin32\bin
set PATH=%PATH%;C:\Python27


echo ���� configure...

cd /d %QTSRC_DIR%
call configure.bat -prefix %BUILD_DIR% -release -force-debug-info -opensource -shared -platform win32-msvc2010 -mp -nomake examples -nomake tests -no-opengl -no-angle -skip qtlocation -skip qtmultimedia -skip qtquick1 -skip qtquickcontrols -skip qtsensors -skip qtwebkit -skip qtwebkit-examples
@if not "%errorlevel%"=="0" goto error

echo ���� nmake...
nmake
@if not "%errorlevel%"=="0" goto error

echo ���� install...
nmake install
@if not "%errorlevel%"=="0" goto error

echo === ����ɹ� ===
pause
goto :eof

:error
echo === ����ʧ�� ===
@pause>NUL rem ��������˳�


REM �������úͱ�����ʹ��
REM nmake distclean
