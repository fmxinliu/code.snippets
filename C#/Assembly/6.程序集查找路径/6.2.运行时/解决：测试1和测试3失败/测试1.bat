@ if "%1" neq "runas" (
@ call D:\Repo\Project\code.snippets\scripts\bat\admin\�Թ���Ա�������.bat %~0
@ exit
)

@ cd ..

@ call ���ɿ�ִ�г���.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title ����1������ʱָ�����ҡ�����������Ŀ¼��
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem �������򵽱���������Ŀ¼
@ xcopy /y DLLs\MyDLL.dll C:\Windows\Microsoft.NET\Framework\v4.0.30319\ >nul

@ echo ������ָ�������ⲿĿ¼.config��
@ copy /y ���������1�Ͳ���3ʧ��\Config\ָ�������ⲿĿ¼.config .\main.exe.config >nul
@ call ��֤�����Ƿ�ɹ�.bat needconfig

@ rem ɾ�����Գ���
@ del /q main.exe C:\Windows\Microsoft.NET\Framework\v4.0.30319\MyDLL.dll >nul

@ pause
