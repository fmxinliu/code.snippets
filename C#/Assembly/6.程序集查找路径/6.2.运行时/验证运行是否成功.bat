@ chcp 936 >nul
@ rem title 验证运行是否成功

@ main.exe

@ set retcode=%errorlevel%
@ if '%retcode%'=='0' (
@ echo 运行成功
) else (
@ echo 运行失败
)

@ exit /b %retcode%
