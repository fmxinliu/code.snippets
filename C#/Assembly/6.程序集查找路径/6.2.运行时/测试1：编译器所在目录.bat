@ if "%1" neq "runas" (
@ call D:\Repo\Project\code.snippets\scripts\bat\admin\�Թ���Ա�������.bat %~0
@ exit
)

@ call ���ɿ�ִ�г���.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title ����1������ʱ�Ƿ���ҡ�����������Ŀ¼��
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem �������򵽱���������Ŀ¼
@ xcopy /y DLLs\MyDLL.dll C:\Windows\Microsoft.NET\Framework\v4.0.30319\ >nul

@ call ��֤�����Ƿ�ɹ�.bat

@ rem ɾ�����Գ���
@ del /q main.exe C:\Windows\Microsoft.NET\Framework\v4.0.30319\MyDLL.dll >nul

@ pause
