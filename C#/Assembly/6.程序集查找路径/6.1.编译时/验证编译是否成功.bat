@ chcp 936 >nul
@ rem title 验证编译是否成功
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem 编译主程序
@ csc /t:exe /out:main.exe /r:MyDLL.dll Program.cs

@ set retcode=%errorlevel%
@ if '%retcode%'=='0' (
@ echo 编译成功
) else (
@ echo 编译失败
)

@ exit /b %retcode%
