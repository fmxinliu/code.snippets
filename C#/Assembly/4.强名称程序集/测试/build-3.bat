@ chcp 936 >nul
@ title 模拟程序集被替换（DLL劫持）
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ------------------- 模拟程序集被替换 ---------------------------
@ echo.
@ echo.
@ echo "开始替换DLL>>>>>"
@ echo\
@ echo\

@ rem 编译被替换后的程序集
@ csc /t:library /out:speaker.dll SpeakerHiJack.cs

@ exit
