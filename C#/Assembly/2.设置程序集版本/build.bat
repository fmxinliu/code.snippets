@ chcp 936 >nul
@ title 设置程序集版本
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem 编译程序集
@ csc /t:library /out:speaker.dll Speaker.cs >nul

@ rem 编译主程序
@ csc /t:exe /out:main.exe /r:speaker.dll Program.cs >nul

@ main.exe

@ rem 延时
@ ping -n 3 127.0.0.1 >nul

@ rem 反汇编 IL
@ ildasm /out:speaker.dll.txt main.exe >nul
@ type speaker.dll.txt

@ ping -n 5 127.0.0.1 >nul
@ del /q main.exe speaker.dll.*
