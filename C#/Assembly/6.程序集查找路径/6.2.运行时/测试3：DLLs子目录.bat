@ call 生成可执行程序.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title 测试3：运行时是否查找【DLLs子目录】
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ call 验证运行是否成功.bat

@ rem 删除测试程序集
@ del /q main.exe >nul

pause
