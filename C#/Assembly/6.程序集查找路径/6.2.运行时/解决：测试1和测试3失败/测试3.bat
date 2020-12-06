@ cd ..

@ call 生成可执行程序.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title 测试3：运行时指定查找【编译器所在目录】
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ echo 拷贝“指定查找子目录.config”
@ copy /y 解决：测试1和测试3失败\Config\指定查找子目录.config .\main.exe.config >nul
@ call 验证运行是否成功.bat needconfig

@ rem 删除测试程序集
@ del /q main.exe >nul

pause
