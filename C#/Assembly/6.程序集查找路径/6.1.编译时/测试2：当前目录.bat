@ call ����ǿ���Ƴ���.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title ����2������ʱ�Ƿ���ҡ���ǰĿ¼��
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem �������򵽱���������Ŀ¼
@ xcopy /y DLLs\MyDLL.dll %CD% >nul

@ call ��֤�����Ƿ�ɹ�.bat

@ rem ɾ�����Գ���
@ del /q main.exe MyDLL.dll >nul

pause
