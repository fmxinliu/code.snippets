@echo off
title ����ʱ
if "%1"=="" (
  set timeout=5
) else (
  set timeout=%1
)

mode con lines=7 cols=55

rem ��ʼ��ʱ
for /l %%i in (%timeout%,-1,1) do (
  cls
  echo.
  echo.
  echo.
  echo. %%i ��ر�...
  ping -n 2 localhost >nul
)
exit
