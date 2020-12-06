@ chcp 936 >nul
@ rem title 验证运行是否成功

@ if '%1'=='needconfig' goto RUN
@ if exist main.exe.config @ del /q main.exe.config

:RUN
@ main.exe

@ set retcode=%errorlevel%
@ if '%retcode%'=='0' (
@ echo 运行成功
) else (
@ echo 运行失败
)

@ if exist main.exe.config @ del /q main.exe.config
@ exit /b %retcode%
