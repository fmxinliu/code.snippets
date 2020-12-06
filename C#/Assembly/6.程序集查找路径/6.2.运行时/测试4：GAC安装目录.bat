@ if "%1" neq "runas" (
@ call ..\..\..\..\scripts\bat\admin\以管理员身份运行.bat %~0
@ exit
)

@ call 生成可执行程序.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title 测试4：运行时是否查找【GAC安装目录】
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul


@ rem 安装到GAC
@ gacutil /i DLLs\MyDLL.dll

@ call 验证运行是否成功.bat

@ rem 删除测试程序集
@ del /q main.exe >nul


@ rem 从GAC卸载
@ gacutil /u MyDLL >nul

pause
