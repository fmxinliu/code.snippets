@echo 将当前路径下文件or目录名中的大写转成小写
@echo ---
@echo usage: rename [选项]
@echo               /f 所有文件
@echo               /d 所有目录
@echo               /a 所有文件和目录

@echo off

@rem 解析参数
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

@rem 删除变量
set p1=
set p2=
set p3=
set sum=
pause

@rem 递归遍历，将名字中的大写转成小写
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
@echo ******重命名结束******
pause