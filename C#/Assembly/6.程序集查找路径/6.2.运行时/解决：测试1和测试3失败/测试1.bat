@ if "%1" neq "runas" (
@ call D:\Repo\Project\code.snippets\scripts\bat\admin\以管理员身份运行.bat %~0
@ exit
)

@ cd ..

@ call 生成可执行程序.bat
@ if not '%errorlevel%'=='0' @ exit

@ chcp 936 >nul
@ title 测试1：运行时指定查找【编译器所在目录】
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem 拷贝程序到编译器所在目录
@ xcopy /y DLLs\MyDLL.dll C:\Windows\Microsoft.NET\Framework\v4.0.30319\ >nul

@ echo 拷贝“指定查找外部目录.config”
@ copy /y 解决：测试1和测试3失败\Config\指定查找外部目录.config .\main.exe.config >nul
@ call 验证运行是否成功.bat needconfig

@ rem 删除测试程序集
@ del /q main.exe C:\Windows\Microsoft.NET\Framework\v4.0.30319\MyDLL.dll >nul

@ pause
