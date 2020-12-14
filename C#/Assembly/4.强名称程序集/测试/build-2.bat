@ chcp 936 >nul
@ title 模拟程序集被篡改（DLL注入）
@ call "%VS100COMNTOOLS%vsvars32.bat" >nul

@ rem ------------------- 模拟程序集被篡改 ---------------------------
@ rem 编译篡改程序
@ csc /t:exe /out:speakerAttack.exe SpeakerAttack.cs

@ echo.
@ echo.
@ echo "开始篡改DLL>>>>>"
@ echo\
@ echo\

@ rem 运行篡改程序
@ copy /y speaker.dll AssemblyLib.dll >nul
@ speakerAttack.exe
@ copy /y AssemblyLib.dll speaker.dll >nul

@ rem 删除临时文件
@ del /q speakerAttack.exe AssemblyLib.dll >nul

@ exit
