@ call 生成强名称程序集.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title 测试3：编译时是否查找【DLLs子目录】
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ call 验证编译是否成功.bat

@ rem 删除测试程序集
@ del /q main.exe >nul

pause
