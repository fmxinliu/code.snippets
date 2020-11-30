@ chcp 936 >nul
@ title 篡改程序集演示
@ call "%VS100COMNTOOLS%vsvars32.bat"

@ rem 编译程序集
@ csc /t:library /out:speaker.dll Speaker.cs

@ rem 编译主程序
@ csc /t:exe /out:main.exe /r:speaker.dll Program.cs

@ main.exe

@ echo.
@ echo.
@ echo "开始侵入>>>>>"
@ echo\
@ echo\

@ rem 编译被篡改后的程序集
@ csc /t:library /out:speaker.dll SpeakerHiJack.cs
@ main.exe

@ rem 延时删除
@ ping -n 5 127.0.0.1 >nul
@ del /q main.exe speaker.dll
