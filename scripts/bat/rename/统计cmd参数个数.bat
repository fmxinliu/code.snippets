@echo off

@rem ͳ�Ʋ�������
set num=
for %%a in (%*) do set /a num+=1
if defined num (echo %0%: ������ %num% ������) else echo %0%: û�д����κβ���
pause