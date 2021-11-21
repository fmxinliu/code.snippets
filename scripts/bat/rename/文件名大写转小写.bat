@echo ����ǰ·�����ļ�orĿ¼���еĴ�дת��Сд
@echo ---
@echo usage: rename [ѡ��]
@echo               /f �����ļ�
@echo               /d ����Ŀ¼
@echo               /a �����ļ���Ŀ¼

@echo off

@rem ��������
for %%a in (%*) do (
    if /I "%%a"=="/f" set p1=1
    if /I "%%a"=="/d" set p2=2
    if /I "%%a"=="/a" set p3=3
    if /I "%%a"=="/h" goto Help
)

set /a sum=0
if defined p1 set /a sum+=1
if defined p2 set /a sum+=2
if defined p3 set /a sum=3

if "%sum%"=="0" goto Help
if "%sum%"=="3" goto All
if "%sum%"=="1" goto Files
if "%sum%"=="2" goto Directory

@rem ɾ������
set p1=
set p2=
set p3=
set sum=
pause

@rem �ݹ�������������еĴ�дת��Сд
:All
FOR /R %%A IN (.) DO cd %%A && (FOR /F %%B IN ('dir /a /b /l')  DO rename %%B %%B)
goto END

:Files
FOR /R %%A IN (.) DO cd %%A && (FOR /F %%B IN ('dir /a:a /b /l')  DO rename %%B %%B)
goto END

:Directory
FOR /R %%A IN (.) DO cd %%A && (FOR /F %%B IN ('dir /a:d /b /l')  DO rename %%B %%B)
goto END

:Help
pause && exit

:END
@echo ******����������******
pause