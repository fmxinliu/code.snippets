@ chcp 936 >nul
@ rem title ��֤�����Ƿ�ɹ�

@ main.exe

@ set retcode=%errorlevel%
@ if '%retcode%'=='0' (
@ echo ���гɹ�
) else (
@ echo ����ʧ��
)

@ exit /b %retcode%
