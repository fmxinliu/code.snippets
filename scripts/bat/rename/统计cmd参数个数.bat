@echo off

@rem 统计参数个数
set num=
for %%a in (%*) do set /a num+=1
if defined num (echo %0%: 传入了 %num% 个参数) else echo %0%: 没有传入任何参数
pause