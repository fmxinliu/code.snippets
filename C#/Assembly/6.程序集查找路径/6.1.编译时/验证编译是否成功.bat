@ chcp 936 >nul
@ rem title ��֤�����Ƿ�ɹ�
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ����������
@ csc /t:exe /out:main.exe /r:MyDLL.dll Program.cs

@ set retcode=%errorlevel%
@ if '%retcode%'=='0' (
@ echo ����ɹ�
) else (
@ echo ����ʧ��
)

@ exit /b %retcode%
