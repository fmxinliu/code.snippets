@echo off

>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"

if '%errorlevel%' NEQ '0' (

echo �������ԱȨ��...

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

@rem ��������������ֵ������ֵ����ֵ������
@rem for /f %%i in ('where python') do @ set python_path=%%i
@rem echo %python_path%

@ set python3_bin=C:\Python37\python.exe
@ set python3_venv=C:\Python37\venv\

@rem ��װ֧�����⻷���İ�virtualenvwrapper
@ pip list | findstr virtualenvwrapper >nul || pip install virtualenvwrapper-win

@rem ָ�����⻷������Ŀ¼
@ mkdir %python3_venv% >nul 2>nul
@ setx WORKON_HOME %python3_venv% /m
@ start mkvirtualenv -p %python3_bin% ai
