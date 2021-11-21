@echo off
title 倒计时
if "%1"=="" (
  set timeout=5
) else (
  set timeout=%1
)

mode con lines=7 cols=55

rem 开始计时
for /l %%i in (%timeout%,-1,1) do (
  cls
  echo.
  echo.
  echo.
  echo. %%i 后关闭...
  ping -n 2 localhost >nul
)
exit
