@ chcp 936 >nul
@ rem title ��֤�����Ƿ�ɹ�

@ if '%1'=='needconfig' goto RUN
@ if exist main.exe.config @ del /q main.exe.config

:RUN
@ main.exe

@ set retcode=%errorlevel%
@ if '%retcode%'=='0' (
@ echo ���гɹ�
) else (
@ echo ����ʧ��
)

@ if exist main.exe.config @ del /q main.exe.config
@ exit /b %retcode%
