@ call ����ǿ���Ƴ���.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title ����3������ʱ�Ƿ���ҡ�DLLs��Ŀ¼��
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ call ��֤�����Ƿ�ɹ�.bat

@ rem ɾ�����Գ���
@ del /q main.exe >nul

pause
