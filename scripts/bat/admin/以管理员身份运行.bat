@echo off

>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"

if '%errorlevel%' NEQ '0' (

echo 请求管理员权限...

goto UACPrompt

) else ( goto gotAdmin )

:UACPrompt

echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"

echo UAC.ShellExecute "%~s0", "%1", "", "runas", 1 >> "%temp%\getadmin.vbs"

"%temp%\getadmin.vbs"

exit /B 0

:gotAdmin

if exist "%temp%\getadmin.vbs" ( del "%temp%\getadmin.vbs" )

pushd "%CD%"

CD /D "%~dp0"

@ echo on
@ rem call user scripts
@ rem call xxx.bat %1 & exit
@ rem if '%1' NEQ '' ( call %1 runas ) & exit

@ for /f "delims=" %%f in (%1) do @ set script_dir=%~dp1
@ CD /D "%script_dir%"
@ if '%1' NEQ '' ( call %1 runas ) & exit
