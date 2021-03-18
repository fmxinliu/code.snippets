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


echo 正在 configure...

cd /d %QTSRC_DIR%
call configure.bat -prefix %BUILD_DIR% -release -force-debug-info -opensource -shared -platform win32-msvc2010 -mp -nomake examples -nomake tests -no-opengl -no-angle -skip qtlocation -skip qtmultimedia -skip qtquick1 -skip qtquickcontrols -skip qtsensors -skip qtwebkit -skip qtwebkit-examples
@if not "%errorlevel%"=="0" goto error

echo 正在 nmake...
nmake
@if not "%errorlevel%"=="0" goto error

echo 正在 install...
nmake install
@if not "%errorlevel%"=="0" goto error

echo === 编译成功 ===
pause
goto :eof

:error
echo === 编译失败 ===
@pause>NUL rem 按任意键退出


REM 重新配置和编译请使用
REM nmake distclean
