@ if "%1" neq "runas" (
@ call ..\..\..\..\scripts\bat\admin\�Թ���Ա�������.bat %~0
@ exit
)

@ call ���ɿ�ִ�г���.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title ����4������ʱ�Ƿ���ҡ�GAC��װĿ¼��
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul


@ rem ��װ��GAC
@ gacutil /i DLLs\MyDLL.dll

@ call ��֤�����Ƿ�ɹ�.bat

@ rem ɾ�����Գ���
@ del /q main.exe >nul


@ rem ��GACж��
@ gacutil /u MyDLL >nul

pause
